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
    using Schema.UI.Helpers;
    using Schema.UI.Table;

    using Shema.Server;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
      public IBidirectionalGraph<object, IEdge<object>> Graph
       {
           get { return this._graph; }
       }

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

       public static List<string> ServerNames;

       public static ChooseMsSqlServerWindow ServerWindow;

       public readonly BidirectionalGraph<object, IEdge<object>> _graph = new BidirectionalGraph<object, IEdge<object>>();



       private ColumnDefinition column1CloneForLayer0;
       private ColumnDefinition column2CloneForLayer0;
       private ColumnDefinition column2CloneForLayer1;

       //"EfficientSugiyama";//{ "Circular", "Tree", "FR", "BoundedFR", "KK", "ISOM", "LinLog", "EfficientSugiyama", "Sugiyama", "CompoundFDP" };
       private string layoutAlgorithm = "CompoundFDP";

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            this.CreateGraph();

            this.column1CloneForLayer0 = new ColumnDefinition { SharedSizeGroup = "column1" };
            this.column2CloneForLayer0 = new ColumnDefinition { SharedSizeGroup = "column2" };
            this.column2CloneForLayer1 = new ColumnDefinition { SharedSizeGroup = "column2" };
            ServerNames = ServerGetter.GetMsSqlServerNames();
        }
        
        private void CreateGraph()
        {
            var obj1 = new TableVertex("One");
            this._graph.AddVertex(obj1);
            var obj2 = new TableVertex("Two");
            this._graph.AddVertex(obj2);
            mainWindow._graph.AddEdge(new Edge<object>(obj1, obj2));
        }

        private void OpenMySqlServers(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("TODO");
        }

        private void OpenMsSqlServers(object sender, RoutedEventArgs e)
        {
            ServerWindow = new ChooseMsSqlServerWindow { Owner = this };
            ServerWindow.Show();
        }

        private void ExitClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
        }

        public void PanePropertiesPinClick(object sender, RoutedEventArgs e)
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

        public void PanePropertiesButtonMouseEnter(object sender, RoutedEventArgs e)
        {
            this.layerProperties.Visibility = Visibility.Visible;
            Panel.SetZIndex(this.layerProperties, 1);
             Panel.SetZIndex(layer2, 0);
         if (this.paneInfoButton.Visibility == Visibility.Visible)
         {
             layer2.Visibility = Visibility.Collapsed;
         }
        }

        public void LayerMainMouseEnter(object sender, RoutedEventArgs e)
        {
            if (this.panePropertiesButton.Visibility == Visibility.Visible)

            {
                this.layerProperties.Visibility = Visibility.Collapsed;
            }
            if (this.paneInfoButton.Visibility == Visibility.Visible)
            {
                layer2.Visibility = Visibility.Collapsed;
            }
        }

        public void DockPane(int paneNumber)
        {
            switch (paneNumber)
            {
                case 1:
                    {
                        this.panePropertiesButton.Visibility = Visibility.Collapsed;
                        this.layerMain.ColumnDefinitions.Add(this.column1CloneForLayer0);
                        this.panePropertiesPinImage.Source = new BitmapImage(CommonHelper.GetUriString("Images/pin.png"));

                        if (this.paneInfoButton.Visibility == Visibility.Collapsed)
                        {
                            this.layerProperties.ColumnDefinitions.Add(this.column2CloneForLayer1);
                        }

                        break;
                    }

                case 2:
                    {
                        this.paneInfoButton.Visibility = Visibility.Collapsed;
                        pane2PinImage.Source = new BitmapImage(CommonHelper.GetUriString("Images/pin.png"));
                        this.layerMain.ColumnDefinitions.Add(this.column2CloneForLayer0);
                        if (panePropertiesButton.Visibility == Visibility.Collapsed)
                        {
                            layerProperties.ColumnDefinitions.Add(this.column2CloneForLayer1);
                        }

                        break;
                    }
            }
          }

        public void UndockPane(int paneNumber)
        {
            switch (paneNumber)
            {
                case 1:
                    {
                        this.layerProperties.Visibility = Visibility.Visible;
                        this.panePropertiesButton.Visibility = Visibility.Visible;
                        this.panePropertiesPinImage.Source = new BitmapImage(CommonHelper.GetUriString("Images/unpin.png"));
                        this.layerMain.ColumnDefinitions.Remove(this.column1CloneForLayer0);
                        this.layerProperties.ColumnDefinitions.Remove(this.column2CloneForLayer1);
                        break;
                    }

                case 2:
                    {
                        layer2.Visibility = Visibility.Visible;
                        this.paneInfoButton.Visibility = Visibility.Visible;
                        pane2PinImage.Source = new BitmapImage(CommonHelper.GetUriString("Images/unpin.png"));
                        this.layerMain.ColumnDefinitions.Remove(this.column2CloneForLayer0);
                        this.layerProperties.ColumnDefinitions.Remove(this.column2CloneForLayer1);
                        break;
                    }
            }
         }

        private void PropertiesClick(object sender, RoutedEventArgs e)
        {
            var menuItem = sender as MenuItem;
            var vertex = menuItem.Tag as TableVertex;
            PropertyTree.Children.Clear();
            TableName.Children.Clear();
            if (vertex != null && vertex.Model != null)
            {
                vertex.Change();
                TableName.Children.Add(new TextBlock { Text = vertex.Model.Name, Margin = new Thickness(10, 5, 5, 5) });
                PropertyTree.Children.Add(CommonHelper.PropertiesTreeView(vertex.Model));
            }
            else
            {
                TableName.Children.Add(new TextBlock { Text = "No info", Margin = new Thickness(10, 5, 5, 5) });
            }
           
            this.layerProperties.Visibility = Visibility.Visible;
            Panel.SetZIndex(this.layerProperties, 1);
        }
        
        private void PaneInfoMouseEnter(object sender, MouseEventArgs e)
        {
            if (panePropertiesButton.Visibility == Visibility.Visible)
            {
                layerProperties.Visibility = Visibility.Collapsed;
            }
        }

        private void PaneInfoPinClick(object sender, RoutedEventArgs e)
        {
            if (this.paneInfoButton.Visibility == Visibility.Collapsed)
            {
                this.UndockPane(2);
            }
            else
            {
                this.DockPane(2);
            }
        }
        
        public void PaneInfoButtonMouseEnter(object sender, RoutedEventArgs e)
        {
            layer2.Visibility = Visibility.Visible;
            Panel.SetZIndex(layer2, 1);
            Panel.SetZIndex(layerProperties, 0);
            
        if (panePropertiesButton.Visibility == Visibility.Visible)
            {
                layerProperties.Visibility = Visibility.Collapsed;
            }
        }

        public void PanePropertiesMouseEnter(object sender, RoutedEventArgs e)
        {
            if (this.paneInfoButton.Visibility == Visibility.Visible)
            {
                layer2.Visibility = Visibility.Collapsed;
            }
        }
    }
}
