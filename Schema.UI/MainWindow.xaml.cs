namespace Schema.UI
{
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media.Imaging;

    using QuickGraph;

    using Schema.Core.Models;
    using Schema.Core.Models.Table;
    using Schema.UI.Table;

    //  using Schema.UI.MoveResize;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ColumnDefinition column1CloneForLayer0;
        private ColumnDefinition column2CloneForLayer0;
        private ColumnDefinition column2CloneForLayer1;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
           this.CreateGraph();

            this.column1CloneForLayer0 = new ColumnDefinition { SharedSizeGroup = "column1" };
            this.column2CloneForLayer0 = new ColumnDefinition { SharedSizeGroup = "column2" };
            this.column2CloneForLayer1 = new ColumnDefinition { SharedSizeGroup = "column2" };
        }

        public static ChooseMsSqlServerWindow serverWindow;

        public readonly BidirectionalGraph<object, IEdge<object>> _graph = new BidirectionalGraph<object, IEdge<object>>();

        public IBidirectionalGraph<object, IEdge<object>> Graph
        {
            get { return this._graph; }
        }

        //"EfficientSugiyama";//{ "Circular", "Tree", "FR", "BoundedFR", "KK", "ISOM", "LinLog", "EfficientSugiyama", "Sugiyama", "CompoundFDP" };
        private string layoutAlgorithm = "CompoundFDP";

        public string LayoutAlgorithm
        {
            get
            {
                return this.layoutAlgorithm;
            }

            set
            {
                if (value != this.layoutAlgorithm)
                {
                    this.layoutAlgorithm = value;
                }
            }
        }


        private void CreateGraph()
        {
            var obj1 = new TableVertex("One");
            this._graph.AddVertex(obj1);
            var obj2=new TableVertex("Two");
            this._graph.AddVertex(obj2);
              mainWindow._graph.AddEdge(new Edge<object>(obj1, obj2));

        }

        private void OpenMySqlServers(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("TODO");
        }

        private void OpenMsSqlServers(object sender, RoutedEventArgs e)
        {
            serverWindow = new ChooseMsSqlServerWindow { Owner = this };
            serverWindow.Show();
        }

        private void ExitClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }


        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var menuItem = sender as MenuItem;
            var vertex = menuItem.Tag as TableVertex;
            if (vertex != null)
            {
                vertex.Change();
            }
        }

        public void panePropertiesPin_Click(object sender, RoutedEventArgs e)
        {
            if (this.panePropertiesButton.Visibility == Visibility.Collapsed)
            {
                this.UndockPane(1);
            }
            else
            {
                this.DockPane(1);
            }
        }

        public void panePropertiesButton_MouseEnter(object sender, RoutedEventArgs e)
        {
            this.layerProperties.Visibility = Visibility.Visible;
            Panel.SetZIndex(this.layerProperties, 1);
             Panel.SetZIndex(layer2, 0);
         if (pane2Button.Visibility == Visibility.Visible)
         {
             layer2.Visibility = Visibility.Collapsed;
         }
        }

        public void layerMain_MouseEnter(object sender, RoutedEventArgs e)
        {
            if (this.panePropertiesButton.Visibility == Visibility.Visible)

            {
                this.layerProperties.Visibility = Visibility.Collapsed;
            }
            if (pane2Button.Visibility == Visibility.Visible)
            {
                layer2.Visibility = Visibility.Collapsed;
            }
        }

        public void DockPane(int paneNumber)
        {
            if (paneNumber == 1)
            {
                this.panePropertiesButton.Visibility = Visibility.Collapsed;
                this.layerMain.ColumnDefinitions.Add(this.column1CloneForLayer0);
                this.panePropertiesPinImage.Source = new BitmapImage(this.GetUriString("Images/pin.png"));

                if (pane2Button.Visibility == Visibility.Collapsed)
                {
                    this.layerProperties.ColumnDefinitions.Add(this.column2CloneForLayer1);
                }
            }
            if (paneNumber == 2)
            {
                pane2Button.Visibility = Visibility.Collapsed;
                pane2PinImage.Source = new BitmapImage(this.GetUriString("Images/pin.png"));
                this.layerMain.ColumnDefinitions.Add(this.column2CloneForLayer0);
                if (panePropertiesButton.Visibility == Visibility.Collapsed)
                {
                    layerProperties.ColumnDefinitions.Add(this.column2CloneForLayer1);
                }
            }
        }

        public void UndockPane(int paneNumber)
        {
            if (paneNumber == 1)
            {
                this.layerProperties.Visibility = Visibility.Visible;
                this.panePropertiesButton.Visibility = Visibility.Visible;
                this.panePropertiesPinImage.Source = new BitmapImage(this.GetUriString("Images/unpin.png"));
                this.layerMain.ColumnDefinitions.Remove(this.column1CloneForLayer0);
                this.layerProperties.ColumnDefinitions.Remove(this.column2CloneForLayer1);
            }
            if (paneNumber == 2)
            {
                layer2.Visibility = Visibility.Visible;
                pane2Button.Visibility = Visibility.Visible;
                pane2PinImage.Source = new BitmapImage(this.GetUriString("Images/unpin.png"));
                this.layerMain.ColumnDefinitions.Remove(this.column2CloneForLayer0);
                this.layerProperties.ColumnDefinitions.Remove(this.column2CloneForLayer1);
            }
        }

        private void Properties_Click(object sender, RoutedEventArgs e)
        {
            var menuItem = sender as MenuItem;
            var vertex = menuItem.Tag as TableVertex;
            PropertyTree.Children.Clear();
            TableName.Children.Clear();
            if (vertex != null && vertex.Model != null)
            {
                vertex.Change();
               TableName.Children.Add(new TextBlock { Text = vertex.Model.Name, Margin = new Thickness(10, 5, 5, 5)});
                PropertyTree.Children.Add(this.PropertiesTreeView(vertex.Model));
            }
            else
            {
                TableName.Children.Add(new TextBlock { Text = "No info", Margin = new Thickness(10, 5, 5, 5) });
            }
           
            this.layerProperties.Visibility = Visibility.Visible;
            Panel.SetZIndex(this.layerProperties, 1);
        }

        public TreeViewItem GetColumnItem(List<ColumnModel> columns, Uri mainImageUri)
        {
            if (columns == null) return null;
            var columnItem = this.GetTreeViewItem("Columns", mainImageUri);
            foreach (var column in columns)
            {
                var allowNull = column.AllowNull ? "null" : "not null";
                var imageUri = this.GetUriString("Images/table.png");
                var item = this.GetTreeViewItem(string.Format("{0} ({1} , {2})", column.ColumnName, column.TypeName, allowNull), imageUri);
                columnItem.Items.Add(item);
            }
            return columnItem;
        }

        public TreeViewItem GetKeyItem(List<KeyModel> keys, Uri mainImageUri)
        {
            if (keys == null) return null;
            var keyItem = this.GetTreeViewItem("Keys", mainImageUri);
            foreach (var key in keys)
            {
                var imageUri = this.GetUriString("Images/key.png");
                var item = this.GetTreeViewItem(key.Name, imageUri);
                keyItem.Items.Add(item);
            }
            return keyItem;
        }

        public TreeViewItem GetTriggerItem(List<TriggerModel> triggerModels,Uri folderUri )
        {
            if (triggerModels == null) return null;
            var triggerItem = this.GetTreeViewItem("Triggers", folderUri);
            foreach (var trigger in triggerModels)
            {
                var imageUri = this.GetUriString("Images/trigger.png");
                var item = this.GetTreeViewItem(string.Format("{0} ({1})", trigger.TrigerName, trigger.Event), imageUri);

                triggerItem.Items.Add(item);
            }
            return triggerItem;
        }
        public TreeViewItem GetIndexItem(List< IndexModel> indexModels, Uri folderUri )
        {
            if (indexModels == null) return null;
            var indexItem = this.GetTreeViewItem("Indexes", folderUri);
            foreach (var index in indexModels)
            {
                var imageUri = this.GetUriString("Images/index.png");
                var item = this.GetTreeViewItem(
                    string.Format("{0} ({1})", index.Name, index.IsDescending), imageUri);
                indexItem.Items.Add(item);
            }
            return indexItem;
        }

        public TreeView PropertiesTreeView(TableModel model)
        {
            var tree = new TreeView();
            var folderUri = this.GetUriString("Images/folder.png");
            if (model.Columns != null)
            {
                tree.Items.Add(this.GetColumnItem(model.Columns, folderUri));
            }
            
            if (model.Keys != null)
            {
                tree.Items.Add(this.GetKeyItem(model.Keys, folderUri));
            }
            
            if (model.Trigers != null)
            {
               tree.Items.Add(this.GetTriggerItem(model.Trigers, folderUri));
            }

            if (model.Indexes != null)
            {
               tree.Items.Add(this.GetIndexItem(model.Indexes,folderUri));
            }

            return tree;
        }

        public TreeViewItem GetTreeViewItem(string text, Uri imageUri)
        {
            var item = new TreeViewItem();
            var stackPanel = new StackPanel { Orientation = Orientation.Horizontal };
            var image = new Image { Source = new BitmapImage(imageUri) };
            var textBlock = new TextBlock
                {
                    Text = text,
                    Margin = new Thickness(20, 5, 5, 10)
                };
            stackPanel.Children.Add(image);
            stackPanel.Children.Add(textBlock);
            item.Header = stackPanel;
            return item;
        }

        public Uri GetUriString(string imagePath)
        {
            return new Uri("pack://application:,,,/Schema.UI;component/" + imagePath);
        }

        private void pane2_MouseEnter(object sender, MouseEventArgs e)
        {
            if (panePropertiesButton.Visibility == Visibility.Visible)
            {
                layerProperties.Visibility = Visibility.Collapsed;
            }
        }

        private void pane2Pin_Click(object sender, RoutedEventArgs e)
        {
            if (pane2Button.Visibility == Visibility.Collapsed)
            {
                this.UndockPane(2);
            }
            else
            {
                this.DockPane(2);
            }
        }


        public void pane2Button_MouseEnter(object sender, RoutedEventArgs e)
        {
            layer2.Visibility = Visibility.Visible;
            Panel.SetZIndex(layer2, 1);
            Panel.SetZIndex(layerProperties, 0);
            
        if (panePropertiesButton.Visibility == Visibility.Visible)
            {
                layerProperties.Visibility = Visibility.Collapsed;
            }
        }

        public void pane1_MouseEnter(object sender, RoutedEventArgs e)
        {
            if (pane2Button.Visibility == Visibility.Visible)
            {
                layer2.Visibility = Visibility.Collapsed;
            }
        }
    }
}
