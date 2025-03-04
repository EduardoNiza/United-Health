namespace UnitedHealth.Server.Models
{
    public class UserInfo
    {
        public UserInfo(int id, string username, string token, string profile, DateTime birthDate, string name, string email, string phoneNumber)
        {
            Id = id;
            Username = username;
            Token = token;
            Profile = profile;
            BirthDate = birthDate;
            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }
        public string Profile { get; set; }
        public DateTime BirthDate { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

    }
}
