using System;
using System.Collections.Generic;

namespace ApiCore_facebook.Models
{
    public partial class FbAccountQuyen
    {
        public int Id { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Hoten { get; set; }
        public string Phone { get; set; }
        public string Diachi { get; set; }
        public string Page { get; set; }
        public string Quyen { get; set; }
        public bool? Active { get; set; }
        public DateTime? CreatedTime { get; set; }
        public string CreatedBy { get; set; }
        public int? WebConnect { get; set; }
        public string AccountNhanvien { get; set; }
        public string Avatar { get; set; }
    }
}
