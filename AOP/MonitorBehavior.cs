using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Interception.InterceptionBehaviors;
using Unity.Interception.PolicyInjection.Pipeline;

namespace AOP
{
    /// <summary>
    /// 性能监控的AOP扩展
    /// </summary>
    public class MonitorBehavior : IInterceptionBehavior
    {
        public IEnumerable<Type> GetRequiredInterfaces()
        {
            return Type.EmptyTypes;
        }

        public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            Console.WriteLine("\r\n");
            Console.WriteLine("=================耗时统计=========================");
            Console.WriteLine(this.GetType().Name);
            //性能监控推荐 精确度比较高 精确到毫秒
            Stopwatch stopwatch = new Stopwatch();
            string methodName = input.MethodBase.Name;
            //方法执行前记录一下时间
            stopwatch.Start();
            //业务逻辑执行
            var methodReturn = getNext().Invoke(input,getNext);//后续逻辑执行

            stopwatch.Stop();
            Console.WriteLine("请稍后正在计算运行时间······");
            Console.WriteLine($"{this.GetType().Name}统计方法{methodName}执行耗时{stopwatch.ElapsedMilliseconds}ms");
            Console.WriteLine("=================耗时统计=========================");

            Console.Read();
            return methodReturn;         
        }

        public bool WillExecute
        {
            get { return true; }
        }
    }
}
