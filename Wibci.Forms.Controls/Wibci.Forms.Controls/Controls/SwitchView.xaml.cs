using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Wibci.Forms.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SwitchView : ContentView
    {
        public static readonly BindableProperty IsToggledProperty =
                            BindableProperty.Create(nameof(IsToggled), typeof(bool), typeof(SwitchView), false, BindingMode.TwoWay, null, (bindable, oldValue, newValue) =>
                            {
                                var ctrl = (SwitchView)bindable;
                                ctrl.IsToggled = (bool)newValue;
                            });

        public static readonly BindableProperty OffDescriptionProperty = BindableProperty.Create(nameof(OffDescription), typeof(string), typeof(SwitchView), null, BindingMode.TwoWay, null, (bindable, oldValue, newValue) =>
        {
            var ctrl = (SwitchView)bindable;
            ctrl.ApplyText();
        });

        public static readonly BindableProperty OffTextProperty = BindableProperty.Create(nameof(OffText), typeof(string), typeof(SwitchView), "off", BindingMode.TwoWay, null, (bindable, oldValue, newValue) =>
        {
            var ctrl = (SwitchView)bindable;
            ctrl.ApplyText();
        });

        public static readonly BindableProperty OnDescriptionProperty = BindableProperty.Create(nameof(OnDescription), typeof(string), typeof(SwitchView), null, BindingMode.TwoWay, null, (bindable, oldValue, newValue) =>
        {
            var ctrl = (SwitchView)bindable;
            ctrl.ApplyText();
        });

        public static readonly BindableProperty OnTextProperty = BindableProperty.Create(nameof(OnText), typeof(string), typeof(SwitchView), "on", BindingMode.TwoWay, null, (bindable, oldValue, newValue) =>
        {
            var ctrl = (SwitchView)bindable;
            ctrl.ApplyText();
        });

        public static readonly BindableProperty ShowDescriptionProperty = BindableProperty.Create(nameof(ShowDescription), typeof(bool), typeof(SwitchView), false, BindingMode.TwoWay, null, (bindable, oldValue, newValue) =>
        {
            var ctrl = (SwitchView)bindable;
            ctrl.ApplyText();
        });

        public SwitchView()
        {
            InitializeComponent();
        }

        public bool IsToggled
        {
            get { return (bool)GetValue(IsToggledProperty); }
            set
            {
                SetValue(IsToggledProperty, value);
                _switch.IsToggled = value;
                ApplyText();
            }
        }

        public string OffDescription
        {
            get { return (string)GetValue(OffDescriptionProperty); }
            set
            {
                SetValue(OffDescriptionProperty, value);
                ApplyText();
            }
        }

        public string OffText
        {
            get { return (string)GetValue(OffTextProperty); }
            set
            {
                SetValue(OffTextProperty, value);
                ApplyText();
            }
        }

        public string OnDescription
        {
            get { return (string)GetValue(OnDescriptionProperty); }
            set
            {
                SetValue(OnDescriptionProperty, value);
                ApplyText();
            }
        }

        public string OnText
        {
            get { return (string)GetValue(OnTextProperty); }
            set
            {
                SetValue(OnTextProperty, value);
                ApplyText();
            }
        }

        public bool ShowDescription
        {
            get { return (bool)GetValue(ShowDescriptionProperty); }
            set
            {
                SetValue(ShowDescriptionProperty, value);
                ApplyText();
            }
        }

        private void ApplyText()
        {
            _label.Text = IsToggled ? OnText : OffText;
            string description = IsToggled ? OnDescription : OffDescription;
            _lblDescription.Text = description;
            _lblDescription.IsVisible = ShowDescription && !string.IsNullOrEmpty(description);
        }
    }
}