using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace UnitedHealth.Common.Models
{
    public class Doctor : User
    {
        public Doctor() { }
        public Doctor(User user)
        {
            this.PhoneNumber = user.PhoneNumber;
            this.BirthDate = user.BirthDate;
            this.Name = user.Name;
            this.Email = user.Email;
            this.Type = user.Type;
            this.Password = user.Password;
            this.Username = user.Username;

            this.FreeTimeSlots = new List<DateTime>();
            this.Specialty = MedicalSpecialties.General;
        }

        public List<DateTime> FreeTimeSlots { get; set; }
        public MedicalSpecialties Specialty { get; set; }
    }

    public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.Property(e => e.Specialty)
                .HasConversion(
                    v => v.ToString(),
                    v => (MedicalSpecialties)Enum.Parse(typeof(MedicalSpecialties), v));
        }
    }
}
