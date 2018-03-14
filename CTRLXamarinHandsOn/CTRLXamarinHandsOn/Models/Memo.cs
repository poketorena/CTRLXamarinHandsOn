using Prism.Mvvm;
using System;

namespace CTRLXamarinHandsOn.Models
{
    public class Memo : BindableBase
    {
        // プロパティ
        public string Id { get; }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private string _text;
        public string Text
        {
            get { return _text; }
            set { SetProperty(ref _text, value); }
        }

        // コンストラクタ
        public Memo()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
