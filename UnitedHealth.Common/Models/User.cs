using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace UnitedHealth.Common.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime BirthDate { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public UserTypes Type { get; set; }
    }

    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasIndex(p => p.Username)
                .IsUnique();

            builder.UseTptMappingStrategy();

            builder.Property(e => e.Type)
                .HasConversion(
                    v => v.ToString(),
                    v => (UserTypes)Enum.Parse(typeof(UserTypes), v));
        }
    }
}
