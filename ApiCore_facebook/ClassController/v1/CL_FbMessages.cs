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
            
        }
        public class PamListMessage
        {

            [Required]
            public string id_page { get; set; }
            /// <summary>
            /// Trang số 1,2, 3...
            /// </summary>
            [Required]
            public int page_index { get; set; }

            /// <summary>
            /// Số item lấy ra
            /// </summary>
            [Required]
            public int take { get; set; }
        }
        public class BodyListMessage
        {
            [Required]
            public string id_page { get; set; }
            /// <summary>
            /// Dafault = false
            /// </summary>
            public string check_chogui { get; set; }
            /// <summary>
            /// Dafault = false
            /// </summary>
            public string check_chuatag { get; set; }
            /// <summary>
            /// Dafault = false
            /// </summary>
            public string check_chuaxong { get; set; }
            /// <summary>
            /// Dafault = false
            /// </summary>
            public string check_cophone { get; set; }
            /// <summary>
            /// Dafault = false
            /// </summary>
            public string check_daxong { get; set; }
            /// <summary>
            /// Default = ""
            /// </summary>
            public string check_daxong_con { get; set; }

            /// <summary>
            /// Dafault = false
            /// </summary>
            public string check_khongphone { get; set; }
            /// <summary>
            /// Dafault = false
            /// </summary>
            public string check_ngay { get; set; }
            /// <summary>
            /// Giá trị phone - id, địa chỉ  ( có hoặc rỗng)
            /// </summary>
            public string key { get; set; }
           
            public DateTime? ngaybatdau { get; set; }

            public DateTime? ngayketthuc { get; set; }

            [Required]
            public string option_tab { get; set; }
            
            public string option_tag { get; set; }
            /// <summary>
            /// Default = "phone"
            /// </summary>
            public string style_key { get; set; }
            /// <summary>
            /// Số item lấy ra
            /// </summary>
            [Required]
            public int take { get; set; }
            /// <summary>
            /// Trang số 1,2,3 ...
            /// </summary>
            [Required]
            public int page_index { get; set; }
          
        }
    }
}
