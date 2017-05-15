using Prism.Mvvm;

namespace Wibci.Forms.Controls.Samples.Models
{
    public class ChildItemModel : BindableBase
    {
        private string _description;
        private string _name;

        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }

        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }
    }
}