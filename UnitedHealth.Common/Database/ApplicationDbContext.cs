using Microsoft.EntityFrameworkCore;
using UnitedHealth.Common.Models;

namespace UnitedHealth.Common.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Nutritionist> Nutritionists { get; set; }
        public DbSet<Patient> Patients { get; set; }

        public DbSet<Trainer> Trainer { get; set; }

        public DbSet<TrainerAppointment> TrainerAppointments { get; set; }
        public DbSet<MedicalAppointment> MedicalAppointments { get; set; }
        public DbSet<NutritionalAppointment> NutritionalAppointments { get; set; }

        


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }

    }
}
