using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiCore_facebook.Library;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiCore_facebook.Controllers
{
    //Không cần kiểm tra version
    //[ApiVersionNeutral]
    //[Route("api/[controller]")]


   
    [ApiVersionNeutral]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        
        [Route("authenticate"), HttpPost]
        public IActionResult Authenticate([FromBody]User userParam)
        {
            //return Ok(userParam.Username + "- "+ userParam.Password);
            var user = _userService.Authenticate(userParam.Username, userParam.Password);
            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });
            return Ok(user);
        }
        
       
    }
}
