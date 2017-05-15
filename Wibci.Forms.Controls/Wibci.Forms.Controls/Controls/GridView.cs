using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace Wibci.Forms.Controls
{
    //see: https://chaseflorell.github.io/xamarin/2015/03/14/xamarinforms-gridview/

    /// <summary>
    /// GridView for showing control templates in a tile-like layout
    /// NOTE: Not designed for many items in the ItemsSource, as cached Recycling is not built in like the ListView
    /// </summary>
    public class GridView : Grid
    {
        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(nameof(ItemsSource), typeof(IList), typeof(GridView), default(IList), BindingMode.TwoWay);
        public static readonly BindableProperty ItemTappedCommandProperty = BindableProperty.Create(nameof(ItemTappedCommand), typeof(ICommand), typeof(GridView), null);
        public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create(nameof(ItemTemplate), typeof(DataTemplate), typeof(GridView), default(DataTemplate));
        public static readonly BindableProperty MaxColumnsProperty = BindableProperty.Create(nameof(MaxColumns), typeof(int), typeof(GridView), 2);
        public static readonly BindableProperty TileHeightProperty = BindableProperty.Create(nameof(TileHeight), typeof(float), typeof(GridView), 100f);

        public GridView()
        {
            PropertyChanged += GridView_PropertyChanged;
            PropertyChanging += GridView_PropertyChanging;
        }

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
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }

        public int MaxColumns
        {
            get { return (int)GetValue(MaxColumnsProperty); }
            set { SetValue(MaxColumnsProperty, value); }
        }

        public float TileHeight
        {
            get { return (float)GetValue(TileHeightProperty); }
            set { SetValue(TileHeightProperty, value); }
        }

        private void BuildColumns()
        {
            ColumnDefinitions.Clear();
            for (var i = 0; i < MaxColumns; i++)
            {
                ColumnDefinitions.Add(new ColumnDefinition());
            }
        }

        private View BuildTile(object item1)
        {
            var template = ItemTemplate.CreateContent() as View;
            template.BindingContext = item1;

            var tapGestureRecognizer = new TapGestureRecognizer
            {
                Command = ItemTappedCommand,
                CommandParameter = item1
            };

            template.GestureRecognizers.Add(tapGestureRecognizer);
            return template;
        }

        private void BuildTiles()
        {
            // Wipe out the previous row & Column definitions if they're there.
            if (RowDefinitions.Any())
            {
                RowDefinitions.Clear();
            }

            BuildColumns();

            Children.Clear();

            var tiles = ItemsSource;

            var numberOfRows = Math.Ceiling(tiles.Count / (float)MaxColumns);
            for (var i = 0; i < numberOfRows; i++)
            {
                RowDefinitions.Add(new RowDefinition { Height = TileHeight });
            }

            for (var index = 0; index < tiles.Count; index++)
            {
                var column = index % MaxColumns;
                var row = (int)Math.Floor(index / (float)MaxColumns);

                var tile = BuildTile(tiles[index]);

                Children.Add(tile, column, row);
            }
        }

        private void GridView_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == ItemsSourceProperty.PropertyName)
            {
                var items = ItemsSource as INotifyCollectionChanged;
                if (items != null)
                    items.CollectionChanged += ItemsCollectionChanged;

                BuildTiles();
            }

            if (e.PropertyName == MaxColumnsProperty.PropertyName || e.PropertyName == TileHeightProperty.PropertyName)
            {
                BuildTiles();
            }
        }

        private void GridView_PropertyChanging(object sender, PropertyChangingEventArgs e)
        {
            if (e.PropertyName == ItemsSourceProperty.PropertyName)
            {
                var items = ItemsSource as INotifyCollectionChanged;
                if (items != null)
                    items.CollectionChanged -= ItemsCollectionChanged;
            }
        }

        private void ItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            BuildTiles();
        }
    }
}