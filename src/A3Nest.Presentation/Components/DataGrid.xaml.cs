using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace A3Nest.Presentation.Components;

public partial class DataGrid : ContentView
{
    public static readonly BindableProperty ItemsSourceProperty =
        BindableProperty.Create(
            nameof(ItemsSource),
            typeof(IEnumerable),
            typeof(DataGrid),
            null,
            propertyChanged: OnItemsSourceChanged);

    public static readonly BindableProperty ColumnsProperty =
        BindableProperty.Create(
            nameof(Columns),
            typeof(ObservableCollection<DataGridColumn>),
            typeof(DataGrid),
            null,
            propertyChanged: OnColumnsChanged);

    public IEnumerable ItemsSource
    {
        get => (IEnumerable)GetValue(ItemsSourceProperty);
        set => SetValue(ItemsSourceProperty, value);
    }

    public ObservableCollection<DataGridColumn> Columns
    {
        get => (ObservableCollection<DataGridColumn>)GetValue(ColumnsProperty);
        set => SetValue(ColumnsProperty, value);
    }

    public DataGrid()
    {
        InitializeComponent();
        Columns = new ObservableCollection<DataGridColumn>();
        Columns.CollectionChanged += OnColumnsCollectionChanged;
    }

    private static void OnItemsSourceChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is DataGrid dataGrid)
        {
            dataGrid.UpdateDataGrid();
        }
    }

    private static void OnColumnsChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is DataGrid dataGrid)
        {
            if (oldValue is ObservableCollection<DataGridColumn> oldColumns)
            {
                oldColumns.CollectionChanged -= dataGrid.OnColumnsCollectionChanged;
            }

            if (newValue is ObservableCollection<DataGridColumn> newColumns)
            {
                newColumns.CollectionChanged += dataGrid.OnColumnsCollectionChanged;
            }

            dataGrid.UpdateDataGrid();
        }
    }

    private void OnColumnsCollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        UpdateDataGrid();
    }

    private void UpdateDataGrid()
    {
        if (Columns == null || !Columns.Any())
            return;

        // Create header row
        CreateHeaderRow();
        
        // Update collection view item template
        UpdateItemTemplate();
    }

    private void CreateHeaderRow()
    {
        var headerGrid = new Grid
        {
            BackgroundColor = Colors.LightGray,
            Padding = new Thickness(8, 12)
        };

        // Define columns
        for (int i = 0; i < Columns.Count; i++)
        {
            headerGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
        }

        // Add header labels
        for (int i = 0; i < Columns.Count; i++)
        {
            var column = Columns[i];
            var headerLabel = new Label
            {
                Text = column.Header,
                FontAttributes = FontAttributes.Bold,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Start
            };

            Grid.SetColumn(headerLabel, i);
            headerGrid.Children.Add(headerLabel);
        }

        // Clear existing header and add new one
        DataGridContainer.Children.Clear();
        DataGridContainer.RowDefinitions.Clear();
        DataGridContainer.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        DataGridContainer.RowDefinitions.Add(new RowDefinition { Height = GridLength.Star });

        Grid.SetRow(headerGrid, 0);
        DataGridContainer.Children.Add(headerGrid);

        Grid.SetRow(ItemsCollectionView, 1);
        DataGridContainer.Children.Add(ItemsCollectionView);
    }

    private void UpdateItemTemplate()
    {
        var dataTemplate = new DataTemplate(() =>
        {
            var grid = new Grid
            {
                Padding = new Thickness(8, 8)
            };

            // Define columns to match header
            for (int i = 0; i < Columns.Count; i++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
            }

            // Add data labels
            for (int i = 0; i < Columns.Count; i++)
            {
                var column = Columns[i];
                var label = new Label
                {
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Start
                };

                if (!string.IsNullOrEmpty(column.PropertyName))
                {
                    label.SetBinding(Label.TextProperty, column.PropertyName);
                }

                Grid.SetColumn(label, i);
                grid.Children.Add(label);
            }

            return new ViewCell { View = grid };
        });

        ItemsCollectionView.ItemTemplate = dataTemplate;
    }
}

public class DataGridColumn
{
    public string Header { get; set; } = string.Empty;
    public string PropertyName { get; set; } = string.Empty;
    public double Width { get; set; } = 100;
}