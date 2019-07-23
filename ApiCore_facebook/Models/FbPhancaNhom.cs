using System;
using System.Collections.Generic;

namespace ApiCore_facebook.Models
{
    public partial class FbPhancaNhom
    {
        public int Id { get; set; }
        public string Casang { get; set; }
        public string Cachieu { get; set; }
        public bool? Doica { get; set; }
        public bool? Tructhay { get; set; }
        public string AccountTruc { get; set; }
        public string AccountThay { get; set; }
        public string CreatedBy { get; set; }
    }
}
