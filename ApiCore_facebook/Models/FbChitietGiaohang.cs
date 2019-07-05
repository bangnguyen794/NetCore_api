using System;
using System.Collections.Generic;

namespace ApiCore_facebook.Models
{
    public partial class FbChitietGiaohang
    {
        public int Id { get; set; }
        public string Mahd { get; set; }
        public string Magiaohang { get; set; }
        public string Lydo { get; set; }
        public int? TrangthaiVanchuyen { get; set; }
        public int? TrangthaiDonhang { get; set; }
        public int? ReasonCode { get; set; }
        public string Reason { get; set; }
        public string Ghichu { get; set; }
        public DateTime? ThoigianCapnhap { get; set; }
        public bool? View { get; set; }
        public DateTime? CreatedTime { get; set; }
        public bool? New { get; set; }
    }
}
