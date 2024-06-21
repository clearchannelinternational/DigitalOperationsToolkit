using Broadsign_DOMS.Model;

using Broadsign_DOMS.Service;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Broadsign_DOMS.ViewModel
{
    public class HomeViewModel : ObservableObject, IPageViewModel
    {
        ICommand _problemView;
        ICommand _adminView;
        ICommand _logOutCommand;
        public ObservableCollection<Domain> ListDomains { get; set; }
   

        public ICommand ProblemView
        {
            get
            {
                return _problemView ?? (new RelayCommand(x =>
                {
                    Mediator.Notify("ProblemViewModel", "");
                }));
            }
        }

        public ICommand AdminView
        {
            get
            {
                return _adminView ?? (new RelayCommand(x =>
                {
                    Mediator.Notify("AdminViewModel", "");
              
                }));

            }
        }

        public ICommand LogOutCommand
        {
            get
            {
                return _logOutCommand ?? new RelayCommand(x => { Mediator.Notify("LoginViewModel", ""); });
            }
        }
        public HomeViewModel()
        {
       
            Messenger.Default.Register<ObservableCollection<Domain>>(this,"HomeViewModel", x => ListDomains = x, true);
   

        }

    }
}
