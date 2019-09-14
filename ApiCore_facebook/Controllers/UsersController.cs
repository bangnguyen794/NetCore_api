using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using ApiCore_facebook.ClassController.log;
using ApiCore_facebook.Library;
using ApiCore_facebook.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
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

        db_facebookContext XLDL = new db_facebookContext();
        private IUserService _userService;
        private IConfiguration configuration;
        private bool ConsoleError = false; //Trạng thái muốn thông báo chi tiết lỗi không.
        private bool AllowPosman = false;// cho paosmand gọi dữ liêu
        private string AllowRequest = ""; //Trang web được lấy dữ liệu..
        private readonly ILogRepository _logRepository;
        private readonly ILogger _logger;
        private readonly IMemoryCache _cache;
        private  object GetAuthorization()
        {
            var accessToken = HttpContext.Request.Headers["Authorization"][0].Replace("Bearer ", "").Trim();
            //var authenticateInfo = await HttpContext.Authentication.GetAuthenticateInfoAsync("Bearer");
            //var  stream = authenticateInfo.Properties.Items[".Token.access_token"];
            var handler = new JwtSecurityTokenHandler();
            //var jsonToken = handler.ReadToken(accessToken);
            var tokenS = handler.ReadToken(accessToken) as JwtSecurityToken;
            return tokenS;
        }
        public UsersController(IUserService userService, ILogRepository logRepository, ILoggerFactory logger, IConfiguration iconfig,IMemoryCache memoryCache)
        {
           
            _userService = userService;
            _logRepository = logRepository;
            _logger = logger.CreateLogger("ApiCore_facebook.Controllers.Setting");
            configuration = iconfig; //Lấy ra value file appseting.json
            
            AllowRequest = configuration.GetSection("AllowRequest").Value;
            AllowPosman = bool.Parse(configuration.GetSection("AllowPosman").Value);
            _cache = memoryCache; //Bộ nhớ đệm server


        }

        /// <summary>
        /// Đang nhaaoj xác thực facebook
        /// </summary>
        /// <param name="userParam"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [Route("authenticate"), HttpPost]
        //[ResponseCache(Duration = 86400, VaryByHeader = "authenticate_login")]
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
