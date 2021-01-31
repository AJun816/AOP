using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Interception.InterceptionBehaviors;
using Unity.Interception.PolicyInjection.Pipeline;

namespace PrismUnityAop.Behavior
{
    /// <summary>
    /// 异常处理  和其他几个不同，其他几个都是自己的逻辑执行完以后才回去执行下一步，异常通常是包裹住方法，所以虽然是在写在前面，但是我先把后面的步骤执行一下
    /// 后面的步骤执行完以后，检查异常做出对应判断
    /// </summary>
    public class ExceptionLoggingBehavior : IInterceptionBehavior
    {
        public IEnumerable<Type> GetRequiredInterfaces()
        {
            return Type.EmptyTypes;
        }

        public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
           
            Console.WriteLine("ExceptionLoggingBehavior");
            /// 异常处理  和其他几个不同，其他几个都是自己的逻辑执行完以后才回去执行下一步，异常通常是包裹住方法，
            /// 所以虽然是在写在前面，但是我先把后面的步骤执行一下
            IMethodReturn methodReturn = getNext()(input, getNext);
            /// 后面的步骤执行完以后，检查异常做出对应判断
            if (methodReturn.Exception == null) //检查methodReturn中是否有异常
            {
                Console.WriteLine("无异常");
            }
            else
            {
                Console.WriteLine($"异常:{methodReturn.Exception.Message}");
            }
            return methodReturn;
        }

        public bool WillExecute
        {
            get { return true; }
        }
    }
}
