namespace Schema.UI
{
    using System.Windows;
    using System.Windows.Controls;
    using QuickGraph;
    using Helpers;
    using Table;

    public partial class MainWindow : Window
    {

        public IBidirectionalGraph<object, IEdge<object>> Graph
        {
            get { return _graph; }
        }

        public static ChooseMsSqlServerWindow ServerWindow;

        public readonly BidirectionalGraph<object, IEdge<object>> _graph = new BidirectionalGraph<object, IEdge<object>>();

       
        //"EfficientSugiyama";//{ "Circular", "Tree", "FR", "BoundedFR", "KK", "ISOM", "LinLog", "EfficientSugiyama", "Sugiyama", "CompoundFDP" };
        //private string layoutAlgorithm = "CompoundFDP";

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            CreateGraph();

           //// ServerNames = ServerGetter.GetServerNamesFromConfigFile();
        }

        private void CreateGraph()
        {
            var obj1 = new TableVertex("One");
            _graph.AddVertex(obj1);
            var obj2 = new TableVertex("Two");
            _graph.AddVertex(obj2);
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

        public void LayerMainMouseEnter(object sender, RoutedEventArgs e)
        {
            if (panels.panePropertiesButton.Visibility == Visibility.Visible)
            {
                panels.layerProperties.Visibility = Visibility.Collapsed;
            }
            if (panels.paneInfoButton.Visibility == Visibility.Visible)
            {
                panels.layer2.Visibility = Visibility.Collapsed;
            }

        }

        private void PropertiesClick(object sender, RoutedEventArgs e)
        {
            var menuItem = sender as MenuItem;
            var vertex = menuItem.Tag as TableVertex;
            panels.PropertyTree.Children.Clear();
            panels.TableName.Children.Clear();
            if (vertex != null && vertex.Model != null)
            {
                vertex.Change();
                panels.TableName.Children.Add(new TextBlock { Text = vertex.Model.Name, Margin = new Thickness(10, 5, 5, 5) });
                panels.PropertyTree.Children.Add(CommonHelper.PropertiesTreeView(vertex.Model));
            }
            else
            {
                panels.TableName.Children.Add(new TextBlock { Text = "No info", Margin = new Thickness(10, 5, 5, 5) });
            }

            panels.layerProperties.Visibility = Visibility.Visible;
            Panel.SetZIndex(panels.layerProperties, 1);
        }


        private void AlgorithmRadioButtonChecked(object sender, RoutedEventArgs e)
        {
            var menuItem = sender as RadioButton;
            if (menuItem != null && graphLayout != null)
            {
                var algorithmName = (string)menuItem.Tag;
                graphLayout.LayoutAlgorithmType = algorithmName;
                graphLayout.Relayout();
            }
        }
    }
}
