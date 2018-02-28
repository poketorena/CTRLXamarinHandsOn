using CTRLXamarinHandsOn.Models;
using CTRLXamarinHandsOn.Views;
using Prism.Commands;
using Prism.Navigation;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System.Collections.ObjectModel;

namespace CTRLXamarinHandsOn.ViewModels
{
    public class MemoListPageViewModel : ViewModelBase
    {
        // プロパティ
        public ReactiveProperty<ObservableCollection<Memo>> Memos { get; }

        // デリゲートコマンド
        public DelegateCommand NavigateEditPageCommand { get; }
        public DelegateCommand<Memo> ItemSelectedCommand { get; }

        // プライベート変数
        private readonly MemoHolder _memoHolder;

        // コンストラクタ
        public MemoListPageViewModel(INavigationService navigationService, MemoHolder memoHolder)
            : base(navigationService)
        {
            _memoHolder = memoHolder;
            Memos = _memoHolder.ToReactivePropertyAsSynchronized(x => x.Memos);
            NavigateEditPageCommand = new DelegateCommand(() => NavigationService.NavigateAsync(nameof(EditPage)));
            ItemSelectedCommand = new DelegateCommand<Memo>(ExecuteItemSelected);
        }

        // プライベート関数
        private void ExecuteItemSelected(Memo memo)
        {
            var parameters = new NavigationParameters
            {
                { "memo", memo }
            };
            NavigationService.NavigateAsync(nameof(EditPage), parameters);
        }
    }
}
