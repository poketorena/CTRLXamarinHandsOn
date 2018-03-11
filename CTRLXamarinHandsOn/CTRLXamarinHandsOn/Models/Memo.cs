using Prism.Mvvm;
using System;

namespace CTRLXamarinHandsOn.Models
{
    public class Memo : BindableBase
    {
        // プロパティ
        private string _id;
        public string Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }

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
