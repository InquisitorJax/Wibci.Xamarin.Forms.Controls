using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace Wibci.Forms.Controls.Behaviors
{
    /*
     *	<Entry.Behaviors>
            <behaviour:EntryCompletedCommandBehavior CompletedCommand="{Binding SaveCommand}"/>
        </Entry.Behaviors>
     */

    public class EntryCompletedCommandBehavior : Behavior<Entry>
    {
        public static readonly BindableProperty CompletedCommandProperty = BindableProperty.Create("CompletedCommand", typeof(ICommand), typeof(EntryCompletedCommandBehavior), null, BindingMode.OneWay);

        private Entry _entry;

        public ICommand CompletedCommand
        {
            get { return (ICommand)GetValue(CompletedCommandProperty); }
            set { SetValue(CompletedCommandProperty, value); }
        }

        protected override void OnAttachedTo(Entry bindable)
        {
            base.OnAttachedTo(bindable);

            _entry = bindable;
            if (_entry != null)
            {
                _entry.Completed += Entry_Completed;
            }
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            base.OnDetachingFrom(bindable);

            if (_entry != null)
            {
                _entry.Completed -= Entry_Completed;
            }
        }

        private void Entry_Completed(object sender, EventArgs e)
        {
            var entry = (Entry)sender;
            if (CompletedCommand != null && CompletedCommand.CanExecute(null))
            {
                CompletedCommand.Execute(entry.Text);
            }
        }
    }
}