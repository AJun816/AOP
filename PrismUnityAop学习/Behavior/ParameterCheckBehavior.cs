using PrismUnityAop.Models;
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
    /// 参数检查
    /// </summary>
    public class ParameterCheckBehavior : IInterceptionBehavior
    {
        public IEnumerable<Type> GetRequiredInterfaces()
        {
            return Type.EmptyTypes;
        }

        public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            Console.WriteLine("ParameterCheckBehavior");

            //业务逻辑执行前，检查一下第一个参数是不是User
            User user = input.Inputs[0] as User;//可以不写死类型，反射+特性完成数据有效性监测
            if (user.Password.Length < 10)//可以过滤一下敏感词
            {
                //返回一个异常
                return input.CreateExceptionMethodReturn(new Exception("密码长度不能小于10位"));
                //注意只要抛出异常, 那么后面的都不会再执行了, 在这里也就是说后的 logafterbehavior是不会再执行了
                //throw new Exception("密码长度不能小于10位");
            }
            else
            {
                Console.WriteLine("参数检测无误");
                return getNext().Invoke(input, getNext);
            }
        }

        public bool WillExecute
        {
            get { return true; }
        }
    }
}
