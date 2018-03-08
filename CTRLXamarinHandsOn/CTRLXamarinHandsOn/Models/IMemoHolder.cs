using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;

namespace CTRLXamarinHandsOn.Models
{
    public interface IMemoHolder : INotifyPropertyChanged
    {
        ObservableCollection<Memo> Memos { get; set; }
        Task AddAsync(Memo memo);
        Task LoadAsync();
        Task Update(string memoId, Memo memo);
        Task Delete(string memoId);
    }
}
