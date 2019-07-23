using System;
using System.Collections.Generic;

namespace ApiCore_facebook.Models
{
    public partial class AdminAccount
    {
        public int Id { get; set; }
        public string Avatar { get; set; }
        public string Gioitinh { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public DateTime? Ngaysinh { get; set; }
        public string TenNv { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Diachi { get; set; }
        public string Quanhuyen { get; set; }
        public string Tinhthanh { get; set; }
        public string Phongban { get; set; }
        public string Chucvu { get; set; }
        public string Phanquyen { get; set; }
        public string Tinhtrang { get; set; }
        public DateTime? Ngaylam { get; set; }
        public string MaCn { get; set; }
        public string Active { get; set; }
        public DateTime? Date { get; set; }
        public string Status { get; set; }
        public DateTime? Lastactive { get; set; }
        public string Lastpage { get; set; }
        public string Birthday { get; set; }
    }
}
