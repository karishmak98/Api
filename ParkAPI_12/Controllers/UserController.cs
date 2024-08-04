using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkAPI_12.Repository.IRepository;
using ParkAPI_12.View_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkAPI_12.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] User user)
        {
            if(ModelState.IsValid)
            {
                var isUniqueUser = _userRepository.IsUniqueUser(user.Username);
                if (!isUniqueUser)
                    return BadRequest("Username is used");
                var userInfo = _userRepository.Register(user.Username, user.Password);
                if (userInfo == null) return BadRequest();
            }
            return Ok();
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] UserVM userVM)
        {
            var user = _userRepository.Authenticate(userVM.UserName, userVM.Password);
            if (user == null)
                return BadRequest("wrong user/pwd");
            return Ok(user);
                
        }

    }
}
