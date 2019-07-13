using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCore_facebook.Models
{
    public class FBFanpage
    {
        public string id { get; set; }
        public bool? active { get; set; }
        public string name { get; set; }
        public DateTime? syn_time { get; set; }
        public DateTime? active_time { get; set; }
        public string active_by { get; set; }
        public string active_name_by { get; set; }
        public string banefit { get; set; }
        public bool? subscribed_apps { get; set; }
        public string access_token { get; set; }
        public bool? hide_cmt { get; set; }
        public bool? like_cmt { get; set; }
        public bool? sync { get; set; }
        public string account_acctive { get; set; }
        public DateTime? ngay_hethan { get; set; }
        public string app_id { get; set; }
    }
}
