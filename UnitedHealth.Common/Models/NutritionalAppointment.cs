using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitedHealth.Common.Models
{
    public class NutritionalAppointment
    {
        public NutritionalAppointment() { }
        public NutritionalAppointment(Patient patient, Nutritionist nutritionist, DateTime scheduled, string prescription, bool isCompleted)
        {
            Patient = patient;
            Nutritionist = nutritionist;
            Scheduled = scheduled;
            Prescription = prescription;
            IsCompleted = isCompleted;
        }

        public int Id { get; set; }
        public Patient Patient { get; set; }
        public Nutritionist Nutritionist { get; set; }

        public DateTime Scheduled { get; set; }
        public string Prescription { get; set; }
        public bool IsCompleted { get; set; }
    }
}