using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyMVCDemoo.Models.ViewModels.Member
{

    /// <summary>
    /// 搜索模型
    /// </summary>
    public class SearchModel
    {
        [Required(ErrorMessage="不能为空")]
        [Display(Name ="搜索")]
        public string SearchTxt
        {
            get;set;
        }

    }
}
