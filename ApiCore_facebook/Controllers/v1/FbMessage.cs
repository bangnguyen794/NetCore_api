using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
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
        private IUserService _userService;
        private IConfiguration configuration;
        private bool ConsoleError = false; //Trạng thái muốn thông báo chi tiết lỗi không.
        private bool AllowPosman = false;// cho paosmand gọi dữ liêu
        private string AllowRequest = ""; //Trang web được lấy dữ liệu..
        private readonly ILogRepository _logRepository;
        private readonly ILogger _logger;
        private readonly IMemoryCache _cache;

        #region Setting log 
        public FbMessage(IUserService userService, ILogRepository logRepository, ILoggerFactory logger, IConfiguration iconfig, IMemoryCache memoryCache)
        {
            _logRepository = logRepository;
            _userService = userService;
            _logger = logger.CreateLogger("ApiCore_facebook.Controllers.FbMessage");
            configuration = iconfig; //Lấy ra value file appseting.json
            AllowRequest = configuration.GetSection("AllowRequest").Value;
            AllowPosman = bool.Parse(configuration.GetSection("AllowPosman").Value);
            _cache = memoryCache; //Bộ nhớ đệm server
          
           
        }
        private string GetAuthorization(string key)
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
                await XLDL.Database.ExecuteSqlCommandAsync($"exec _pro_UpdateInsert_Psid_message @id={body.id}, @id_page={ body.id_page }, @id_user ={ body.id_user }, @name_user ={ body.name_user }");
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
                
                if (Check_request())
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
                else
                {
                    return NotFound();
                }
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
                if (Check_request())
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
                else
                {
                    return BadRequest();
                }
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


        #region Cập nhập trạng thái
        public class body_UpdateStatus
        {
            public string account { get; set; }
            public string id_message { get; set; }
            public string id_page { get; set; }
            public string id_user { get; set; }
            public string trangthai { get; set; }
        }
        /// <summary>
        /// Cập nhập trạng thái tin nhắn  
        /// </summary>
        /// <param name="body"> trả về Object</param>
        /// <returns></returns>
        /// <example>
        /// 
        ///     <code>
        ///     {
        ///  "additionalProp1": "string",
        ///  "additionalProp2": "string",
        ///  "additionalProp3": "string"
        ///}
        ///     </code>
        /// </example>
        //[ApiExplorerSettings(IgnoreApi = true)] an document
        // PUT api/<controller>/5

        [Route("Uptrangthai"), HttpPost]
        public async  Task<ActionResult> UpdateStatus([FromBody]  body_UpdateStatus body)
        {
            try
            {
                var group_idPage = GetAuthorization("groupsid");
                if (Check_request())
                {
                    string account= _Extensions.NullToString(body.account), id_message= _Extensions.NullToString(body.id_message), id_page= _Extensions.NullToString(body.id_page), id_user= _Extensions.NullToString(body.id_user), trangthai= _Extensions.NullToString(body.trangthai);

                    //string account="", id_message="", id_page="", id_user="", trangthai="";
                    //if (body.ContainsKey("account")) account = body["account"].ToString().Trim();
                    //if (body.ContainsKey("id_message")) id_message = body["id_message"].ToString().Trim();
                    //if (body.ContainsKey("id_page")) id_page = body["id_page"].ToString().Trim();
                    //if (body.ContainsKey("id_user")) id_user = body["id_user"].ToString().Trim();
                    //if (body.ContainsKey("trangthai")) trangthai = body["trangthai"].ToString().Trim();

                    if (!group_idPage.Contains(id_page)) return Ok(new { success = false, message = "Bạn không có quyền cập nhập trang này" });

                    var query = await XLDL.FbMessages.FirstOrDefaultAsync(x => x.Id == id_message && x.IdUser == id_user && x.IdPage == id_page);

                    if (query != null)
                    {
                        query.Advisory = false;
                        query.Khongmuahang = false;
                        query.Khongphanhoi = false;
                        query.Cotuongtac = false;
                        query.Tuongtactot = false;
                        query.Damuahang = false;
                        query.Boqua = false;
                        query.ThoigianTuongtac = DateTime.Now;
                        if (trangthai == "advisory" || trangthai == "") query.Advisory = true;
                        if (trangthai == "khongmuahang") query.Khongmuahang = true;
                        if (trangthai == "khongphanhoi") query.Khongphanhoi = true;
                        if (trangthai == "cotuongtac") query.Cotuongtac = true;
                        if (trangthai == "tuongtactot") query.Tuongtactot = true;
                        if (trangthai == "damuahang") query.Damuahang = true;
                        if (trangthai == "boqua") query.Boqua = true;
                        XLDL.FbMessages.Update(query);
                        await XLDL.SaveChangesAsync();
                        return Ok(new { success = true, message = "Cập nhâp thành công" });
                    }
                    return Ok(new { success = false, message = "Không tìm thấy item" });
                }
                else
                {
                    return BadRequest();
                }

            }
            catch (Exception ex)
            {
                _logger.LogWarning(LoggingEvents.GetItemNotFound, ex, "");
                if (ConsoleError) return NotFound(ex);
                return NotFound("Lỗi ngoại lệ");
            }
        }
        #endregion



        [ApiExplorerSettings(GroupName = "delete",IgnoreApi =true)]
        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
