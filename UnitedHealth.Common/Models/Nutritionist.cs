using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitedHealth.Common.Models
{
    public class Nutritionist : User
    {
        public Nutritionist() { }
        public Nutritionist(User user)
        {
            this.PhoneNumber = user.PhoneNumber;
            this.BirthDate = user.BirthDate;
            this.Name = user.Name;
            this.Email = user.Email;
            this.Type = user.Type;
            this.Password = user.Password;
            this.Username = user.Username;

            this.FreeTimeSlots = new List<DateTime>();
        }

        public List<DateTime> FreeTimeSlots { get; set; }
    }
}
