using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitedHealth.Common.Models
{
    public class MedicalAppointment
    {
        public MedicalAppointment() { }
        public MedicalAppointment(Patient patient, Doctor doctor, DateTime scheduled, string prescription, bool isCompleted)
        {
            Patient = patient;
            Doctor = doctor;
            Scheduled = scheduled;
            Prescription = prescription;
            IsCompleted = isCompleted;
        }

        public int Id {  get; set; }
        public Patient Patient { get; set; }
        public Doctor Doctor { get; set; }

        public DateTime Scheduled { get; set; }
        public string Prescription { get; set; }
        public bool IsCompleted { get; set; }
    }



}
