using System;
using System.Collections.Generic;

namespace ApiCore_facebook.Models
{
    public partial class FbPhancaChitiet
    {
        public int Id { get; set; }
        public string PhancaSang { get; set; }
        public string PhancaChieu { get; set; }
        public int? Macn { get; set; }
        public int? Ngay { get; set; }
        public int? Thang { get; set; }
        public int? Nam { get; set; }
        public string Thu { get; set; }
        public string ThuVn { get; set; }
    }
}
