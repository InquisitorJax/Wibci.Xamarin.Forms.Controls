using System.Windows.Input;
using Xamarin.Forms;

namespace Wibci.Forms.Controls
{
    public partial class AddStringEntry : ContentView
    {
        public static readonly BindableProperty PlaceholderTextProperty = BindableProperty.Create(nameof(PlaceholderText), typeof(string), typeof(AddStringEntry), "add new item");

        public static BindableProperty AddItemCommandProperty = BindableProperty.Create(nameof(AddItemCommand), typeof(ICommand), typeof(AddStringEntry), default(ICommand));

        public static BindableProperty NewStringItemProperty = BindableProperty.Create(nameof(NewStringItem), typeof(string), typeof(AddStringEntry), null);

        public AddStringEntry()
        {
            InitializeComponent();
        }

        public ICommand AddItemCommand
        {
            get { return (ICommand)GetValue(AddItemCommandProperty); }
            set { SetValue(AddItemCommandProperty, value); }
        }

        public string NewStringItem
        {
            get { return (string)GetValue(NewStringItemProperty); }
            set { SetValue(NewStringItemProperty, value); }
        }

        public string PlaceholderText
        {
            get { return (string)GetValue(PlaceholderTextProperty); }
            set { SetValue(PlaceholderTextProperty, value); }
        }
    }
}