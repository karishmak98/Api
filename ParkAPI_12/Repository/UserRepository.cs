using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ParkAPI_12.Data;
using ParkAPI_12.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ParkAPI_12.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly appSettings _appSettings;
        public UserRepository(ApplicationDbContext context,IOptions<appSettings> appSettings)
        {
            _context = context;
            _appSettings = appSettings.Value;
        }
        public User Authenticate(string userName, string password)
        {
            var UserInDb=_context.Users.FirstOrDefault(u=>u.Username==userName && u.Password==password);
            if (UserInDb == null) 
                return null;
            //JWT Authentication
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, UserInDb.Id.ToString()),
                    new Claim(ClaimTypes.Role, UserInDb.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
            SecurityAlgorithms.HmacSha256Signature)
            };
            var token =tokenHandler.CreateToken(tokenDescriptor);
            UserInDb.Token = tokenHandler.WriteToken(token);
            UserInDb.Password = "";

            return UserInDb;

        }

        public bool IsUniqueUser(string userName)
        {
            var user=_context.Users.FirstOrDefault(u=>u.Username==userName);
            if (user == null)
                return true;
            else
                return false;
        }

        public User Register(string userName, string password)
        {
            User user = new User()
            {
                Username = userName,
                Password=password,
                Role="Admin"
            };
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;  
        }
    }
}
