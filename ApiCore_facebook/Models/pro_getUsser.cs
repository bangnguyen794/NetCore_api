using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCore_facebook.Models
{
    public class pro_getUsser
    {
        public int id { get; set; }
        public string id_user { get; set; }
        public string name_user { get; set; }
        public string email { get; set; }
        public DateTime? active_time { get; set; }
        public string link_connect { get; set; }
        public string api_connect { get; set; }
        public bool check_connect { get; set; }
        public bool hienthi_tentag { get; set; }
        public bool bat_nhieutag { get; set; }
        public bool loc_nhieutag { get; set; }
        public bool typing { get; set; }
        public bool click_right_tag { get; set; }
        public bool save_infor_hopthoai { get; set; }
        public string page { get; set; }
        public string page_active { get; set; }
        public string access_token { get; set; }
        public int sotrang { get; set; }
        public int songuoi { get; set; }
        public bool? taodon { get; set; }
        public int gioihan_gui { get; set; }
        public bool? tuvan { get; set; }
        public bool? chamsoc_kh { get; set; }
        public bool? tim_sdt { get; set; }
        public bool? goptrang { get; set; }
        public bool? tim_kh { get; set; }
        public bool? baotri { get; set; }

    }
}
