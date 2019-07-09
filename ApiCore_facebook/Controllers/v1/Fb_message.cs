using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ApiCore_facebook.ClassController.v1;
using ApiCore_facebook.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiCore_facebook.Controllers.v1
{

    //[Authorize] // check token
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    [EnableCors("AllowOrigin")]
    public class Fb_message : Controller
    {

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
                await ctx.Database.ExecuteSqlCommandAsync($"exec Pro0_UpdateInsertMessages @id_message={body.id_message}, @id_page={ body.id_page}, @id_user={ body.id_user}, @message={ body.message}, @name_user={ body.name_user}, @views={ body.views}, @views_update={ body.views_update}, @update_time={ body.update_time}, @type={ body.type}, @phone={ body.phone}");
                //var query= await ctx.FbMessages.FromSql($"exec seach_name_fb @name =N{body.name_user}").ToListAsync();
                return Ok("success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
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
