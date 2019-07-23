using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCore_facebook.ClassController.v1
{
    public class Form_FbFanpage
    {
        public class In_check_active_page
        {

            [Required]
            public string IdUser { get; set; }
            [Required]
            public string IdPage { get; set; }
            [Required]
            public string AppId { get; set; }
        }
    }
}
