using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Wibci.Forms.Controls.Samples
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EntryViewPage : ContentPage
    {
        public EntryViewPage()
        {
            InitializeComponent();
            BindingContext = new EntryViewViewModel();
        }
    }
}