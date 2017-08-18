using Prism.Mvvm;

namespace Wibci.Forms.Controls.Samples
{
    public class EntryViewViewModel : BindableBase
    {
        private Book _model;

        public EntryViewViewModel()
        {
            Model = new Book();
        }

        public Book Model
        {
            get { return _model; }
            set { SetProperty(ref _model, value); }
        }
    }
}