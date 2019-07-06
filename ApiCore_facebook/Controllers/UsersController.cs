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
    [ApiVersionNeutral]
    //[Route("api/[controller]")]
    //[Authorize]
    [Produces("application/json")]
    [EnableCors("AllowOrigin")]
    public class UsersController : Controller
    {
        // GET: api/<controller>
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        //[AllowAnonymous]
        [HttpPost]
        [Route("api/login/authenticate")]
        public IActionResult Authenticate([FromBody]User userParam)
        {
            var user = _userService.Authenticate(userParam.Username, userParam.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }
        
       
    }
}
