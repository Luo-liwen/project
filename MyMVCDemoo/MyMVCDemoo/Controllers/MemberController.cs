using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyMVCDemoo.Models.ViewModels.Member;
using MyMVCDemoo.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyMVCDemoo.Controllers
{
    [Authorize(Roles = "Member")]//filter写在类前面要求这个类里的所有都要满足该条件
    public class MemberController : Controller
    {
        private readonly AddressServices _addressServices;
        private readonly AccountServices _accountServices;

        public MemberController(AddressServices addressServices, AccountServices accountServices)
        {
            _addressServices = addressServices;
            _accountServices = accountServices;
        }

        public IActionResult Index()
        {
            return RedirectToAction("List");//???????
        }

        /// <summary>
        /// 当前用户的通讯录列表
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> List()
        {
            var model = await _addressServices.GetListByHostId(_accountServices.MemberQq);
            return View(model);
        }

        /// <summary>
        /// 找人
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Search(SearchModel model)
        {
            var ans = await _addressServices.SearchListByHostId(_accountServices.MemberQq, model.SearchTxt);
            return View("List", ans);
        }
      
        
        /// <summary>
        /// 新建记录
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]//!!!!!!!!!!!!
        public async Task<IActionResult> Create(AddressCreateModel model)//名字要和上面那个一样
        {
            //如果校验不通过，返回原页面（带着输入的值
            if (!ModelState.IsValid)
                return View(model);
            try
            {
                //存盘
                await _addressServices.Create(model);
                return RedirectToAction(actionName: "List");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                return View(model);
            }
        }



        /// <summary>
        /// 详细页
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Detail(string id)
        {
            var model = await _addressServices.Detail(id);
            if (model == null)
            {
                return RedirectToAction("List");//防止越权,通过地址栏看别人记录
            }
            return View(model);
    }

        /// <summary>
        /// 编辑记录
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Edit(string id)
        {
            var model = await _addressServices.GetEditModel(id);
            if (model == null)
            {
                return RedirectToAction("List");//防止越权,通过地址栏看别人记录
            }
          return View(model);
        }

        [HttpPost]//!!!!!!!!!!!!
        public async Task<IActionResult> Edit(AddressEditModel model)//名字要和上面那个一样
        {
            //如果校验不通过，返回原页面（带着输入的值
            if (!ModelState.IsValid)
                return View(model);
            try
            {
                //存盘
                await _addressServices.Edit(model);
                return RedirectToAction(actionName: "Detail",new {id=model.Id });
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                return View(model);
            }
        }

        public async Task<IActionResult> Delete(string id)
        {
            await _addressServices.Delete(id);
            return RedirectToAction(actionName: "List");
        }

        /// <summary>
        /// 安全性提高了，防止直接从地址栏走
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Delete2([FromForm]string id)
        {
            await _addressServices.Delete(id);
            return RedirectToAction(actionName: "List");
        }

    }



}

