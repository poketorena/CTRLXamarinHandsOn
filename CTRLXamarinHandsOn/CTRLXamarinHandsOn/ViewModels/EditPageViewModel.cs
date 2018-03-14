using CTRLXamarinHandsOn.Models;
using Prism.Commands;
using Prism.Navigation;
using Reactive.Bindings;

namespace CTRLXamarinHandsOn.ViewModels
{
    public class EditPageViewModel : ViewModelBase
    {
        // プロパティ
        public ReactiveProperty<bool> IsAddMode { get; } = new ReactiveProperty<bool>();

        public ReactiveProperty<bool> IsUpdateMode { get; } = new ReactiveProperty<bool>();

        public ReactiveProperty<Memo> Memo { get; } = new ReactiveProperty<Memo>();

        public ReactiveProperty<Memo> TempMemo { get; } = new ReactiveProperty<Memo>();

        // デリゲートコマンド
        private DelegateCommand _saveCommand;
        public DelegateCommand SaveCommand =>
            _saveCommand ?? (_saveCommand = new DelegateCommand(ExecuteSaveCommand));

        private DelegateCommand _updateCommand;
        public DelegateCommand UpdateCommand =>
            _updateCommand ?? (_updateCommand = new DelegateCommand(ExecuteUpdateCommand));

        private DelegateCommand _deleteCommand;
        public DelegateCommand DeleteCommand =>
            _deleteCommand ?? (_deleteCommand = new DelegateCommand(ExecuteDeleteCommand));

        // プライベート変数
        private readonly IMemoHolder _memoHolder;

        // コンストラクタ
        public EditPageViewModel(INavigationService navigationService, IMemoHolder memoHolder)
            : base(navigationService)
        {
            _memoHolder = memoHolder;
        }

        // パブリック関数
        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            if (parameters.TryGetValue<Memo>("memo", out var memo))
            {
                Memo.Value = new Memo
                {
                    Title = memo.Title,
                    Text = memo.Text
                };
                TempMemo.Value = memo;
                IsUpdateMode.Value = true;
                IsAddMode.Value = false;
            }
            else
            {
                Memo.Value = new Memo();
                IsUpdateMode.Value = false;
                IsAddMode.Value = true;
            }
        }

        // プライベート関数
        private async void ExecuteSaveCommand()
        {
            await _memoHolder.AddAsync(Memo.Value);
            await NavigationService.GoBackAsync();
        }

        private async void ExecuteUpdateCommand()
        {
            await _memoHolder.Update(TempMemo.Value.Id, Memo.Value);
            await NavigationService.GoBackAsync();
        }

        private async void ExecuteDeleteCommand()
        {
            await _memoHolder.Delete(TempMemo.Value.Id);
            await NavigationService.GoBackAsync();
        }
    }
}
