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
        db_facebook_vmContext ctx = new db_facebook_vmContext();
        private IConfiguration configuration;
        private bool ConsoleError = false;
        #region Setting log 
        private readonly ILogRepository _logRepository;
        private readonly ILogger _logger;
        public FbUserToken(ILogRepository logRepository, ILoggerFactory logger, IConfiguration iconfig)
        {
            _logRepository = logRepository;
            _logger = logger.CreateLogger("FbUserToken.Controllers.FbUserToken");
            configuration = iconfig; //Lấy ra value file appseting.json
            ConsoleError = bool.Parse(configuration.GetSection("ConsoleError").Value);
        }
        #endregion
        
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
                var query = await ctx.pro_getUsser.AsNoTracking().FromSql($"Exec [dbo].[_pro_getUsser] @id_user = {pram.id_user},@app_id  = {pram.app_id}").Take(1).FirstOrDefaultAsync();
                //var query_setting = await ctx.FbSetting.AsNoTracking().Select(s => s.Baotri).FirstOrDefaultAsync();
                _logger.LogInformation(LoggingEvents.GetItem, "Get user (_pro_getUsser)", pram);
                if (query != null) return Ok(query);
                return NoContent();//400

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
