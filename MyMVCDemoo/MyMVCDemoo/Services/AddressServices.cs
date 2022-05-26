using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyMVCDemoo.Models.EF;
using MyMVCDemoo.Models.Tools;
using MyMVCDemoo.Models.ViewModels.Member;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyMVCDemoo.Services
{
    public class AddressServices
    {
        private readonly MiniQQContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AccountServices _accountServices;

        public AddressServices(MiniQQContext context, IHttpContextAccessor httpContextAccessor, AccountServices accountServices)
        {
            _context = context;
            _accountServices = accountServices;
            _httpContextAccessor = httpContextAccessor;
        }


        public async Task<IEnumerable<T002好友表>> GetListByHostId(string id)
        {
            var model = await _context.T002好友表s.Where(m => m.Qq号 == id.ToString()).ToListAsync();//只显示自己
            //分页显示
            //   await _context.T002好友表s.Where(m => m.Qq号 == id).Skip(10).Take(10);
            return model;
        }

        internal Task GetListByHostId()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 搜索指定用户通讯录记录
        /// </summary>
        /// <param name="id"></param>
        /// <param name="searchTxt"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T002好友表>> SearchListByHostId(string id, string searchTxt)
        {
            var model = await _context.T002好友表s.Where(m => m.Qq号 == id &&
            m.姓名.Contains(searchTxt) ||
           m.电话.Contains(searchTxt)).ToListAsync();
            return model;
        }

        /// <summary>
        /// 加好友（？
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
       public async Task Create(AddressCreateModel model)
        {
            var newItem = new T002好友表() {
                Id = Guid.NewGuid().ToString(),//?????????????
                Qq号=_accountServices.MemberQq,
                电话=model.电话,
                姓名=model.姓名
          
            };
            await _context.AddAsync(newItem);
            await _context.SaveChangesAsync();

        }


        /// <summary>
        /// 返回指定key的详细记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<T002好友表> Detail(string id)
        {
            var item =await _context.T002好友表s.FindAsync(id.ToString());
            //如果不是当前用户相关记录，返回null
            if (item != null&&item.Qq号!=_accountServices.MemberQq)
            {
                item = null;
            }       
            //如果用first什么的找不到就会报错
            return item;
        }


        /// <summary>
        /// 编辑信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task Edit(AddressEditModel model)
        {
            var item = await _context.T002好友表s.FindAsync(model.Id.ToString());
            //如果不是当前用户相关记录，返回null
            if (item == null || item.Qq号 != _accountServices.MemberQq)
                return;
            item.姓名 = model.姓名;
            item.电话 = model.电话;
            await _context.SaveChangesAsync();
        }
        public async Task<AddressEditModel> GetEditModel(string id)
        {
            var item = await Detail(id);
            if (item != null)
            {
                var newModel = new AddressEditModel()
                {
                    姓名 = item.姓名,
                    电话 = item.电话
                };
                //   Util.CopyObjectData(item, newModel);
                return newModel;
            }
            return null;

        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task Delete (string id)
        {
            var item = await _context.T002好友表s.FindAsync(id);
            //如果不是当前用户相关记录，返回null
            if (item == null || item.Qq号 != _accountServices.MemberQq)
                return;
             _context.T002好友表s.Remove(item);
            await _context.SaveChangesAsync();
        }
    }
}
