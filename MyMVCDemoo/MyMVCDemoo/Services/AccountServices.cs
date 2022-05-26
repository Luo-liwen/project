using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MyMVCDemoo.Models.EF;
using MyMVCDemoo.Models.Tools;
using MyMVCDemoo.Models.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;


namespace MyMVCDemoo.Services
{
    public class AccountServices  //!!!!!!!!别忘了写完之后还要在starup注入服务
    {
        private readonly MiniQQContext _context;
        private readonly Random rnd;
        private readonly IConfiguration _configuration;
      private readonly LoginModel model;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AccountServices(MiniQQContext context, IConfiguration configuration,IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _configuration = configuration;
            rnd = new Random();
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// 返回指定的nickname是否被使用
        /// </summary>
        /// <param name="nickname"></param>
        /// <returns></returns>
        public async Task<bool> IsNicknameUsed(string nickname)
        {
            return await _context.T001用户表s.AnyAsync(m => m.昵称 == nickname);
        }

        /// <summary>
        ///注册
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<String> Create(RegisterModel model)
        {
            //生成一个新的QQ号
            string newQQ = " ";
            do
            {
                newQQ = rnd.Next(10000, 99999).ToString();
                if (await _context.T001用户表s.AllAsync(m => m.Qq号 != newQQ))
                {
                    break;
                }
            } while (true);

            var t001 = new T001用户表();
            Util.CopyObjectData(model, t001);//同名属性的赋值,拷贝
            t001.Qq号 = newQQ;
            t001.密码 = Util.Md5Hash(model.密码);

            await _context.T001用户表s.AddAsync(t001);
            await _context.SaveChangesAsync();
            return newQQ;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> Login(LoginModel model)
        {
            var pwd = Util.Md5Hash(model.密码);
            var item = await _context.T001用户表s.FirstOrDefaultAsync(m=>m.Qq号==model.Qq号 && m.密码 == pwd);
            //如果不存在，item==null，否则返回满足条件的第一条记录

            if (item == null) return false;
            else
            {
                var claims=new List<Claim>
                {
                  new Claim(ClaimTypes.Name, item.昵称),
                  new Claim(ClaimTypes.Sid,item.Qq号),
                  new Claim(ClaimTypes.Role, "Member")
                };
                var claimsIdentity = new ClaimsIdentity(
                       claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await _httpContextAccessor.HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity));
            }
            return true;
            }

        /// <summary>
        /// 退出登录
        /// </summary>
        /// <returns></returns>
        public async Task Logout()
        {
          await _httpContextAccessor. HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
     
        public string MemberQq => _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Sid)?.Value;

        public string MemberName => _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
        }



    }


