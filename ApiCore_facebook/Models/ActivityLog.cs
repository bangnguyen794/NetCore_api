using System;
using System.Collections.Generic;

namespace ApiCore_facebook.Models
{
    public partial class ActivityLog
    {
        public int LogId { get; set; }
        public int? LogRunId { get; set; }
        public DateTime? LogTimestamp { get; set; }
        public string LogMessage { get; set; }
    }
}
