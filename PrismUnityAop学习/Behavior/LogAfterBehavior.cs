using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Interception.InterceptionBehaviors;
using Unity.Interception.PolicyInjection.Pipeline;

namespace PrismUnityAop.Behavior
{
    //IInterceptionBehavior来自于AOP容器中      Unity.Interception.InterceptionBehaviors;
    public class LogAfterBehavior : IInterceptionBehavior
    {
        /// <summary>
        /// 固定写法
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Type> GetRequiredInterfaces()
        {
            return Type.EmptyTypes;
        }

        /// <summary>
        /// 可以记录调用时间, 参数, 函数名称
        /// </summary>
        /// <param name="input"></param>
        /// <param name="getNext"></param>
        /// <returns></returns>
        public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            //getNext()(input, getNext);  关键点; 如果业务代码写在这句话之前, 那么就会先执行业务代码, 再执行配置文件中配置的实例代码; 反之同理

            IMethodReturn methodReturn = getNext().Invoke(input, getNext);//执行后面的全部动作
            //原始方法执行后

            Console.WriteLine("LogAfterBehavior");
            Console.WriteLine(input.MethodBase.Name);
            foreach (var item in input.Inputs)
            {
                Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(item));
                //反射&序列化获取更多信息
            }
            Console.WriteLine("LogAfterBehavior" + methodReturn.ReturnValue);
            return methodReturn;
        }

        /// <summary>
        /// 固定写法
        /// </summary>
        public bool WillExecute
        {
            get { return true; }
        }
    }
}
