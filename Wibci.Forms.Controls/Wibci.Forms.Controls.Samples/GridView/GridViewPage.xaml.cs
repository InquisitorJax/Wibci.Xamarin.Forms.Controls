using Wibci.Forms.Controls.Samples.GridView;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Wibci.Forms.Controls.Samples
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GridViewPage : ContentPage
    {
        private GridViewViewModel _viewModel;

        public GridViewPage()
        {
            InitializeComponent();
            _viewModel = new GridViewViewModel();
            BindingContext = _viewModel; ;
            Title = "GridView";
        }

        protected override void OnAppearing()
        {
            _viewModel.LoadData();
        }
    }
}