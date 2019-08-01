using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ApiCore_facebook.ClassController.log;
using ApiCore_facebook.ClassController.v1;
using ApiCore_facebook.Library;
using ApiCore_facebook.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiCore_facebook.Controllers.v1
{

    [Authorize]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    [EnableCors("AllowOrigin")]
    public class FbTagController : Controller
    {
        db_facebookContext XLDL = new db_facebookContext();
        private IConfiguration configuration;
        private bool ConsoleError = false;

        #region Setting log 
        private readonly ILogRepository _logRepository;
        private readonly ILogger _logger;
        public FbTagController(ILogRepository logRepository, ILoggerFactory logger, IConfiguration iconfig)
        {
            _logRepository = logRepository;
            _logger = logger.CreateLogger("FbTagController.Controllers.FbTag");
            configuration = iconfig; //Lấy ra value file appseting.json
            ConsoleError = bool.Parse(configuration.GetSection("ConsoleError").Value);
        }
        #endregion
        // GET: api/<controller>
        /// <summary>
        /// Danh sách thẻ tag theo page tin nhắn
        /// </summary>
        /// <param name="id_page"></param>
        /// <param name="id_message"></param>
        /// <returns></returns>
        [Route("GetTagMs/{id_page}/{id_message}"), HttpGet]
        [ResponseCache(Duration =30)]
        public async Task<IActionResult> GetTagMs(string id_page, string id_message)
        {
            try
            {
                var query_tag = await XLDL._pro_GetTagMs.AsNoTracking().FromSql($"exec _pro_GetTagMs @id_page = {id_page},@id_message = {id_page}").ToListAsync();
                return Ok(query_tag);

            }catch(Exception ex)
            {
                _logger.LogWarning(LoggingEvents.GetItemNotFound, ex, "");
                if (ConsoleError) return NotFound(ex);
                return NotFound("Lỗi ngoại lệ");
            }
            
        }


        // GET: api/<controller>
        /// <summary>
        /// Danh sách thẻ tag theo page 
        /// </summary>
        /// <param name="id_page"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [Route("GetTagMsAll/{id_page}"), HttpGet]
        [ResponseCache(Duration = 86400)]
        public async Task<IActionResult> GetTagMsAll(string id_page)
        {
            try
            {
                var query_tag = await XLDL.FbTag.AsNoTracking().Where(w=> w.IdPage== id_page && w.Status==true).Select(s=>new{ s.Id,s.Title,s.Color }).ToListAsync();
                return Ok(query_tag);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(LoggingEvents.GetItemNotFound, ex, "");
                if (ConsoleError) return NotFound(ex);
                return NotFound("Lỗi ngoại lệ");
            }

        }
    }
}
