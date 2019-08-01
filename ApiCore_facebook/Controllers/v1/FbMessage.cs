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
    public class FbMessage : Controller
    {
        db_facebookContext XLDL = new db_facebookContext();
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
        /// ( Thêm - cập nhập tin nhắn new)
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
        [AllowAnonymous]
        public async Task<ActionResult> Save_change_message([FromBody]  CL_FbMessages.From_add_message body)
        {
            try
            {
                await XLDL.Database.ExecuteSqlCommandAsync($"exec dbo.Pro_UpdateInsertMessages @id_message={body.id_message}, @id_page={ body.id_page}, @id_user={ body.id_user}, @message={ body.message}, @name_user={ body.name_user}, @views={ body.views}, @views_update={ body.views_update}, @update_time={ body.update_time}, @type={ body.type}, @phone={body.phone}");
                //_logger.LogInformation(LoggingEvents.InsertItem, "Add mesage :{ body}", body);
                //var query= await XLDL.FbMessages.FromSql($"exec seach_name_fb @name ={body.name_user}").ToListAsync();
                //_logger.LogWarning(LoggingEvents.GetItemNotFound, "GetById({ID}) NOT FOUND", id);
                //return NotFound();
                return Ok(new { success = true, message = "Add messsage new success " });
            }
            catch (Exception ex)
            {
                _logger.LogWarning(LoggingEvents.GetItemNotFound, ex, body.ToString());
                if(ConsoleError) return NotFound(ex);
                return NotFound("Lỗi ngoại lệ");
            }
        }
        /// <summary>
        /// ( Thêm - cập nhập tin nhắn psid)
        /// </summary>
        /// <param name="body">Giá trị truyền vào</param>
        /// <returns></returns>
        /// 
        /// <remarks>Mô tả ghi chú!</remarks>
        /// <response code="400">Product has missing/invalid values</response>
        /// <response code="500">Oops! Can't create your product right now</response>

        [Route("SaveChangeMessagePsid"), HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Save_change_message_psid([FromBody]  CL_FbMessages.BodyListMessagePsid body)
        {
            try
            {
                await XLDL.Database.ExecuteSqlCommandAsync($"exec _pro_UpdateInsert_Psid_message @id={body.id}, @id_page={ body.id_page }, @id_user ={ body.id_user }, @name_user ={ body.name_user }, @update_time = { body.update_time }");
                return Ok(new { success=true, message="Add messsage psid success "});
            }
            catch (Exception ex)
            {
                _logger.LogWarning(LoggingEvents.GetItemNotFound, ex, body.ToString());
                if (ConsoleError) return NotFound(ex);
                return NotFound("Lỗi ngoại lệ");

            }
        }
        /// <summary>
        /// Load Danh sách tin nhắn
        /// </summary>
        /// <param name="pram"></param>
        /// <returns></returns>
        // GET api/<controller>/5
        [Route("ListMessage"), HttpGet()]
        [ResponseCache(Duration = 10)]
        public async Task<IActionResult> ListMessage([FromQuery] CL_FbMessages.BodyListMessage pram)
        {
            try
            {

                if (pram.check_daxong_con == null)
                { pram.check_daxong_con = ""; }
                if (pram.key == null)
                { pram.key = ""; }
                if (pram.option_tag == null)
                { pram.option_tag = ""; }

                string[] array_page = pram.id_page.Split('_');
                string str_or_page_mg = "";
                if (array_page.Count() > 1)
                {
                    foreach (var e in array_page)
                    {
                        str_or_page_mg += "'" + e + "',";
                    }
                }
                else
                {
                    str_or_page_mg = "'" + pram.id_page + "'";

                }

                if (pram.check_ngay == "false")
                {
                    pram.ngaybatdau = DateTime.Parse(DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd") + " 00:00:00.000");
                    pram.ngayketthuc = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59.000");

                }
                else if (pram.check_ngay == "true")
                {
                    pram.ngaybatdau = DateTime.Parse(DateTime.Parse(pram.ngaybatdau.ToString()).ToString("yyyy-MM-dd HH:mm:00.000"));
                    pram.ngayketthuc = DateTime.Parse(DateTime.Parse(pram.ngayketthuc.ToString()).ToString("yyyy-MM-dd HH:mm:59.000"));

                    if (DateTime.Parse(pram.ngayketthuc.ToString()).ToString("HH") == "00")
                    {
                        pram.ngayketthuc = DateTime.Parse(DateTime.Parse(pram.ngayketthuc.ToString()).ToString("yyyy-MM-dd 23:59:59.000"));
                    }
                }


                if (pram.key != "")
                {
                    pram.ngaybatdau = DateTime.Parse(DateTime.Now.AddMonths(-4).ToString("yyyy-MM-dd") + " 00:00:00.000");
                    pram.ngayketthuc = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59.000");
                }
                if (pram.check_chogui == "true" || pram.option_tab == "lichhen")
                {
                    if (pram.check_ngay == "false")
                    {
                        pram.ngaybatdau = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));
                        pram.ngayketthuc = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));
                    }
                    else if (pram.check_ngay == "true")
                    {
                        pram.ngaybatdau = DateTime.Parse(pram.ngaybatdau + ":00.000");
                        pram.ngayketthuc = DateTime.Parse(pram.ngayketthuc + ":59.000");

                        if (DateTime.Parse(pram.ngayketthuc + ":59.000").ToString("HH") == "00")
                        {
                            pram.ngayketthuc = DateTime.Parse(pram.ngayketthuc + " 23:59:59.000");
                        }
                    }
                }
                var query = await XLDL.ProListMessage.FromSql($"View_list_message_new  @id_page = {str_or_page_mg.Trim().TrimEnd(',')}, @page_index ={pram.page_index}, @row_item ={pram.take}, @option_tab ={pram.option_tab}, @option_tag ={pram.option_tag.ToString().Trim().TrimEnd(',')}, @check_ngay ={pram.check_ngay}, @check_daxong = {pram.check_daxong.ToString()}, @check_chuaxong={pram.check_chuaxong}, @check_daxong_con = {pram.check_daxong_con.ToString()}, @check_cophone = {pram.check_cophone}, @check_khongphone = {pram.check_khongphone}, @check_chuatag = {pram.check_chuatag}, @ngaybatdau = {pram.ngaybatdau}, @ngayketthuc ={pram.ngayketthuc}, @key = {pram.key.ToString()}, @style_key ={pram.style_key}, @check_chogui ={pram.check_chogui} ").AsNoTracking().ToListAsync();
                List<object> Result_item = new List<object>();
                foreach (var row in query)
                {
                   
                    //string tag_detail = row.tag_detail.ToString();
                    JArray new_tag_detail = JArray.Parse("[" + row.tag_detail+ "]");
                    string update_time_fm = _Extensions.datetime_parse(DateTime.Parse(row.update_time.ToString()));
                    Result_item.Add(new
                    {
                        id_message = row.id_message.ToString(),
                        id_page = row.id_page.ToString(),
                        id_user = row.id_user.ToString(),
                        message = row.message.ToString(),
                        ghichu = row.ghichu,
                        xong = row.xong,
                        name_user = row.name_user.ToString(),
                        views = row.views,
                        views_update = row.views_update,
                        update_time = DateTime.Parse(row.update_time.ToString()).ToString("yyyymmddhhmmss"),
                        update_time_format = update_time_fm,
                        type = row.type,
                        advisory = row.advisory,
                        khongmuahang = row.khongmuahang,
                        khongphanhoi = row.khongphanhoi,
                        cotuongtac = row.cotuongtac,
                        tuongtactot = row.tuongtactot,
                        damuahang = row.damuahang,
                        phone = row.phone.ToString(),
                        daxong = bool.Parse(row.daxong.ToString()),
                        tag = new_tag_detail,
                    });
                }
                return Ok(Result_item);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(LoggingEvents.GetItemNotFound, ex, pram.ToString());
                if (ConsoleError) return NotFound(ex);
                return NotFound("Lỗi ngoại lệ");

            }
           
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("ListMessageKhac"), HttpGet()]
        [ResponseCache(Duration =120)] //caching 2 phút
        public async Task<IActionResult> ListMessageKhac([FromQuery] CL_FbMessages.BodyListMessage pram)
        {
            try
            {
                if (pram.check_daxong_con == null)
                { pram.check_daxong_con = ""; }
                if (pram.key == null)
                { pram.key = ""; }
                if (pram.option_tag == null)
                { pram.option_tag = ""; }

                string[] array_page = pram.id_page.Split('_');
                string str_or_page_mg = "";
                if (array_page.Count() > 1)
                {
                    foreach (var e in array_page)
                    {
                        str_or_page_mg += "'" + e + "',";
                    }
                }
                else
                {
                    str_or_page_mg = "'" + pram.id_page + "'";

                }

                if (pram.check_ngay == "false")
                {
                    pram.ngaybatdau = DateTime.Parse(DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd") + " 00:00:00.000");
                    pram.ngayketthuc = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59.000");

                }
                else if (pram.check_ngay == "true")
                {
                    pram.ngaybatdau = DateTime.Parse(DateTime.Parse(pram.ngaybatdau.ToString()).ToString("yyyy-MM-dd HH:mm:00.000"));
                    pram.ngayketthuc = DateTime.Parse(DateTime.Parse(pram.ngayketthuc.ToString()).ToString("yyyy-MM-dd HH:mm:59.000"));

                    if (DateTime.Parse(pram.ngayketthuc.ToString()).ToString("HH") == "00")
                    {
                        pram.ngayketthuc = DateTime.Parse(DateTime.Parse(pram.ngayketthuc.ToString()).ToString("yyyy-MM-dd 23:59:59.000"));
                    }
                }


                if (pram.key != "")
                {
                    pram.ngaybatdau = DateTime.Parse(DateTime.Now.AddMonths(-4).ToString("yyyy-MM-dd") + " 00:00:00.000");
                    pram.ngayketthuc = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59.000");
                }
                if (pram.check_chogui == "true" || pram.option_tab == "lichhen")
                {
                    if (pram.check_ngay == "false")
                    {
                        pram.ngaybatdau = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));
                        pram.ngayketthuc = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));
                    }
                    else if (pram.check_ngay == "true")
                    {
                        pram.ngaybatdau = DateTime.Parse(pram.ngaybatdau + ":00.000");
                        pram.ngayketthuc = DateTime.Parse(pram.ngayketthuc + ":59.000");

                        if (DateTime.Parse(pram.ngayketthuc + ":59.000").ToString("HH") == "00")
                        {
                            pram.ngayketthuc = DateTime.Parse(pram.ngayketthuc + " 23:59:59.000");
                        }
                    }
                }
                var query = await XLDL.ProListMessage.FromSql($"View_list_message_new  @id_page = {str_or_page_mg.Trim().TrimEnd(',')}, @page_index ={pram.page_index}, @row_item ={pram.take}, @option_tab ={pram.option_tab}, @option_tag ={pram.option_tag.ToString().Trim().TrimEnd(',')}, @check_ngay ={pram.check_ngay}, @check_daxong = {pram.check_daxong.ToString()}, @check_chuaxong={pram.check_chuaxong}, @check_daxong_con = {pram.check_daxong_con.ToString()}, @check_cophone = {pram.check_cophone}, @check_khongphone = {pram.check_khongphone}, @check_chuatag = {pram.check_chuatag}, @ngaybatdau = {pram.ngaybatdau}, @ngayketthuc ={pram.ngayketthuc}, @key = {pram.key.ToString()}, @style_key ={pram.style_key}, @check_chogui ={pram.check_chogui} ").AsNoTracking().ToListAsync();
                List<object> Result_item = new List<object>();
                foreach (var row in query)
                {
                    JArray new_tag_detail = JArray.Parse("[" + row.tag_detail + "]");
                    string update_time_fm = _Extensions.datetime_parse(DateTime.Parse(row.update_time.ToString()));
                    Result_item.Add(new
                    {
                        id_message = row.id_message.ToString(),
                        id_page = row.id_page.ToString(),
                        id_user = row.id_user.ToString(),
                        message = row.message.ToString(),
                        ghichu = row.ghichu,
                        xong = row.xong,
                        name_user = row.name_user.ToString(),
                        views = row.views,
                        views_update = row.views_update,
                        update_time = DateTime.Parse(row.update_time.ToString()).ToString("yyyymmddhhmmss"),
                        update_time_format = update_time_fm,
                        type = row.type,
                        advisory = row.advisory,
                        khongmuahang = row.khongmuahang,
                        khongphanhoi = row.khongphanhoi,
                        cotuongtac = row.cotuongtac,
                        tuongtactot = row.tuongtactot,
                        damuahang = row.damuahang,
                        phone = row.phone.ToString(),
                        daxong = bool.Parse(row.daxong.ToString()),
                        tag = new_tag_detail,
                    });
                }
                return Ok(Result_item);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(LoggingEvents.GetItemNotFound, ex, pram.ToString());
                if (ConsoleError) return NotFound(ex);
                return NotFound("Lỗi ngoại lệ");

            }
        }

        /// <summary>
        /// Chi tiết người nhắn tin
        /// </summary>
        /// <param name="id_message"></param>
        /// <returns></returns>
        [Route("GetMsDetail/{id_message}"), HttpGet]
        [ResponseCache(Duration = 3600)]
        public async Task<IActionResult> GetMsDetail(string id_message)
        {
            try
            {
                var query_result = await XLDL.FbMessageDetail.AsNoTracking().Select(s => new { s.Id, s.IdMessage, s.SenderIdBy, s.SenderNameBy }).Where(w => w.IdMessage == id_message).ToListAsync();
                return Ok(query_result);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(LoggingEvents.GetItemNotFound, ex, "");
                if (ConsoleError) return NotFound(ex);
                return NotFound("Lỗi ngoại lệ");

            }
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        [ApiExplorerSettings(GroupName = "delete",IgnoreApi =true)]
        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
