using Microsoft.Practices.Unity.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace AOP
{
    class Program
    {
        static void Main(string[] args)
        {
            Print();
        }

        static void Print()
        {


            #region 引入Unity容器，这是一段固定写法 作用是读取当前应用程序运行目录下的Unity.Config配置文件，配置一个容器
            //1. 初始化UnityContainer容器
            IUnityContainer container = new UnityContainer();
            ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap();
            //2. 开始读取配置文件
            fileMap.ExeConfigFilename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "CfgFiles\\Unity.Config");
            Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
            UnityConfigurationSection configSection = (UnityConfigurationSection)configuration.GetSection(UnityConfigurationSection.SectionName);
            #endregion
            //01 使用UnityConfigurationSection 配置(容器，名称)方法代替  配置容器 将容器和配置文件中的别名关联"
            configSection.Configure(container, "aopContainer");
            //02 通过容器创建对象
            Itest processor = container.Resolve<Itest>();
            //02 调用业务方法
            processor.show(); 
        }
    }
}
