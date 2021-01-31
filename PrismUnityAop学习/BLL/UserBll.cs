using PrismUnityAop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrismUnityAop.BLL
{
    public class UserBll : IUserBll
    {
        [Obsolete]
        public User GetUser(User user)
        {
            return user;
        }

        public void RegUser(User user)
        {
            Console.WriteLine("用户已经注册");
            //throw new Exception("账号或密码错误");
        }
    }
}
