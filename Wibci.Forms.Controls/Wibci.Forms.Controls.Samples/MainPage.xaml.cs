using Xamarin.Forms;

namespace Wibci.Forms.Controls.Samples
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainPageViewModel();
            Title = "WIBCI XAMARIN FORMS CONTROLS";
        }
    }
}