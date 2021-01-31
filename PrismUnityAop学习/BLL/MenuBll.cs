using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrismUnityAop.BLL
{
    public class MenuBll : IMenuBll
    {
        public void GetMenus()
        {
            Console.WriteLine("获取了系统菜单");
        }
    }
}
