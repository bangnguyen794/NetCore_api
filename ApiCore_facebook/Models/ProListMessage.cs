using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCore_facebook.Models
{
    public class ProListMessage
    {
        [Key]
        public string id_message { get; set; }
        public string id_page { get; set; }
        public string id_user { get; set; }
        public string message { get; set; }
        public string name_user { get; set; }
        public bool? views { get; set; }
        public bool? views_update { get; set; }
        public bool? xong { get; set; }
        public DateTime? update_time { get; set; }
        public string type { get; set; }
        public bool? advisory { get; set; }
        public bool? khongmuahang { get; set; }
        public bool? khongphanhoi { get; set; }
        public bool? cotuongtac { get; set; }
        public bool? tuongtactot { get; set; }
        public bool? damuahang { get; set; }
        public string phone { get; set; }
        public bool daxong { get; set; }
        public string ghichu { get; set; }
        public string tag_detail { get; set; }
       
    }
}
