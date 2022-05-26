using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;//!!!!!!让我们说，谢谢MVC
using System.Linq;
using System.Threading.Tasks;

namespace MyMVCDemoo.Models.ViewModels.Account
{


    /// <summary>
    /// 用户注册模型
    /// </summary>
    public class RegisterModel
    {
        //限制不能重复
        [Remote("CheckIfNicknameIsNotUsed", "Account")]
        [Required(ErrorMessage = "必填项")]//不填昵称不行
        [MaxLength(50, ErrorMessage = "长度限制为50")]
        public string 昵称 { get; set; }

        [Required(ErrorMessage = "必填项")]
        [DataType(DataType.Date)]
        public DateTime 生日 { get; set; }

        //todo 限制只能是男女
        [UIHint("GenderSelector")]
        [Required(ErrorMessage = "必填项")]
        public string 性别 { get; set; }

        [Required(ErrorMessage = "必填项")]
        //格式校验(使用了正则表达式<随便找的
        [RegularExpression(
            @"^[A-Za-z0-9-._]+@[A-Za-z0-9-]+(\.[A-Za-z0-9]+)*(\.[A-Za-z]{2,6})$",ErrorMessage ="请输入正确的邮件格式")]
        public string 邮箱 { get; set; }



        [Required(ErrorMessage = "必填项")]
        [MaxLength(50, ErrorMessage = "长度限制为20")]
        [DataType(DataType.Password)]//输入时密码显示为点点点
        public string 密码 { get; set; }

        [Required(ErrorMessage = "必填项")]
        [MaxLength(50, ErrorMessage = "长度限制为20")]
        [DataType(DataType.Password)]//输入时密码显示为点点点
        [Compare("密码", ErrorMessage = "两次密码不一致")]
        public string 确认密码 { get; set; }
    }
}
