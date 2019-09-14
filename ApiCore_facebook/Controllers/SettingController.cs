using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using ApiCore_facebook.ClassController.log;
using ApiCore_facebook.Library;
using ApiCore_facebook.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiCore_facebook.Controllers
{
    
    [Authorize]
    [ApiVersionNeutral]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class SettingController : ControllerBase
    {
        db_facebookContext XLDL = new db_facebookContext();
       
        private IConfiguration configuration;
        private bool ConsoleError = false; //Trạng thái muốn thông báo chi tiết lỗi không.
        private bool AllowPosman = false;// cho paosmand gọi dữ liêu
        private string AllowRequest = ""; //Trang web được lấy dữ liệu..
        private readonly ILogRepository _logRepository;
        private readonly ILogger _logger;
        private readonly IMemoryCache _cache;
        public SettingController(ILogRepository logRepository, ILoggerFactory logger, IConfiguration iconfig, IMemoryCache memoryCache)
        {
            _logRepository = logRepository;
            _logger = logger.CreateLogger("ApiCore_facebook.Controllers.Setting");
            configuration = iconfig; //Lấy ra value file appseting.json
           
            AllowRequest = configuration.GetSection("AllowRequest").Value;
            AllowPosman = bool.Parse(configuration.GetSection("AllowPosman").Value);
            _cache = memoryCache; //Bộ nhớ đệm server
        }
        private string  GetAuthorization(string key)
        {
            var accessToken = HttpContext.Request.Headers["Authorization"][0].Replace("Bearer ", "").Trim();
            //var authenticateInfo = await HttpContext.Authentication.GetAuthenticateInfoAsync("Bearer");
            //var  stream = authenticateInfo.Properties.Items[".Token.access_token"];
            var handler = new JwtSecurityTokenHandler();
            //var jsonToken = handler.ReadToken(accessToken);
            var tokenS = handler.ReadToken(accessToken) as JwtSecurityToken;
            var rs = tokenS.Claims.First(claim => claim.Type == key).Value; //unique_name,role(quyen),groupsid (Id page sở hữu)
            return rs;
        }
        private bool Check_request()
        {
            bool request = false;
            if (HttpContext.Request.Headers.ContainsKey("Origin"))
            {
                var link_request = HttpContext.Request.Headers["Origin"][0];
                //Kiểm tra  trùng AllowRequest 
                if (AllowRequest.Contains(link_request))
                {
                    request = true;
                }
                else
                {
                    request = false;
                }
            }
            else
            {
                request = AllowPosman;

            }
            return request;
        }



        public class BodyGetSetting
        {
            [Required]
            public string token { get; set; }
            public bool refest_cahe { get; set; }
        }
        /// <summary>
        /// Lấy ra cấu hình bảo trì, check xem tải khoản được add full quyền không
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        // GET: api/<controller>
        [Route("GetSetting"), HttpPost]
        [AllowAnonymous]
        //[Authorize(Roles = Role.Admin)]
        public async Task<ActionResult> GetSetting([FromBody] BodyGetSetting body)
        {
          
            //var name = GetAuthorization("unique_name");
            try
            {
                if(Check_request())
                {
                    if (body.token == "eyJ1bmlxdWVfbmFtZSI6IjIiLCJyb2xlIjoiVXNlciIsIm5iZiI6MTU2Mjg5Njk1OSwiZXhwIjoxNTYzNTAxNzU5LCJpYXQiOjE1NjI4OTY5NTl9")
                    {
                        string keyCache = "ca_getSetting";
                        if (body.refest_cahe) _cache.Remove("ca_getSetting"); // if = true .. load mới 
                        if (!_cache.TryGetValue(keyCache, out var query))
                        {
                            query = await XLDL.FbSetting.AsNoTracking().Select(s => new { s.Baotri, s.FullQuyen }).Take(1).FirstOrDefaultAsync();
                            var cacheEntryOptions = new MemoryCacheEntryOptions()
                            .SetSlidingExpiration(TimeSpan.FromDays(365));
                            _cache.Set(keyCache, query, cacheEntryOptions);
                            return Ok(query);
                        }
                        else
                        {
                            return Ok(_cache.Get(keyCache));
                        }
                    }
                    else
                    {
                        return BadRequest(new { message = "Lỗi chưa xác định" });
                    }
                }
                else
                {
                    return NotFound();
                }



            }
            catch (Exception ex)
            {
                _logger.LogInformation(LoggingEvents.GetItemNotFound, ex, body.token);
                if (ConsoleError) return NotFound(new { message = ex.ToString() });
                return NotFound(new { message = "Lỗi noại lệ" });
            }

        }
        
        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
        

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

    }
}