using PrismUnityAop.Views;
using Prism.Ioc;
using Prism.Modularity;
using System.Windows;
using Unity;
using Unity.Interception;
using PrismUnityAop.BLL;

namespace PrismUnityAop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            //01 使用容器Unity
            IUnityContainer unityContainer = Container.Resolve<IUnityContainer>();
            //02 代码处理  添加一个扩展，扩展用来选择容器，创建对象的时候考虑一下切面
            unityContainer.AddNewExtension<Interception>().RegisterType<IMenuBll,MenuBll>();
            


            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }
    }
}
