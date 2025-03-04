using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UnitedHealth.Common.Database;
using UnitedHealth.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

[Authorize]
[ApiController]
[Route("[controller]")]
public class MedicalController : ControllerBase
{
    private readonly ILogger<MedicalController> _logger;
    private readonly ApplicationDbContext _context;

    public MedicalController(ILogger<MedicalController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpPost("InsertFreeTimeSlot")]
    [Authorize(Policy = "MedicalOnly")]
    public IActionResult InsertFreeTimeSlot([FromQuery] DateTime date)
    {
        string id = HttpContext.User.FindFirstValue("id");
        Doctor doctor = _context.Doctors.First(a => a.Id.ToString() == id);

        date = DateTime.SpecifyKind(date, DateTimeKind.Utc);
        doctor.FreeTimeSlots.Add(date);
        _context.SaveChanges();

        return Ok();
    }

    [HttpPost("InsertSpecialty")]
    [Authorize(Policy = "MedicalOnly")]
    public IActionResult InsertSpecialty([FromQuery] MedicalSpecialties specialty)
    {
        string id = HttpContext.User.FindFirstValue("id");
        Doctor doctor = _context.Doctors.First(a => a.Id.ToString() == id);

        doctor.Specialty = specialty;
        _context.SaveChanges();

        return Ok();
    }


    [HttpPost("InsertMedicalPrescription")]
    [Authorize(Policy = "MedicalOnly")]
    public IActionResult InsertMedicalPrescription([FromQuery] int appointmentId, [FromQuery] string medicalPrescription)
    {
        var appointment = _context.MedicalAppointments.FirstOrDefault(a => a.Id == appointmentId);

        if (appointment == null || appointment.IsCompleted)
        {
            return NotFound(new { message = "Appointment not found" });
        }

        appointment.Prescription = medicalPrescription;
        appointment.IsCompleted = true;
        _context.SaveChanges();

        return Ok();
    }



    [HttpPost("InsertConsultation")]
    [Authorize(Policy = "PatientOnly")]
    public IActionResult InsertConsultation([FromQuery] DateTime date, int doctorId)
    {
        Doctor doctor = _context.Doctors.First(a => a.Id == doctorId);

        date = DateTime.SpecifyKind(date, DateTimeKind.Utc);

        if (!doctor.FreeTimeSlots.Remove(date))
        {
            return this.BadRequest(new ErrorObject("This date is not available"));
        }

        string idPatient = HttpContext.User.FindFirstValue("id");

        Patient patient = _context.Patients.First(a => a.Id.ToString() == idPatient);

        MedicalAppointment medicalAppointment = new MedicalAppointment(patient, doctor, date, "", false);

        this._context.MedicalAppointments.Add(medicalAppointment);

        _context.SaveChanges();

        return Ok();
    }

    [HttpGet("GetAllConsultation")]
    public IActionResult GetAllConsultation()
    {
        string id = HttpContext.User.FindFirstValue("id");

        var consultations = _context.MedicalAppointments
            .Include(appointment => appointment.Doctor)
            .Include(appointment => appointment.Patient)
            .Where(appointment => appointment.Doctor.Id.ToString() == id || appointment.Patient.Id.ToString() == id)
            .Select(appointment => new
            {
                AppointmentId = appointment.Id,
                DoctorName = appointment.Doctor.Name,
                PatientName = appointment.Patient.Name,
                Scheduled = appointment.Scheduled,
                Prescription = appointment.Prescription,
                IsCompleted = appointment.IsCompleted
            })
            .ToList();

        return Ok(consultations);
    }

    [HttpGet("GetCurrentSpecialty")]
    [Authorize(Policy = "MedicalOnly")]
    public IActionResult GetCurrentSpecialty()
    {
        string id = HttpContext.User.FindFirstValue("id");

        Doctor doctor = _context.Doctors
            .Where(doc => doc.Id.ToString() == id)
            .First();

        return Ok(new { Name = doctor.Specialty.ToString() });
    }

    [HttpGet("GetDoctors")]
    [Authorize(Policy = "PatientOnly")]
    public ActionResult<IEnumerable<Dictionary<string, string>>> GetDoctorsBySpecialty(MedicalSpecialties specialty)
    {

        var doctors = _context.Doctors
            .Where(doctor => doctor.Specialty == specialty)
            .Select(doctor => new
            {
                doctor.Id,
                doctor.Name,
                doctor.Specialty,
                doctor.FreeTimeSlots
            })
            .ToList();


        return Ok(doctors.Select(d => new Dictionary<string, string>{
            { "name", d.Name },
            { "specialty", d.Specialty.ToString() },
            { "freeTimeSlots", string.Join(", ", d.FreeTimeSlots.Select(dt => dt.ToString())) },
            { "id", d.Id.ToString() },
            }).ToList()
        );
    }

    [HttpGet("GetSpecialties")]
    public ActionResult<IEnumerable<Dictionary<string, string>>> GetSpecialties()
    {
        return Ok(Enum.GetValues(typeof(MedicalSpecialties))
            .Cast<MedicalSpecialties>()
            .Select(s => s.ToString()));
    }

    [HttpPost("CancelConsultation")]
    [Authorize(Policy = "PatientOnly")]
    public IActionResult UpdateConsultationStatus([FromQuery] int appointmentId)
    {
        var appointment = _context.MedicalAppointments
            .Include(appointment => appointment.Doctor)
            .FirstOrDefault(a => a.Id == appointmentId);

        if (appointment == null)
        {
            return NotFound();
        }

        appointment.Doctor.FreeTimeSlots.Add(appointment.Scheduled);
        appointment.IsCompleted = true;
        _context.SaveChanges();

        return Ok();
    }




}
// Add - Migration Doctor - Project UnitedHealth.Common - StartupProject UnitedHealth.Server

//Update-Database -Project UnitedHealth.Common -StartupProject UnitedHealth.Server

//   http://localhost:5207/Medical/InsertConsultation?date=2023-02-23T13:00:00&doctorId=19
// Remove-Migration -Project UnitedHealth.Common -StartupProject UnitedHealth.Server

//http://localhost:5207/Medical/InsertSpecialty?specialty=Pediatra