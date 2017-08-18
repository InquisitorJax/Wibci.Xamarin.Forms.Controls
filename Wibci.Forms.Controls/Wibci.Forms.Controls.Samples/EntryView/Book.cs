using Prism.Mvvm;

namespace Wibci.Forms.Controls.Samples
{
    public class Book : BindableBase
    {
        private string _author;
        private int _referenceNumber;
        private string _secret;
        private string _title;

        public string Author
        {
            get { return _author; }
            set { SetProperty(ref _author, value); }
        }

        public int ReferenceNumber
        {
            get { return _referenceNumber; }
            set { SetProperty(ref _referenceNumber, value); }
        }

        public string Secret
        {
            get { return _secret; }
            set { SetProperty(ref _secret, value); }
        }

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
    }
}