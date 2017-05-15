using System.Windows.Input;
using Xamarin.Forms;

namespace Wibci.Forms.Controls
{
    /// <summary>
    /// Extended ListView
    /// Support for binding a Command to ItemClick event
    /// Defaults caching strategy to RecycleElement
    /// </summary>
    public class ListViewEx : ListView
    {
        public static BindableProperty ItemClickCommandProperty = BindableProperty.Create("ItemClickCommand", typeof(ICommand), typeof(ListView), null);

        public ListViewEx() : base(ListViewCachingStrategy.RecycleElement)
        {
            this.ItemTapped += this.OnItemTapped;
        }

        public ListViewEx(ListViewCachingStrategy cacheStrategy) : base(cacheStrategy)
        {
            this.ItemTapped += this.OnItemTapped;
        }

        public ICommand ItemClickCommand
        {
            get { return (ICommand)this.GetValue(ItemClickCommandProperty); }
            set { this.SetValue(ItemClickCommandProperty, value); }
        }

        private void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item != null && this.ItemClickCommand != null && this.ItemClickCommand.CanExecute(e.Item))
            {
                this.ItemClickCommand.Execute(e.Item);
                this.SelectedItem = null;
            }
        }
    }
}