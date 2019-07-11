using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCore_facebook.ClassController.v1
{
    public class CL_FbMessages
    {
        /// <summary>
        /// Body lưu tin nhắn
        /// </summary>
      
        public class From_add_message
        {   
            [Required] 

            public string id_message { get; set; }
            [Required]
            public string id_page { get; set; }
            [Required]
            public string id_user { get; set; }
            [Required]
            public string message { get; set; }
            public string name_user { get; set; }
            [Required]
            public DateTime update_time { get; set; }
            public bool views { get; set; }
            /// <summary>
            /// Default = false
            /// </summary>
            /// <example>10</example>
            [Required]
            public bool views_update { get; set; }
            /// <summary>
            ///  ms (Tin nhắn) - cmt (Bình luận)
            /// </summary>
            [Required]
            public string type { get; set; }
            [MaxLength(13)]
            [MinLength(10)]
            public string phone { get; set; }
            ///// <summary>
            ///// Trang số 1,2, 3...
            ///// </summary>
            //[Required]
            //public int page_index { get; set; }
           
            ///// <summary>
            ///// Số item lấy ra
            ///// </summary>
            //[Required]
            //public int take { get; set; }
        }
    }
}
