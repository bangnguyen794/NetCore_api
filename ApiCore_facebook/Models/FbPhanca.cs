using System;
using System.Collections.Generic;

namespace ApiCore_facebook.Models
{
    public partial class FbPhanca
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public TimeSpan? Start { get; set; }
        public TimeSpan? Done { get; set; }
        public int? Delay { get; set; }
    }
}
