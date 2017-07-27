using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Wibci.Forms.Controls.Samples.SwitchView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SwitchViewPage : ContentPage
    {
        private SwitchViewViewModel _viewModel;

        public SwitchViewPage()
        {
            InitializeComponent();
            _viewModel = new SwitchViewViewModel();
            BindingContext = _viewModel; ;
            Title = "SwitchView";
        }
    }
}