using System;
using System.Collections.Generic;

namespace ApiCore_facebook.Models
{
    public partial class FbDonviVanchuyen
    {
        public int Id { get; set; }
        public string Ten { get; set; }
        public string ApiKey { get; set; }
        public string Link { get; set; }
        public bool? Trangthai { get; set; }
        public int? IdDomain { get; set; }
        public DateTime? Ngaytao { get; set; }
        public string MakhoViettel { get; set; }
        public string CusidViettel { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Logo { get; set; }
        public string Weight { get; set; }
    }
}
