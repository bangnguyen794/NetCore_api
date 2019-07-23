using System;
using System.Collections.Generic;

namespace ApiCore_facebook.Models
{
    public partial class FbUserBieucam
    {
        public int Id { get; set; }
        public string IdPage { get; set; }
        public string IdUser { get; set; }
        public string NameUser { get; set; }
        public string Hoten { get; set; }
        public string Phone { get; set; }
        public string Item { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public bool? Baoxau { get; set; }
        public string Lydo { get; set; }
        public DateTime? UpdateTime { get; set; }
        public bool? Block { get; set; }
        public bool? Boqua { get; set; }
        public int? Goi { get; set; }
        public bool? Khongphanhoi { get; set; }
        public bool? Cotuongtac { get; set; }
        public bool? Tuongtactot { get; set; }
        public bool? Damuahang { get; set; }
        public string Note { get; set; }
        public string Thoigiangoi { get; set; }
        public DateTime? ThoigianTuongtac { get; set; }
        public string Nguoicapnhap { get; set; }
        public string PhoneAo { get; set; }
    }
}
