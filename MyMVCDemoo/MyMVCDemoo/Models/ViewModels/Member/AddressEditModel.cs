using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MyMVCDemoo.Models.EF;

namespace MyMVCDemoo.Models.ViewModels.Member
{
    public class AddressEditModel:AddressCreateModel
    {
        [Key]//代表是个关键字
        public Guid Id { get; set; }

    }
}
