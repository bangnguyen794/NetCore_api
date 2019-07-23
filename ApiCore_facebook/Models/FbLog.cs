using System;
using System.Collections.Generic;

namespace ApiCore_facebook.Models
{
    public partial class FbLog
    {
        public int Id { get; set; }
        public string IdUser { get; set; }
        public string Account { get; set; }
        public string IdPage { get; set; }
        public string NameUser { get; set; }
        public bool Status { get; set; }
        public DateTime? CreatedTime { get; set; }
        public string Message { get; set; }
    }
}
