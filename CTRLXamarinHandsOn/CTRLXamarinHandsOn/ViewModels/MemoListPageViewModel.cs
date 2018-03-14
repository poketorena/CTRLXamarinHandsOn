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
        private DelegateCommand _navigateEditPageCommand;
        public DelegateCommand NavigateEditPageCommand =>
            _navigateEditPageCommand ?? (_navigateEditPageCommand = new DelegateCommand(async () => await NavigationService.NavigateAsync(nameof(EditPage))));

        private DelegateCommand<Memo> _itemSelectedCommand;
        public DelegateCommand<Memo> ItemSelectedCommand =>
            _itemSelectedCommand ?? (_itemSelectedCommand = new DelegateCommand<Memo>(ExecuteItemSelectedCommand));

        // プライベート変数
        private readonly IMemoHolder _memoHolder;

        // コンストラクタ
        public MemoListPageViewModel(INavigationService navigationService, IMemoHolder memoHolder)
            : base(navigationService)
        {
            _memoHolder = memoHolder;
            Memos = _memoHolder.ToReactivePropertyAsSynchronized(x => x.Memos);
        }

        // プライベート関数
        private async void ExecuteItemSelectedCommand(Memo memo)
        {
            var parameters = new NavigationParameters
            {
                { "memo", memo }
            };
            await NavigationService.NavigateAsync(nameof(EditPage), parameters);
        }
    }
}
