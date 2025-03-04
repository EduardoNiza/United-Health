using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UnitedHealth.Common.Database;
using UnitedHealth.Common.Models;
using UnitedHealth.Server.Config;
using UnitedHealth.Server.Models;

namespace UnitedHealth.Server.Controllers
{
    [ApiController]
    [Route("/auth")]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ITokenBuilder _tokenBuilder;

        public AuthController(
            ApplicationDbContext context,
            ITokenBuilder tokenBuilder)
        {
            _context = context;
            _tokenBuilder = tokenBuilder;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto user)
        {
            var dbUser = await _context
                .Users
                .SingleOrDefaultAsync(u => u.Username == user.Username);

            if (dbUser == null)
            {
                return this.NotFound(new ErrorObject("User not found."));
            }

            PasswordHasher<User> passwordHasher = new PasswordHasher<User>();
            PasswordVerificationResult isValid = passwordHasher.VerifyHashedPassword(dbUser, dbUser.Password, user.Password);

            if (isValid != PasswordVerificationResult.Success)
            {
                return this.BadRequest(new ErrorObject("Could not authenticate user."));
            }

            string token = _tokenBuilder.BuildToken(dbUser.Id.ToString(), dbUser.Type.ToString());

            return this.Ok(new UserInfo(dbUser.Id, dbUser.Username, token, dbUser.Type.ToString(), dbUser.BirthDate, dbUser.Name, dbUser.Email, dbUser.PhoneNumber));
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] User user)
        {
            PasswordHasher<User> passwordHasher = new PasswordHasher<User>();
            user.Password = passwordHasher.HashPassword(user, user.Password);
            user.BirthDate = DateTime.SpecifyKind(user.BirthDate, DateTimeKind.Utc);
            try
            {
                if(user.Type == UserTypes.Medical) {
                    Doctor doc = new Doctor(user);
                    this._context.Doctors.Add(doc);
                }

                if (user.Type == UserTypes.Patient)
                {
                    Patient patient = new Patient(user);
                    this._context.Patients.Add(patient);
                }

                if (user.Type == UserTypes.Training)
                {
                    Trainer doc = new Trainer(user);
                    this._context.Trainer.Add(doc);
                }


                if (user.Type == UserTypes.Nutrition)
                {
                    Nutritionist doc = new Nutritionist(user);
                    this._context.Nutritionists.Add(doc);
                }

                this._context.SaveChanges();
            } catch (Exception ex)
            {
                return this.BadRequest(new ErrorObject($"An error occurred: {ex.Message}"));
            }
            

            return this.Ok();
        }

    } 

}
