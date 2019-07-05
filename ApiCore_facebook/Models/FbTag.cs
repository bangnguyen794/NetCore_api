using System;
using System.Collections.Generic;

namespace ApiCore_facebook.Models
{
    public partial class FbTag
    {
        public int Id { get; set; }
        public string IdPage { get; set; }
        public string CreatedIdBy { get; set; }
        public string Title { get; set; }
        public string Color { get; set; }
        public DateTime? CreatedTime { get; set; }
        public bool? Status { get; set; }
    }
}
