using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyMVCDemoo.Models.ViewModels.Account
{
    /// <summary>
    /// 登录模型
    /// </summary>
    public class LoginModel
    {

        [Required(ErrorMessage = "必填项")]

        public string Qq号 { get; set; }

        [Required(ErrorMessage = "必填项")]
        [DataType(DataType.Password)]
        public string 密码 { get; set; }

    }
}
