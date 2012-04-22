namespace Schema.UI
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Input;
    using System.Windows.Media;

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
        public static List<LineGeometry> EndLines { get; set; }

        public static List<LineGeometry> StartLines { get; set; }

        public static ChooseMsSqlServerWindow serverWindow;

        public static DatabaseModel Model;


        public MainWindow()
        {
           InitializeComponent();
            DataContext = this;
           CreateGraph();
        }
        private void CreateGraph()
        {
            this._graph.Clear();
            var obj1 = new SampleVertex("One");
            this._graph.AddVertex(obj1);
            var obj2 = new SampleVertex("Two");
            this._graph.AddVertex(obj2);
            //var obj3 = new SampleVertex("Three");
            //this._graph.AddVertex(obj3);

            //var obj4 = new SampleVertex("4444");
            //this._graph.AddVertex(obj4);
           this._graph.AddEdge(new Edge<object>(obj1, obj2));
            //this._graph.AddEdge(new Edge<object>(obj1, obj3));
            //this._graph.AddEdge(new Edge<object>(obj1, obj1));
            //this._graph.AddEdge(new Edge<object>(obj3, obj2));
            //this._graph.AddEdge(new Edge<object>(obj4, obj2));
            //this._graph.AddEdge(new Edge<object>(obj3, obj4));
            //this._graph.AddEdge(new Edge<object>(obj4, obj1));
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
        public void add( SampleVertex obj1,  SampleVertex obj2)
        {
            this._graph.AddEdge(new Edge<object>(obj1, obj2));
        }

        public readonly BidirectionalGraph<object, IEdge<object>> _graph = new BidirectionalGraph<object, IEdge<object>>();
        public IBidirectionalGraph<object, IEdge<object>> Graph
        {
            get { return _graph; }
        }

        private string _layoutAlgorithm = "ISOM"; //"EfficientSugiyama";//{ "Circular", "Tree", "FR", "BoundedFR", "KK", "ISOM", "LinLog", "EfficientSugiyama", "Sugiyama", "CompoundFDP" };
        public string LayoutAlgorithm
        {
            get { return _layoutAlgorithm; }
            set
            {
                if (value != _layoutAlgorithm)
                {
                    _layoutAlgorithm = value;
                }
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var menuItem = sender as MenuItem;
            var vertex = menuItem.Tag as SampleVertex;
            vertex.Change();    

        }

        public void CreateGraph(string name)
        {
            var obj1 = new SampleVertex(name);
            _graph.AddVertex(obj1);
        }
        public void CreateGraph(string name, TableModel model)
        {
            var obj1 = new SampleVertex(name,model);
            _graph.AddVertex(obj1);
        }

        public void Clear()
        {
            _graph.Clear();
        }
       
    }
}
