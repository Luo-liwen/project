using System;
using System.Collections.Generic;

#nullable disable

namespace MyMVCDemoo.Models.EF
{
    public partial class T001用户表
    {
        public string Qq号 { get; set; }
        public string 昵称 { get; set; }
        public DateTime 生日 { get; set; }
        public string 密码 { get; set; }
        public string 性别 { get; set; }
        public string 邮箱 { get; set; }
    }
}
