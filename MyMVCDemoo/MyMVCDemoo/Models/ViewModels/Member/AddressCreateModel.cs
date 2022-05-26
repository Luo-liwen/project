using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyMVCDemoo.Models.ViewModels.Member
{
    /// <summary>
    /// 通讯录记录创造模型
    /// </summary>
    public class AddressCreateModel
    {
        [Required(ErrorMessage="必填")]
        [MaxLength(50, ErrorMessage = "长度限制为50")]
        public string 姓名 { get; set; }

        [Required(ErrorMessage = "必填")]
        [MaxLength(50, ErrorMessage = "长度限制为50")]
        public string 电话 { get; set; }
    }
}
