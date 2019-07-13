using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    public class FbFanpageController : Controller
    {
        db_facebook_vmContext XLDL = new db_facebook_vmContext();
        private IConfiguration configuration;
        private bool ConsoleError = false;
        #region Setting log 
        private readonly ILogRepository _logRepository;
        private readonly ILogger _logger;
        public FbFanpageController(ILogRepository logRepository, ILoggerFactory logger, IConfiguration iconfig)
        {
            _logRepository = logRepository;
            _logger = logger.CreateLogger("ApiCore_facebook.Controllers.FbFanpage");
            configuration = iconfig; //Lấy ra value file appseting.json
            ConsoleError = bool.Parse(configuration.GetSection("ConsoleError").Value);
        }
        #endregion


        [Route("Check_active_page"), HttpGet]
        [ApiExplorerSettings(GroupName = "get")]
        [ProducesResponseType(typeof(pro_getUsser), 200)]
        [ProducesResponseType(404)]
        [ResponseCache(Duration = 86400)]
        public async Task<ActionResult> Check_active_page([FromQuery]  Form_FbFanpage.In_check_active_page pram)
        {
            try
            {
                var query_user_token = await XLDL.FbPageDetail.AsNoTracking().Select(x=> new {x.Quyen, x.IdUser, x.IdPage }).FirstOrDefaultAsync(x => x.IdUser == pram.id_user && x.IdPage == pram.id_page);

                //if(query_user_token!=null&& (XLDL.FBFanpage.AsNoTracking().Any(x=>x.id== pram.id_page && x.su)))
                
               
                //_logger.LogInformation(LoggingEvents.GetItem, "Get user (_pro_getUsser)", pram);
                //if (query != null) return Ok(query);
                return  NoContent();//400

            }
            catch (Exception ex)
            {
                _logger.LogWarning(LoggingEvents.GetItemNotFound, ex, "");
                if (ConsoleError) return NotFound(ex);
                return NotFound("Lỗi ngoại lệ");//401
            }

        }



        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
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
