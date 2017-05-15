using Prism.Mvvm;
using System.Collections.ObjectModel;

namespace Wibci.Forms.Controls.Samples.Models
{
    public class ParentItemModel : BindableBase
    {
        private ObservableCollection<ChildItemModel> _children;
        private string _description;
        private string _name;

        public ObservableCollection<ChildItemModel> Children
        {
            get { return _children; }
            set { SetProperty(ref _children, value); }
        }

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