using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ApiCore_facebook.ClassController.log;
using ApiCore_facebook.Library;
using ApiCore_facebook.Models;

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
        db_facebook_vmContext XLDL = new db_facebook_vmContext();
        private IUserService _userService;
        private IConfiguration configuration;
        private bool ConsoleError = false;

        private readonly ILogRepository _logRepository;
        private readonly ILogger _logger;
        private readonly IMemoryCache _cache;
        public SettingController(IUserService userService, ILogRepository logRepository, ILoggerFactory logger, IConfiguration iconfig, IMemoryCache memoryCache)
        {
            _userService = userService;
            _logRepository = logRepository;
            _logger = logger.CreateLogger("ApiCore_facebook.Controllers.Setting");
            configuration = iconfig; //Lấy ra value file appseting.json
            ConsoleError = bool.Parse(configuration.GetSection("ConsoleError").Value);
            _cache = memoryCache; //Bộ nhớ đệm server
        }

        public class BodyGetSetting
        {
            [Required]
            public string token { get; set; }

        }
        /// <summary>
        /// Lấy ra cấu hình bảo trì, check xem tải khoản được add full quyền không
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        // GET: api/<controller>
        [Route("GetSetting"), HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> GetSetting([FromBody] BodyGetSetting body)
        {

            try
            {
                if (body.token == "eyJ1bmlxdWVfbmFtZSI6IjIiLCJyb2xlIjoiVXNlciIsIm5iZiI6MTU2Mjg5Njk1OSwiZXhwIjoxNTYzNTAxNzU5LCJpYXQiOjE1NjI4OTY5NTl9")
                {
                    string keyCache = "ca_getSetting";
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
                    return BadRequest(new { message = "Error token" });
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