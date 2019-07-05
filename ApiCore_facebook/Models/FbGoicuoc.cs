using System;
using System.Collections.Generic;

namespace ApiCore_facebook.Models
{
    public partial class FbGoicuoc
    {
        public int Id { get; set; }
        public string Ten { get; set; }
        public int? Giatien { get; set; }
        public int? Cauhinh { get; set; }
        public int? Sotrang { get; set; }
        public int? Songuoi { get; set; }
        public int? GioihanGui { get; set; }
    }
}
