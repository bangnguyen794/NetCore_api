using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiCore_facebook.ClassController.log;
using ApiCore_facebook.ClassController.v1;
using ApiCore_facebook.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiCore_facebook.Controllers.v1
{
    
    [Authorize]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    [EnableCors("AllowOrigin")]
    public class FbUserToken : Controller
    {
        db_facebookContext XLDL = new db_facebookContext();
        
        private IConfiguration configuration;
        private bool ConsoleError = false; //Trạng thái muốn thông báo chi tiết lỗi không.
        private bool AllowPosman = false;// cho paosmand gọi dữ liêu
        private string AllowRequest = ""; //Trang web được lấy dữ liệu..
        private readonly ILogRepository _logRepository;
        private readonly ILogger _logger;
        private readonly IMemoryCache _cache;
        public FbUserToken(ILogRepository logRepository, ILoggerFactory logger, IConfiguration iconfig,IMemoryCache memoryCache)
        {
            _logRepository = logRepository;
            configuration = iconfig; //Lấy ra value file appseting.json
            _logger = logger.CreateLogger("FbTagController.Controllers.FbUser");
            AllowRequest = configuration.GetSection("AllowRequest").Value;
            AllowPosman = bool.Parse(configuration.GetSection("AllowPosman").Value);
            _cache = memoryCache; //Bộ nhớ đệm server
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


        /// <summary>
        /// Lấy thông tin user
        /// </summary>
        /// <param name="pram"></param>
        /// <returns></returns>
        // GET api/<controller>/5 string:alpha
        /// <response code="404">Trả về lỗi ngoại lệ</response>

        [Route("GetUser"),HttpGet]
        [ApiExplorerSettings(GroupName = "get")]
        [ProducesResponseType(typeof(pro_getUsser), 200)]
        [ProducesResponseType(404)]
        //[ResponseCache(Duration = 86400)]
        public async Task<ActionResult> GetUser( [FromQuery]  Form_getUser.In_getUser pram)
        {
            try
            {
                if (Check_request())
                {
                    var query = await XLDL.pro_getUsser.AsNoTracking().FromSql($"Exec [dbo].[_pro_getUsser] @id_user = {pram.id_user},@app_id  = {pram.app_id}").Take(1).FirstOrDefaultAsync();
                    //var query_setting = await ctx.FbSetting.AsNoTracking().Select(s => s.Baotri).FirstOrDefaultAsync();
                    _logger.LogInformation(LoggingEvents.GetItem, "Get user (_pro_getUsser)", pram);
                    if (query != null) return Ok(query);
                    return NoContent();//404
                }
                else
                {
                    return BadRequest();//400
                }



            }
            catch (Exception ex)
            {
                _logger.LogWarning(LoggingEvents.GetItemNotFound, ex,"");
                if (ConsoleError) return NotFound(ex);
                return NotFound("Lỗi ngoại lệ");//401
            }
           
        }
        [ApiExplorerSettings(GroupName ="pub", IgnoreApi =true)]
        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        [ApiExplorerSettings(GroupName = "delete", IgnoreApi = true)]
        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
