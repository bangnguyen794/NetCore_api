using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
    public class FbMessage : Controller
    {
        db_facebook_vmContext ctx = new db_facebook_vmContext();
        private IConfiguration configuration;
        private bool ConsoleError = false;
        
        #region Setting log 
        private readonly ILogRepository _logRepository;
        private readonly ILogger _logger;
        public FbMessage(ILogRepository logRepository, ILoggerFactory logger, IConfiguration iconfig)
        {
            _logRepository = logRepository;
            _logger = logger.CreateLogger("Fb_message.Controllers.Fb_message");
            configuration = iconfig; //Lấy ra value file appseting.json
            ConsoleError = bool.Parse(configuration.GetSection("ConsoleError").Value);
        }
        #endregion
        /// <summary>
        /// ( Thêm - cập nhập tin nhắn publish online )
        /// </summary>
        /// <param name="body">Giá trị truyền vào</param>
        /// <returns></returns>
        /// 
        /// <remarks>Mô tả ghi chú!</remarks>
        /// <response code="400">Product has missing/invalid values</response>
        /// <response code="500">Oops! Can't create your product right now</response>
       
        [Route("SaveChangeMessage"), HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [ProducesResponseType(500)]
        [ApiExplorerSettings(GroupName = "post")]
        public async Task<ActionResult> Save_change_message([FromBody]  CL_FbMessages.From_add_message body)
        {
            try
            {
                await ctx.Database.ExecuteSqlCommandAsync($"exec dbo.Pro_UpdateInsertMessages @id_message={body.id_message}, @id_page={ body.id_page}, @id_user={ body.id_user}, @message={ body.message}, @name_user={ body.name_user}, @views={ body.views}, @views_update={ body.views_update}, @update_time={ body.update_time}, @type={ body.type}, @phone={body.phone}");

                _logger.LogInformation(LoggingEvents.InsertItem, "Add mesage :{ body}", body);
                //var query= await ctx.FbMessages.FromSql($"exec seach_name_fb @name =N{body.name_user}").ToListAsync();
                //_logger.LogWarning(LoggingEvents.GetItemNotFound, "GetById({ID}) NOT FOUND", id);
                //return NotFound();
                return Ok("success");
            }
            catch (Exception ex)
            {
                _logger.LogWarning(LoggingEvents.GetItemNotFound, ex, body.ToString());
                if(ConsoleError) return NotFound(ex);
                return NotFound("Lỗi ngoại lệ");

            }
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
        [ApiExplorerSettings(GroupName = "post")]
        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
