using System;
using System.Collections.Generic;

namespace ApiCore_facebook.Models
{
    public partial class FbSetting
    {
        public int Id { get; set; }
        public bool? SvMsText { get; set; }
        public string GuimailLoi { get; set; }
        public bool? Baotri { get; set; }
        public bool? SaveEtimedout { get; set; }
        public string STrang { get; set; }
        public string SNhanvien { get; set; }
        public string SThang { get; set; }
        public string EmailNhanThanhtoan { get; set; }
        public string FullQuyen { get; set; }
    }
}
