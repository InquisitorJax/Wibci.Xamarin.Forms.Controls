using Prism.Commands;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Wibci.Forms.Controls.Samples.Models;
using Xamarin.Forms;

namespace Wibci.Forms.Controls.Samples.GridView
{
    public class GridViewViewModel : BindableBase
    {
        private int _maxColumns;
        private ObservableCollection<ParentItemModel> _parentModels;

        private float _tileHeight;

        public GridViewViewModel()
        {
            ItemTapCommand = new DelegateCommand<ParentItemModel>(OnParentTapped);
            MaxColumns = 2;
            TileHeight = 100;
        }

        public ICommand ItemTapCommand { get; private set; }

        public int MaxColumns
        {
            get { return _maxColumns; }
            set { SetProperty(ref _maxColumns, value); }
        }

        public ObservableCollection<ParentItemModel> ParentModels
        {
            get { return _parentModels; }
            set { SetProperty(ref _parentModels, value); }
        }

        public float TileHeight
        {
            get { return _tileHeight; }
            set { SetProperty(ref _tileHeight, value); }
        }

        internal void LoadData()
        {
            CreateStubData();
        }

        private ChildItemModel CreateChildModel(string name)
        {
            return new ChildItemModel
            {
                Name = "Child of " + name,
                Description = "Description for: " + name
            };
        }

        private ParentItemModel CreateParentModel(string name)
        {
            var parent = new ParentItemModel
            {
                Name = name,
                Description = name + " Description"
            };

            var children = new List<ChildItemModel>
            {
                CreateChildModel(name + "_A"),
                CreateChildModel(name + "_B"),
                CreateChildModel(name + "_C")
            };

            parent.Children = new ObservableCollection<ChildItemModel>(children);

            return parent;
        }

        private void CreateStubData()
        {
            var parentModels = new List<ParentItemModel>
            {
                CreateParentModel("P_1"),
                CreateParentModel("P_2"),
                CreateParentModel("P_3"),
                CreateParentModel("P_4"),
                CreateParentModel("P_5"),
                CreateParentModel("P_6")
            };

            ParentModels = new ObservableCollection<ParentItemModel>(parentModels);
        }

        private void OnParentTapped(ParentItemModel item)
        {
            Application.Current.MainPage.DisplayAlert("Eurika!!!", "You tapped " + item.Name, "Cancel");
        }
    }
}