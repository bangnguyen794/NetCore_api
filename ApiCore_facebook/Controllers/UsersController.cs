using System;
using System.Collections.Generic;

using System.Threading.Tasks;
using ApiCore_facebook.ClassController.log;
using ApiCore_facebook.Library;
using ApiCore_facebook.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiCore_facebook.Controllers
{
    //Không cần kiểm tra version
    //[ApiVersionNeutral]
    //[Route("api/[controller]")]


    /// <summary>
    /// Login user token
    /// </summary>
    [Authorize]
    [ApiVersionNeutral]//không check phiên bản api
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class UsersController : ControllerBase
    {

        private IUserService _userService;
        private IConfiguration configuration;
        private bool ConsoleError = false;
        db_facebook_vmContext XLDL = new db_facebook_vmContext();
        private readonly ILogRepository _logRepository;
        private readonly ILogger _logger;
        public UsersController(IUserService userService, ILogRepository logRepository, ILoggerFactory logger, IConfiguration iconfig)
        {
            _userService = userService;
            _logRepository = logRepository;
            _logger = logger.CreateLogger("ApiCore_facebook.Controllers.UsersController");
            configuration = iconfig; //Lấy ra value file appseting.json
            ConsoleError = bool.Parse(configuration.GetSection("ConsoleError").Value);
        }

        /// <summary>
        /// Đang nhaaoj xác thực facebook
        /// </summary>
        /// <param name="userParam"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [Route("authenticate"), HttpPost]
        [ResponseCache(Duration = 86400, VaryByHeader = "authenticate_login")]
        public async Task<IActionResult>  Authenticate([FromBody]User userParam)
        {
            try
            {
                string url = "https://graph.facebook.com/me?fields=id,name&access_token=" + userParam.Token;
                string jsonResult = await httpClientCall.Get(url);//Return chuỗi
                JObject Mydata = JObject.Parse(jsonResult);//fomat json
                if (Mydata["error"]==null)
                {
                    string id_user = Mydata["id"].ToString(),name = Mydata["name"].ToString();
                    if(id_user == userParam.id_user)
                    {
                        var user = _userService.Authenticate(userParam.id_user);
                        if (user == null)
                        {
                            _logger.LogInformation(LoggingEvents.GetItemNotFound, "Account login fail:" + userParam.id_user + "-" + name, userParam);
                            return BadRequest(new { message = "account login fail" });
                           
                        }
                        else
                        {
                            _logger.LogInformation(LoggingEvents.GenerateItems, "-- Login success:" + userParam.id_user + "-" + name);
                            return Ok(user);
                        }
                        
                    }
                    else
                    {
                        _logger.LogInformation(LoggingEvents.GenerateItems, "-- Login success:" + userParam.id_user + "-" + name);
                        return Ok("Xác thực không thành");
                    }
                    
                }
                else
                {
                    _logger.LogInformation(LoggingEvents.GetItemNotFound, "Lỗi check token facebook", Mydata);
                    return Ok(Mydata);
                }
                
            }
            catch(Exception ex)
            {
                _logger.LogInformation(LoggingEvents.GetItemNotFound, ex, userParam.Username);
                if (ConsoleError) return NotFound(new { message= ex.ToString()});
                return NotFound(new { message= "Lỗi noại lệ" });
            }
            
        }
        
       
    }
}
