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
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiCore_facebook.Controllers.v1
{

    [Authorize]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    [EnableCors("AllowOrigin")]

    public class Fb_message : Controller
    {
        #region Setting log 
        private readonly ILogRepository _logRepository;
        private readonly ILogger _logger;
        public Fb_message(ILogRepository logRepository, ILoggerFactory logger)
        {
            _logRepository = logRepository;
            _logger = logger.CreateLogger("Fb_message.Controllers.Fb_message"); ;
        }
        #endregion
        db_facebook_vmContext ctx = new db_facebook_vmContext();

        

        /// <summary>
        /// ( Thêm - cập nhập ti nhắn )
        /// </summary>
        /// <param name="body">Giá trị truyền vào</param>
        /// <returns></returns>
        [Route("Save_change_message"), HttpPost]
        public async Task<ActionResult> Save_change_message([FromBody]  CL_FbMessages.From_add_message body)
        {
           
            try
            {
                await ctx.Database.ExecuteSqlCommandAsync($"exec Pro_UpdateInsertMessages @id_message={body.id_message}, @id_page={ body.id_page}, @id_user={ body.id_user}, @message={ body.message}, @name_user={ body.name_user}, @views={ body.views}, @views_update={ body.views_update}, @update_time={ body.update_time}, @type={ body.type}, @phone={ body.phone}");

                _logger.LogInformation(LoggingEvents.InsertItem, "Add mesage :{ body.id_user}", body);
                //var query= await ctx.FbMessages.FromSql($"exec seach_name_fb @name =N{body.name_user}").ToListAsync();
                //_logger.LogWarning(LoggingEvents.GetItemNotFound, "GetById({ID}) NOT FOUND", id);
                //return NotFound();
                //_logger.LogInformation(LoggingEvents.ListItems, "Listing all items"); Lấy ra list item
                //_logger.LogInformation(LoggingEvents.InsertItem, "Item {ID} Created", item.Key); //Thêm 1 item
                //_logger.LogInformation(LoggingEvents.UpdateItem, "Item {ID} Updated", item.Key); cập nhập 1 item
                //_logger.LogInformation(LoggingEvents.DeleteItem, "Item {ID} Deleted", id);Xóa 1 item
                //_logger.LogInformation(LoggingEvents.GenerateItems, "Generating sample items."); tạo một danh sách item
                //_logger.LogInformation(LoggingEvents.GetItem, "Getting item {ID}", id); lấy ra 1 item
                //_logger.LogWarning(LoggingEvents.GetItemNotFound, ex, "GetById({ID}) NOT FOUND", id); trả về lỗi ngoại lệ

                //Tạo list bên trong nếu đã tồn tại
                //using (_logger.BeginScope("Message {HoleValue}", DateTime.Now))
                //{
                //    _logger.LogInformation(LoggingEvents.ListItems, "Listing all items");
                //    EnsureItems();
                //}
                //_logger.BeginScope("Message attached to logs created in the using block") Thông báo đính kèm với nhật ký được tạo trong khối sử dụng

                return Ok("success");
            }
            catch (Exception ex)
            {
                _logger.LogWarning(LoggingEvents.GetItemNotFound, ex, body.ToString());
                return NotFound(ex);
              
            }
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
