using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Interception.InterceptionBehaviors;
using Unity.Interception.PolicyInjection.Pipeline;

namespace AOP
{
    public class LogBehavior : IInterceptionBehavior
    {
        public bool WillExecute
        {
            get { return true; }
        }

        public IEnumerable<Type> GetRequiredInterfaces()
        {
            return Type.EmptyTypes;
        }

        public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            Console.WriteLine("\r\n");
            Console.WriteLine("=================耗时统计=========================");
            Console.WriteLine("正在记录日志，请稍后······");
            var method = getNext().Invoke(input,getNext);
            Console.WriteLine("=================耗时统计=========================");
            return method;
        }
    }
}
