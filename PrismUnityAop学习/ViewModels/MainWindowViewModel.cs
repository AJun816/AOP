using Prism.Commands;
using Prism.Mvvm;
using PrismUnityAop.Behavior;
using PrismUnityAop.BLL;
using PrismUnityAop.Models;

namespace PrismUnityAop.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Prism Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private string _test = "测试";
        public string Test
        {
            get { return _test ; }
            set { SetProperty(ref _test , value); }
        }

        private DelegateCommand _userCommand;
        public DelegateCommand UserCommand =>
            _userCommand ?? (_userCommand = new DelegateCommand(ExecuteUserCommand));

        void ExecuteUserCommand()
        {
            UnityConfigAOP.Show();
        }


        public MainWindowViewModel()
        {

        }
    }
}
