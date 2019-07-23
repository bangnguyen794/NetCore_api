using System;
using System.Collections.Generic;

namespace ApiCore_facebook.Models
{
    public partial class FbFanpage
    {
        public string Id { get; set; }
        public bool? Active { get; set; }
        public string Name { get; set; }
        public DateTime? SynTime { get; set; }
        public DateTime? ActiveTime { get; set; }
        public string ActiveBy { get; set; }
        public string ActiveNameBy { get; set; }
        public string Banefit { get; set; }
        public bool? SubscribedApps { get; set; }
        public string AccessToken { get; set; }
        public bool? HideCmt { get; set; }
        public bool? LikeCmt { get; set; }
        public bool? Sync { get; set; }
        public string AccountAcctive { get; set; }
        public DateTime? NgayHethan { get; set; }
        public string AppId { get; set; }
    }
}
