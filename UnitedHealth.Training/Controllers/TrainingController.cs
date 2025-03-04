using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using UnitedHealth.Common.Database;
using UnitedHealth.Common.Models;

namespace UnitedHealth.Training.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class TrainingController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        private readonly ILogger<TrainingController> _logger;

        public TrainingController(ILogger<TrainingController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }


        [HttpGet("GetAllTrainer")]
        [Authorize(Policy = "PatientOnly")]
        public ActionResult<IEnumerable<Dictionary<string, string>>> GetAllTrainers()
        {

            var trainer = _context.Users
                .OfType<Trainer>()
                .Select(trainer => new
                {
                    trainer.Id,
                    trainer.Name,
                    trainer.FreeTimeSlots
                })
                .ToList();

            return Ok(trainer.Select(d => new Dictionary<string, string>
        {
            { "name", d.Name },
            { "freeTimeSlots", string.Join(", ", d.FreeTimeSlots.Select(dt => dt.ToString())) },
            { "id", d.Id.ToString() },
            }).ToList());
        }

        [HttpPost("InsertFreeTimeSlot")]
        [Authorize(Policy = "TrainingOnly")]
        public IActionResult InsertFreeTimeSlot([FromQuery] DateTime date)
        {
            string id = HttpContext.User.FindFirstValue("id");
            Trainer trainer = _context.Trainer.First(a => a.Id.ToString() == id);

            date = DateTime.SpecifyKind(date, DateTimeKind.Utc);
            trainer.FreeTimeSlots.Add(date);
            _context.SaveChanges();

            return Ok();
        }


        [HttpPost("InsertConsultation")]
        [Authorize(Policy = "PatientOnly")]
        public IActionResult InsertConsultation([FromQuery] DateTime date, int trainerId)
        {
            Trainer trainer = _context.Trainer.First(a => a.Id == trainerId);

            date = DateTime.SpecifyKind(date, DateTimeKind.Utc);

            if (!trainer.FreeTimeSlots.Remove(date))
            {
                return this.BadRequest(new ErrorObject("This date is not available"));
            }

            string idPatient = HttpContext.User.FindFirstValue("id");

            Patient patient = _context.Patients.First(a => a.Id.ToString() == idPatient);

            TrainerAppointment trainerAppointment = new TrainerAppointment(patient, trainer, date, "", false);

            this._context.TrainerAppointments.Add(trainerAppointment);

            _context.SaveChanges();

            return Ok();
        }

        [HttpGet("GetAllConsultation")]
        public IActionResult GetAllConsultation()
        {
            string id = HttpContext.User.FindFirstValue("id");

            var consultations = _context.TrainerAppointments
                .Include(appointment => appointment.Trainer)
                .Include(appointment => appointment.Patient)
                .Where(appointment => appointment.Trainer.Id.ToString() == id || appointment.Patient.Id.ToString() == id)
                .Select(appointment => new
                {
                    AppointmentId = appointment.Id,
                    DoctorName = appointment.Trainer.Name,
                    PatientName = appointment.Patient.Name,
                    Scheduled = appointment.Scheduled,
                    Prescription = appointment.Prescription,
                    IsCompleted = appointment.IsCompleted
                })
                .ToList();

            return Ok(consultations);
        }

        [HttpGet("GetAllPatients")]
        [Authorize(Policy = "TrainingOnly")]
        public IActionResult GetAllPatients(){
            string id = HttpContext.User.FindFirstValue("id");
            //ou en sql: select patient_id from TrainingApointment where doctor_id=id
            var patients = _context.TrainerAppointments
                .Include(appointment => appointment.Trainer)
                .Include(appointment => appointment.Patient)
                .Where(appointment => appointment.Trainer.Id.ToString() == id)
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



        [HttpPost("CancelConsultation")]
        [Authorize(Policy = "PatientOnly")]
        public IActionResult UpdateConsultationStatus([FromQuery] int appointmentId)
        {
            var appointment = _context.TrainerAppointments
                .Include(appointment => appointment.Trainer)
                .Include(appointment => appointment.Patient)
                .FirstOrDefault(a => a.Id == appointmentId);

            if (appointment == null)
            {
                return NotFound();
            }

            appointment.Trainer.FreeTimeSlots.Add(appointment.Scheduled);
            appointment.IsCompleted = true;
            _context.SaveChanges();

            return Ok();
        }

        [HttpPost("InsertWorkouts")] // nesse caso sao os treinos que o paciente vai executar na consulta
        [Authorize(Policy = "TrainingOnly")]
        public IActionResult InsertTrainingPrescription([FromQuery] int appointmentId, [FromQuery] string workouts)
        {
            var appointment = _context.TrainerAppointments.FirstOrDefault(a => a.Id == appointmentId);

            if (appointment == null || appointment.IsCompleted)
            {
                return NotFound(new { message = "Appointment not found" });
            }

            appointment.Prescription = workouts;
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

            var lastConsultation = _context.TrainerAppointments
                .Include(appointment => appointment.Trainer)
                .Include(appointment => appointment.Patient)
                .Where(appointment => (appointment.Trainer.Id.ToString() == id || appointment.Patient.Id.ToString() == id) && appointment.Scheduled <= currentUtcDate)
                .OrderByDescending(appointment => appointment.IsCompleted)
                .Select(appointment => new
                {
                    Name = appointment.Trainer.Name,
                    Prescription = appointment.Prescription,
                })
                .FirstOrDefault();

            return Ok(lastConsultation);
        }

    }
}
