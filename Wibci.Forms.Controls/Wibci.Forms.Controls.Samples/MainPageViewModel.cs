using Prism.Commands;
using Prism.Mvvm;
using System.Windows.Input;
using Wibci.Forms.Controls.Samples.SwitchView;
using Xamarin.Forms;

namespace Wibci.Forms.Controls.Samples
{
    public class MainPageViewModel : BindableBase
    {
        public MainPageViewModel()
        {
            ShowGridViewPageCommand = new DelegateCommand(ShowGridViewPage);
            ShowSwitchViewPageCommand = new DelegateCommand(ShowSwitchViewPage);
            ShowEntryViewPageCommand = new DelegateCommand(ShowEntryViewPage);
        }

        public ICommand ShowEntryViewPageCommand { get; private set; }

        public ICommand ShowGridViewPageCommand { get; private set; }

        public ICommand ShowSwitchViewPageCommand { get; private set; }

        private async void ShowEntryViewPage()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new EntryViewPage());
        }

        private async void ShowGridViewPage()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new GridViewPage());
        }

        private async void ShowSwitchViewPage()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new SwitchViewPage());
        }
    }
}