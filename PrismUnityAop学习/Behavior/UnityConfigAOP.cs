using Microsoft.Practices.Unity.Configuration;
using PrismUnityAop.BLL;
using PrismUnityAop.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Interception;
using Unity;

namespace PrismUnityAop.Behavior
{
    /// <summary>
    /// 使用EntLib\PIAB Unity 实现动态代理
    ///
    /// 1. 添加引用→右键→添加 NuGet包, 添加Unity的包引用
    /// 2. 这里添加的 Unity是5.8.13的版本, 而Unity.Interception是5.5.0; 因为最新的(5.5.5)不支持 .net 4.0
    /// 3. Unity是一个 Unity的容器; 而Unity.Interception当Unity做AOP时的一个扩展
    /// </summary>
    public class UnityConfigAOP
    {
        public static void Show()
        {
            User user = new User()
            {
                Name = "张三",
                Password = "123456"
            };


            #region 引入Unity容器，这是一段固定写法 作用是读取当前应用程序运行目录下的Unity.Config配置文件，配置一个容器
            //1. 初始化UnityContainer容器
            IUnityContainer container = new UnityContainer();
            ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap();

            //2. 开始读取配置文件
            fileMap.ExeConfigFilename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "CfgFiles\\Unity.Config");
            Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
            UnityConfigurationSection configSection = (UnityConfigurationSection)configuration.GetSection(UnityConfigurationSection.SectionName);


            //3.初始化AOP容器  使用配置文件中aopContainer节点下的所有配置信息来初始化container这个AOP容器
            configSection.Configure(container, "aopContainer"); //10. 这个aopContainer就是配置文件中的 <container name="aopContainer"> 这里这个容器的名称 ; 注意如果配置文件中的名字和这里的名字不一样就会报出以下错误:
                                                                // The container named "aopContainer" is not defined in this configuration section.
            #endregion

            // 在这里创建对象, 创建的规则就来自于配置文件的  <register type="MyAOP.UnityWay.IUserProcessor,MyAOP" mapTo="MyAOP.UnityWay.UserProcessor,MyAOP"> 这一行; 如果是 IUserProcessor 类型, 就是使用UserProcessor  来创建实例
            // < !--04 核心代码 把某一个抽象类型（PrismUnityAop.BLL.IUserBll），所在的DLL（EXE名称），mapTo = (把这个接口映射到完整的类型上)-- >
            // < !--IOC容器所做的事情正常情况就是把某一个接口映射到具体类型-- >
            //01 通过容器创建对象
             IUserBll processor = container.Resolve<IUserBll>();
            //02 调用业务方法 正常情况应该是执行业务方法，但是我们设置了AOP所以会按照Unity配置文件中InterfaceInterceptor的顺序运行 所以是LogBeforeBehavior
            //02 所以是在LogBeforeBehavior，里的  public IMethodReturn Invoke方法
            processor.RegUser(user); //4. 注意, 当程序运行到这里的时候, 正常情况应该是去调用userProcessor的Reguser方法, 但是由于在配置文件中进行了配置, 所以它会去执行 LogBeforeBehavior 的 Invoke的方法(当aopContainer这容器中只有一个LogBeforeBehavior的时候, 如果多个, 则那个方法在前面则优先执行, 其它的上依次的调)
            User userNew1 = processor.GetUser(user); //调用GetUser的时候, 也会执行配置文件中对应配置了的方法


            //演示缓存Behavior
            User userNew2 = processor.GetUser(user);
        }
    }

}
