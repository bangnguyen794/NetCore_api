using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCore_facebook.ClassController.v1
{
    public class Form_getUser
    {
        /// <summary>
        /// pram lấy user
        /// </summary>
        public class In_getUser
        {
            [Required]
            public string id_user { get; set; }
            [Required]
            public string app_id { get; set; }
        }
        
    }
}
