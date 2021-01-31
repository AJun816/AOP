using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Interception.InterceptionBehaviors;
using Unity.Interception.PolicyInjection.Pipeline;

namespace PrismUnityAop.Behavior
{
    public class LogBeforeBehavior: IInterceptionBehavior
    {

        /// <summary>
        /// 1. 标准的通过AOP写日志的功能, 必须实现IInterceptionBehavior; 这个接口来自于Unity容器
        /// </summary>

            /// <summary>
            /// 3. 此方法为固定写法
            /// </summary>
            /// <returns></returns>
            public IEnumerable<Type> GetRequiredInterfaces()
            {
                return Type.EmptyTypes;
            }

            //03 业务逻辑跑到这里
            /// <summary>
            /// 4. 关键方法 调用业务逻辑的地方 
            /// </summary>
            /// <param name="input">input是业务方法的所有信息</param>
            /// <param name="getNext">下一个步骤，执行剩下的动作</param>
            /// <returns></returns>
            public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
            {
                Console.WriteLine("LogBeforeBehavior");
                //5. 如果有的方法想用, 有的方法不想被AOP, 则可以在这里做切断

                //这里可以将某个方法打上特性,然后获取MemberInfo中的特性信息, 以此来判断哪个
                //方法需要AOP切入,哪些方法不需要AOP切入
                Console.WriteLine(input.MethodBase.Name);
                // Console.WriteLine( input.MethodBase.GetCustomAttributes(true)[0].ToString()  );


                foreach (var item in input.Inputs) //input中包含了所有的参数信息
                {
                    Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(item));
                    //反射&序列化获取更多信息
                }
            // Unity.Interception.InterceptionBehaviors.InvokeInterceptionBehaviorDelegate gn = getNext();
            // return getNext()(input, getNext);//注意这种写法也是可以的; 这种写法的意思就是getNext()方法返回了一个委托, 然后委托又被调用了
            //04 固定写法 下一个步骤.Invoke()，每次Invoke都需要   input getNext这两个参数，
            return getNext().Invoke(input, getNext);//4.  固定写法

                //  return null;
                //return gn.Invoke(input, getNext);
                //getNext()表示执行完当前的方法之后, 去执行原始方法;   但是注意的是, getNext()中可能有多个方法
            }


        /// <summary>
        /// 2. 固定写法
        /// </summary>
        public bool WillExecute
            {
                get { return true; }
            }
   

    }
}
