using Newtonsoft.Json;
using PCLStorage;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace CTRLXamarinHandsOn.Models
{
    public class MemoHolder : BindableBase, IMemoHolder
    {
        // プロパティ
        private ObservableCollection<Memo> _memos;
        public ObservableCollection<Memo> Memos
        {
            get { return _memos; }
            set { SetProperty(ref _memos, value); }
        }

        // コンストラクタ
        public MemoHolder()
        {
            LoadAsync();
        }

        // パブリック関数
        public async Task AddAsync(Memo memo)
        {
            Memos.Add(memo);
            await SaveToJson();
        }

        public async Task LoadAsync()
        {
            var rootFolder = FileSystem.Current.LocalStorage;
            var file = await rootFolder.CreateFileAsync("Memos.json", CreationCollisionOption.OpenIfExists);
            var jsonString = await file.ReadAllTextAsync();

            Memos = JsonConvert.DeserializeObject<ObservableCollection<Memo>>(jsonString) ?? new ObservableCollection<Memo>();
        }

        public async Task Update(string memoId, Memo memo)
        {
            for (int i = 0; i < Memos.Count; i++)
            {
                if (Memos[i].Id == memoId)
                {
                    Memos[i].Title = memo.Title;
                    Memos[i].Text = memo.Text;
                }
            }
            await SaveToJson();
        }

        public async Task Delete(string memoId)
        {
            int removeIndex = -1;
            for (int i = 0; i < Memos.Count; i++)
            {
                if (Memos[i].Id == memoId)
                {
                    removeIndex = i;
                }
            }
            Memos.RemoveAt(removeIndex);
            await SaveToJson();
        }

        // プライベート関数
        private async Task SaveToJson()
        {
            var jsonString = JsonConvert.SerializeObject(Memos);

            var rootFolder = FileSystem.Current.LocalStorage;
            var file = await rootFolder.CreateFileAsync("Memos.json", CreationCollisionOption.ReplaceExisting);
            await file.WriteAllTextAsync(jsonString);
        }
    }
}
