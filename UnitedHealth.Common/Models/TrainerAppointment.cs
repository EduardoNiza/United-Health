using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitedHealth.Common.Models
{
    public class TrainerAppointment
    {
        public TrainerAppointment() { }
        public TrainerAppointment(Patient patient, Trainer trainer, DateTime scheduled, string prescription, bool isCompleted)
        {
            Patient = patient;
            Trainer = trainer;
            Scheduled = scheduled;
            Prescription = prescription;
            IsCompleted = isCompleted;
        }

        public int Id { get; set; }
        public Patient Patient { get; set; }
        public Trainer Trainer { get; set; }
        public DateTime Scheduled { get; set; }
        public string Prescription { get; set; }
        public bool IsCompleted { get; set; }
    }
}
