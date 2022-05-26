using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using MyMVCDemoo.Models.EF;
using MyMVCDemoo.Models.ViewModels.Account;
using MyMVCDemoo.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyMVCDemoo.Controllers
{
    public class AccountController : Controller
    {
        private readonly AccountServices _accountServices;

        public AccountController(AccountServices accountServices)
        {
            _accountServices = accountServices;
        }
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <returns></returns>
        public IActionResult Register()//到这里为止没有问题，后面说要往数据库送，页面都打开不了
        {
            return View();//传统情况下应该传一个(那个页面最顶上的)模型，这里缺省了
        }

        [HttpPost]//!!!!!!!!!!!!
        public async Task<IActionResult> Register(RegisterModel model)
        {
            //如果校验不通过，返回原页面（带着输入的值
            if (!ModelState.IsValid)
                return View(model);
            try
            {
                //存盘
                var newQQ = await _accountServices.Create(model);
                return RedirectToAction(actionName: "ShowNewQQ", routeValues: new { qq = newQQ });//ShowNewQQ处注释中的"上面"
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                return View(model);
            }
        }

        /// <summary>
        /// 显示新的QQ
        /// </summary>
        /// <returns></returns>
        public IActionResult ShowNewQQ(string qq)//传的参数名称要和上面的名称相同
        {
            ViewBag.id = qq;
            return View();
            // return View(model);//传的不能是string!!!!!!!!!!!传”Error“的话它要找个error的页面
        }

        /// <summary>
        /// 检查指定的nickname是否被使用
        /// </summary>
        /// <param name="昵称"></param>
        /// <returns></returns>
        public async Task<IActionResult> CheckIfNicknameIsNotUsed(string 昵称)//参数名称一定要和校验字段名字相同
        {
            if (!await _accountServices.IsNicknameUsed(昵称))
                return Json(true);//若为true则为没有被使用过
            return Json("该昵称已被使用");
        }


        //跟注册类似，写2个
        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Login()
        {
//#if DEBUG
            //直接登录，省得每次都要登录,调试小技巧
            //var model = new LoginModel()
            //{
            //    Qq号 = "32483",
            //    密码 = "666"
            //};
            ////存盘
            //var ans = await _accountServices.Login(model);
            //if (ans)
            //    return RedirectToAction(actionName: "Index", controllerName: "Member");
            //else
            //{
            //    ModelState.AddModelError("", "用户名或密码错误");
            //    return View(model);
            //}
//#else
            return View();
//#endif
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            //如果校验不通过，返回原页面（带着输入的值
            if (!ModelState.IsValid)
                return View(model);
            try
            {
                //存盘
                var ans = await _accountServices.Login(model);
                if (ans)
                    return RedirectToAction(actionName: "Index", controllerName: "Member");
                else
                {
                    ModelState.AddModelError("", "用户名或密码错误");
                    return View(model);
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                return View(model);
            }
        }


        public async Task<IActionResult> Logout()
        {
            await _accountServices.Logout();
            return RedirectToAction("Index", "Home");
        }
    }
}
