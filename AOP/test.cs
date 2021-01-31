using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOP
{
    public class test : Itest
    {
        public void show()
        {
            Console.WriteLine("======================业务逻辑====================");
            Console.WriteLine("这里是业务逻辑");
            Console.WriteLine("======================业务逻辑====================");
        }
    }
}
