using System;
using System.Collections.Generic;

namespace ApiCore_facebook.Models
{
    public partial class FbTinhtrangDonhang
    {
        public int Id { get; set; }
        public string Ten { get; set; }
        public bool? TrangThai { get; set; }
        public int? IdDomain { get; set; }
    }
}
