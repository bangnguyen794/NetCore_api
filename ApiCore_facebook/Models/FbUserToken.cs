using System;
using System.Collections.Generic;

namespace ApiCore_facebook.Models
{
    public partial class FbUserToken
    {
        public int Id { get; set; }
        public string AppId { get; set; }
        public string IdUser { get; set; }
        public string NameUser { get; set; }
        public string AccessToken { get; set; }
        public DateTime? ActiveTime { get; set; }
        public string Page { get; set; }
        public bool? Sync { get; set; }
        public string PageActive { get; set; }
        public int? Goicuoc { get; set; }
        public string Perms { get; set; }
        public string Email { get; set; }
        public string MaThanhtoan { get; set; }
        public string LinkConnect { get; set; }
        public string ApiConnect { get; set; }
        public bool? CheckConnect { get; set; }
        public bool? HienthiTentag { get; set; }
        public bool? BatNhieutag { get; set; }
        public bool? LocNhieutag { get; set; }
        public bool? Typing { get; set; }
        public bool? ClickRightTag { get; set; }
        public bool? SaveInforHopthoai { get; set; }
    }
}
