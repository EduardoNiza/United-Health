using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UnitedHealth.Common.Database;
using UnitedHealth.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

[Authorize]
[ApiController]
[Route("[controller]")]
public class NutritionalController : ControllerBase
{
    private readonly ILogger<NutritionalController> _logger;
    private readonly ApplicationDbContext _context;

    public NutritionalController(ILogger<NutritionalController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpPost("InsertFreeTimeSlot")]
    [Authorize(Policy = "NutritionOnly")]
    public IActionResult InsertFreeTimeSlot([FromQuery] DateTime date)
    {
        string id = HttpContext.User.FindFirstValue("id");
        Nutritionist doctor = _context.Nutritionists.First(a => a.Id.ToString() == id);

        date = DateTime.SpecifyKind(date, DateTimeKind.Utc);
        doctor.FreeTimeSlots.Add(date);
        _context.SaveChanges();

        return Ok();
    }

    [HttpGet("GetAllPatients")]
    [Authorize(Policy = "NutritionOnly")]
    public IActionResult GetAllPatients()
    {
        string id = HttpContext.User.FindFirstValue("id");
        //ou en sql: select patient_id from TrainingApointment where doctor_id=id
        var patients = _context.NutritionalAppointments
            .Include(appointment => appointment.Nutritionist)
            .Include(appointment => appointment.Patient)
            .Where(appointment => appointment.Nutritionist.Id.ToString() == id)
            .Select(appointment => new
            {
                appointment.Patient.Id,
                appointment.Patient.Name,
                appointment.Patient.BirthDate,
                appointment.Patient.Email,
                appointment.Patient.Username,
                appointment.Patient.PhoneNumber
            })
            .Distinct()
            .ToList();

        return Ok(patients);
    }


    [HttpPost("InsertNutritionPrescription")]
    [Authorize(Policy = "NutritionOnly")]
    public IActionResult InsertNutritionPrescription([FromQuery] int appointmentId, [FromQuery] string prescription)
    {
        var appointment = _context.NutritionalAppointments.FirstOrDefault(a => a.Id == appointmentId);

        if (appointment == null || appointment.IsCompleted)
        {
            return NotFound(new { message = "Appointment not found" });
        }

        appointment.Prescription = prescription;
        appointment.IsCompleted = true;
        _context.SaveChanges();

        return Ok();
    }



    [HttpPost("InsertConsultation")]
    [Authorize(Policy = "PatientOnly")]
    public IActionResult InsertConsultation([FromQuery] DateTime date, int nutritionistId)
    {
        Nutritionist nutritionist = _context.Nutritionists.First(a => a.Id == nutritionistId);

        date = DateTime.SpecifyKind(date, DateTimeKind.Utc);

        if (!nutritionist.FreeTimeSlots.Remove(date))
        {
            return this.BadRequest(new ErrorObject("This date is not available"));
        }

        string idPatient = HttpContext.User.FindFirstValue("id");

        Patient patient = _context.Patients.First(a => a.Id.ToString() == idPatient);

        NutritionalAppointment nutritionalAppointment = new NutritionalAppointment(patient, nutritionist, date, "", false);

        this._context.NutritionalAppointments.Add(nutritionalAppointment);

        _context.SaveChanges();

        return Ok();
    }

    [HttpGet("GetAllConsultation")]
    public IActionResult GetAllConsultation()
    {
        string id = HttpContext.User.FindFirstValue("id");

        var consultations = _context.NutritionalAppointments
            .Include(appointment => appointment.Nutritionist)
            .Include(appointment => appointment.Patient)
            .Where(appointment => appointment.Nutritionist.Id.ToString() == id || appointment.Patient.Id.ToString() == id)
            .Select(appointment => new
            {
                AppointmentId = appointment.Id, 
                NutritionistName = appointment.Nutritionist.Name,
                PatientName = appointment.Patient.Name,
                Scheduled = appointment.Scheduled,
                Prescription = appointment.Prescription,
                IsCompleted = appointment.IsCompleted
            })
            .ToList();

        return Ok(consultations);
    }



    [HttpGet("GetAllNutritionists")]
    [Authorize(Policy = "PatientOnly")]
    public ActionResult<IEnumerable<Dictionary<string, string>>> GetAllNutritionists()
    {
 
            var nutritionists = _context.Users
                .OfType<Nutritionist>() 
                .Select(nutritionist => new
                {
                    nutritionist.Id,
                    nutritionist.Name,
                    nutritionist.FreeTimeSlots
                })
                .ToList();


        return Ok(nutritionists.Select(n => new Dictionary<string, string>
    {
        { "name", n.Name },
        { "freeTimeSlots", string.Join(", ", n.FreeTimeSlots.Select(dt => dt.ToString())) },
        { "id", n.Id.ToString() },
        }).ToList());
    }

    [HttpPost("CancelConsultation")]
    [Authorize(Policy = "PatientOnly")]
    public IActionResult UpdateConsultationStatus([FromQuery] int appointmentId)
    {
        var appointment = _context.NutritionalAppointments
            .Include(appointment => appointment.Nutritionist)
            .Include(appointment => appointment.Patient)
            .FirstOrDefault(a => a.Id == appointmentId);

        if (appointment == null)
        {
            return NotFound();
        }

        appointment.Nutritionist.FreeTimeSlots.Add(appointment.Scheduled);
        appointment.IsCompleted = true;
        _context.SaveChanges();

        return Ok();
    }

    [HttpGet("GetLastPrescription")]
    [Authorize(Policy = "PatientOnly")]
    public IActionResult GetLastPrescription()
    {
        string id = HttpContext.User.FindFirstValue("id");
        DateTime currentUtcDate = DateTime.UtcNow;

        var lastConsultation = _context.NutritionalAppointments
            .Include(appointment => appointment.Nutritionist)
            .Include(appointment => appointment.Patient)
            .Where(appointment => (appointment.Nutritionist.Id.ToString() == id || appointment.Patient.Id.ToString() == id) && appointment.Scheduled <= currentUtcDate)
            .OrderByDescending(appointment => appointment.IsCompleted)
            .Select(appointment => new
            {
                Name = appointment.Nutritionist.Name,
                Prescription = appointment.Prescription,
            })
            .FirstOrDefault();

        return Ok(lastConsultation);
    }





}
// Add - Migration Doctor - Project UnitedHealth.Common - StartupProject UnitedHealth.Server

//Update-Database -Project UnitedHealth.Common -StartupProject UnitedHealth.Server

//   http://localhost:5207/Medical/InsertConsultation?date=2023-02-23T13:00:00&doctorId=19
// Remove-Migration -Project UnitedHealth.Common -StartupProject UnitedHealth.Server

//http://localhost:5207/Medical/InsertSpecialty?specialty=Pediatra