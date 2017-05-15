using Prism.Commands;
using Prism.Mvvm;
using System.Windows.Input;
using Xamarin.Forms;

namespace Wibci.Forms.Controls.Samples
{
    public class MainPageViewModel : BindableBase
    {
        public MainPageViewModel()
        {
            ShowGridViewPageCommand = new DelegateCommand(ShowGridViewPage);
        }

        public ICommand ShowGridViewPageCommand { get; private set; }

        private async void ShowGridViewPage()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new GridViewPage());
        }
    }
}