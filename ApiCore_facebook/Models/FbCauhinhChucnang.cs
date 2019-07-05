using System;
using System.Collections.Generic;

namespace ApiCore_facebook.Models
{
    public partial class FbCauhinhChucnang
    {
        public int Id { get; set; }
        public string Ten { get; set; }
        public bool? Taodon { get; set; }
        public bool? Tuvan { get; set; }
        public bool? ChamsocKh { get; set; }
        public bool? TimSdt { get; set; }
        public bool? Goptrang { get; set; }
        public bool? TimKh { get; set; }
        public bool? GuitinQc { get; set; }
    }
}
