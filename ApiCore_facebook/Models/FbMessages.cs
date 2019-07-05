using System;
using System.Collections.Generic;

namespace ApiCore_facebook.Models
{
    public partial class FbMessages
    {
        public string Id { get; set; }
        public string IdPage { get; set; }
        public string IdUser { get; set; }
        public string Message { get; set; }
        public string NameUser { get; set; }
        public bool? Views { get; set; }
        public bool? ViewsUpdate { get; set; }
        public DateTime? UpdateTime { get; set; }
        public string ViewsBy { get; set; }
        public string Type { get; set; }
        public bool? Advisory { get; set; }
        public bool? YelledCapital { get; set; }
        public bool? Success { get; set; }
        public bool? Change { get; set; }
        public string Note { get; set; }
        public string Unknow { get; set; }
        public bool? Done { get; set; }
        public bool? Block { get; set; }
        public string Psid { get; set; }
        public string Hoten { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int? Goi { get; set; }
        public bool? Boqua { get; set; }
        public bool? Khongmuahang { get; set; }
        public bool? Khongphanhoi { get; set; }
        public bool? Cotuongtac { get; set; }
        public bool? Tuongtactot { get; set; }
        public bool? Damuahang { get; set; }
        public string Nguoicapnhap { get; set; }
        public string PhoneAo { get; set; }
        public bool? FindPhone { get; set; }
        public string PhoneKhac { get; set; }
        public DateTime? ThoigianTuongtac { get; set; }
        public string Thoigiangoi { get; set; }
        public DateTime? ThoigianTao { get; set; }
        public string Tinhthanh { get; set; }
        public string Quanhuyen { get; set; }
        public int I { get; set; }
    }
}
