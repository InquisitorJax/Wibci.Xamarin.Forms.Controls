using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace Wibci.Forms.Controls
{
    /// <summary>
    /// WrapLayoutView: Display UI elements in a wrap layout
    /// Support for custom command on item tap
    /// </summary>
    public class WrapLayoutView : Layout<View>, IDisposable
    {
        //see: http://www.spikie.be/blog/post/2015/04/02/.aspx & https://github.com/daniel-luberda/DLToolkit.Forms.Controls/tree/master/TagEntryView

        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(nameof(ItemsSource), typeof(IList), typeof(WrapLayoutView), default(IList), BindingMode.TwoWay);

        public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create(nameof(ItemTemplate), typeof(DataTemplate), typeof(WrapLayoutView), default(DataTemplate));

        public static readonly BindableProperty SpacingProperty = BindableProperty.Create(nameof(Spacing), typeof(double), typeof(WrapLayoutView), 6d,
                propertyChanged: (bindable, oldvalue, newvalue) => ((WrapLayoutView)bindable).OnSizeChanged());

        public static BindableProperty ItemTappedCommandProperty = BindableProperty.Create(nameof(ItemTappedCommand), typeof(ICommand), typeof(WrapLayoutView), default(ICommand));

        public WrapLayoutView()
        {
            PropertyChanged += WrapLayoutViewPropertyChanged;
            PropertyChanging += WrapLayotViewPropertyChanging;
        }

        public event EventHandler<ItemTappedEventArgs> ItemTapped;

        public IList ItemsSource
        {
            get { return (IList)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public ICommand ItemTappedCommand
        {
            get { return (ICommand)GetValue(ItemTappedCommandProperty); }
            set { SetValue(ItemTappedCommandProperty, value); }
        }

        public DataTemplate ItemTemplate
        {
            get
            {
                return (DataTemplate)GetValue(ItemTemplateProperty);
            }
            set
            {
                SetValue(ItemTemplateProperty, value);
            }
        }

        public double Spacing
        {
            get { return (double)GetValue(SpacingProperty); }
            set { SetValue(SpacingProperty, value); }
        }

        public void Dispose()
        {
            PropertyChanged -= WrapLayoutViewPropertyChanged;
            PropertyChanging -= WrapLayotViewPropertyChanging;

            var items = ItemsSource as INotifyCollectionChanged;
            if (items != null)
            {
                items.CollectionChanged -= ItemsCollectionChanged;
            }
        }

        public void ForceReload()
        {
            Children.Clear();

            for (int i = 0; i < ItemsSource.Count; i++)
            {
                View view = null;

                var templateSelector = ItemTemplate as DataTemplateSelector;
                if (templateSelector != null)
                {
                    var template = templateSelector.SelectTemplate(ItemsSource[i], null);
                    view = (View)template.CreateContent();
                }
                else
                {
                    view = (View)ItemTemplate.CreateContent();
                }

                view.BindingContext = ItemsSource[i];

                view.GestureRecognizers.Add(new TapGestureRecognizer()
                {
                    Command = new Command(() => PerformItemTap(view.BindingContext))
                });

                Children.Add(view);
            }
        }

        internal void PerformItemTap(object item)
        {
            EventHandler<ItemTappedEventArgs> handler = ItemTapped;
            if (handler != null) handler(this, new ItemTappedEventArgs(null, item));

            var command = ItemTappedCommand;
            if (command != null && command.CanExecute(item))
            {
                command.Execute(item);
            }
        }

        protected override void LayoutChildren(double x, double y, double width, double height)
        {
            double rowHeight = 0;
            double yPos = y, xPos = x;

            foreach (var child in Children.Where(c => c.IsVisible))
            {
                var request = child.Measure(width, height);

                double childWidth = request.Request.Width;
                double childHeight = request.Request.Height;

                rowHeight = Math.Max(rowHeight, childHeight);

                if (xPos + childWidth > width)
                {
                    xPos = x;
                    yPos += rowHeight + Spacing;
                    rowHeight = 0;
                }

                var region = new Rectangle(xPos, yPos, childWidth, childHeight);
                LayoutChildIntoBoundingRegion(child, region);
                xPos += region.Width + Spacing;
            }
        }

        protected override SizeRequest OnMeasure(double widthConstraint, double heightConstraint)
        {
            if (WidthRequest > 0)
                widthConstraint = Math.Min(widthConstraint, WidthRequest);
            if (HeightRequest > 0)
                heightConstraint = Math.Min(heightConstraint, HeightRequest);

            double internalWidth = double.IsPositiveInfinity(widthConstraint) ? double.PositiveInfinity : Math.Max(0, widthConstraint);
            double internalHeight = double.IsPositiveInfinity(heightConstraint) ? double.PositiveInfinity : Math.Max(0, heightConstraint);

            return DoHorizontalMeasure(internalWidth, internalHeight);
        }

        private SizeRequest DoHorizontalMeasure(double widthConstraint, double heightConstraint)
        {
            int rowCount = 1;

            double width = 0;
            double height = 0;
            double minWidth = 0;
            double minHeight = 0;
            double widthUsed = 0;

            foreach (var item in Children)
            {
                var size = item.Measure(widthConstraint, heightConstraint);
                height = Math.Max(height, size.Request.Height);

                var newWidth = width + size.Request.Width + Spacing;
                if (newWidth > widthConstraint)
                {
                    rowCount++;
                    widthUsed = Math.Max(width, widthUsed);
                    width = size.Request.Width;
                }
                else
                    width = newWidth;

                minHeight = Math.Max(minHeight, size.Minimum.Height);
                minWidth = Math.Max(minWidth, size.Minimum.Width);
            }

            if (rowCount > 1)
            {
                width = Math.Max(width, widthUsed);
                height = (height + Spacing) * rowCount - Spacing; // via MitchMilam
            }

            return new SizeRequest(new Size(width, height), new Size(minWidth, minHeight));
        }

        private void ItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            ForceReload();
        }

        private void OnSizeChanged()
        {
            this.ForceLayout();
        }

        private void WrapLayotViewPropertyChanging(object sender, PropertyChangingEventArgs e)
        {
            if (e.PropertyName == ItemsSourceProperty.PropertyName)
            {
                var items = ItemsSource as INotifyCollectionChanged;
                if (items != null)
                    items.CollectionChanged -= ItemsCollectionChanged;
            }
        }

        private void WrapLayoutViewPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == ItemsSourceProperty.PropertyName)
            {
                var items = ItemsSource as INotifyCollectionChanged;
                if (items != null)
                    items.CollectionChanged += ItemsCollectionChanged;

                ForceReload();
            }
        }
    }
}