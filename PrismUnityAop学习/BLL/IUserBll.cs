using PrismUnityAop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrismUnityAop.BLL
{
    public interface IUserBll
    {
        void RegUser(User user);
        User GetUser(User user);
    }
}
