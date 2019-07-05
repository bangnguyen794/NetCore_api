using System;
using System.Collections.Generic;

namespace ApiCore_facebook.Models
{
    public partial class FbPageDetail
    {
        public int Id { get; set; }
        public string IdPage { get; set; }
        public string IdUser { get; set; }
        public string Chude { get; set; }
        public string Quyen { get; set; }
        public bool? Xoa { get; set; }
    }
}
