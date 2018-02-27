using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CTRLXamarinHandsOn.ViewModels
{
    public class MemoListPageViewModel : ViewModelBase
    {
        public MemoListPageViewModel(INavigationService navigationService) 
            : base (navigationService)
        {
            Title = "Memo List Page";
        }
    }
}
