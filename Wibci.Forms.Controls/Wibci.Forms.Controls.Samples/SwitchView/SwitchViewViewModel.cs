using Prism.Mvvm;

namespace Wibci.Forms.Controls.Samples.SwitchView
{
    public class SwitchViewViewModel : BindableBase
    {
        private bool _flash;
        private bool _iLikeSuperheroes;

        public bool Flash
        {
            get { return _flash; }
            set { SetProperty(ref _flash, value); }
        }

        public bool ILikeSuperheroes
        {
            get { return _iLikeSuperheroes; }
            set { SetProperty(ref _iLikeSuperheroes, value); }
        }
    }
}