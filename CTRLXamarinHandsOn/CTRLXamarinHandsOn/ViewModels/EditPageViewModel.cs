using CTRLXamarinHandsOn.Models;
using Prism.Commands;
using Prism.Navigation;

namespace CTRLXamarinHandsOn.ViewModels
{
    public class EditPageViewModel : ViewModelBase
    {
        // プロパティ
        private bool _isAddMode;
        public bool IsAddMode
        {
            get { return _isAddMode; }
            set { SetProperty(ref _isAddMode, value); }
        }

        private bool _isUpdateMode;
        public bool IsUpdateMode
        {
            get { return _isUpdateMode; }
            set { SetProperty(ref _isUpdateMode, value); }
        }

        private Memo _memo = new Memo();
        public Memo Memo
        {
            get { return _memo; }
            set { SetProperty(ref _memo, value); }
        }

        private Memo _tempMemo = new Memo();
        public Memo TempMemo
        {
            get { return _tempMemo; }
            set { SetProperty(ref _tempMemo, value); }
        }

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
                Memo = new Memo
                {
                    Title = memo.Title,
                    Text = memo.Text
                };
                TempMemo = memo;
                IsUpdateMode = true;
                IsAddMode = false;
            }
            else
            {
                Memo = new Memo();
                IsUpdateMode = false;
                IsAddMode = true;
            }
        }

        // プライベート関数
        private async void ExecuteSaveCommand()
        {
            await _memoHolder.AddAsync(Memo);
            await NavigationService.GoBackAsync();
        }

        private async void ExecuteUpdateCommand()
        {
            await _memoHolder.Update(TempMemo.Id, Memo);
            await NavigationService.GoBackAsync();
        }

        private async void ExecuteDeleteCommand()
        {
            await _memoHolder.Delete(TempMemo.Id);
            await NavigationService.GoBackAsync();
        }
    }
}
