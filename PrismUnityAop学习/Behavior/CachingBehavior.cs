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
    /// 缓存AOP
    /// </summary>
    public class CachingBehavior : IInterceptionBehavior
    {
        public IEnumerable<Type> GetRequiredInterfaces()
        {
            return Type.EmptyTypes;
        }
        /// <summary>
        /// 定义缓存字典
        /// </summary>
        private static Dictionary<string, object> CachingBehaviorDictionary = new Dictionary<string, object>();

        public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            Console.WriteLine("CachingBehavior");
            //把方法的名称和所有的参数列表全部标识为key , 然后放到字典里面;
            string key = $"{input.MethodBase.Name}_{Newtonsoft.Json.JsonConvert.SerializeObject(input.Inputs)}";
            //当方法名和参数都不变, 则表示有缓存


            if (CachingBehaviorDictionary.ContainsKey(key))
            {
                return input.CreateMethodReturn(CachingBehaviorDictionary[key]);//直接返回  [短路器]  不再往下; 包括后面的Behavior也不会再执行了; 因为全部被直接短路了
            }
            else
            {
                //如果字典中没有, 则继续执行这里
                IMethodReturn result = getNext().Invoke(input, getNext);
                if (result.ReturnValue != null) //并将其加入到缓存列表中去; 当然缓存中放一个null没有任何意义, 所以这里判断一下
                {
                    //不存在则, 加入缓存中
                    CachingBehaviorDictionary.Add(key, result.ReturnValue);
                }

              

                return result;
            }


        }

        public bool WillExecute
        {
            get { return true; }
        }
    }
}
