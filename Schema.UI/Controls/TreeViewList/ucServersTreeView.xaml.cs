using System.Threading;
using System.Windows.Threading;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Aga.Controls.Tree;
using QuickGraph;
using Schema.Core.Helpers;
using Schema.Core.Models;
using Schema.Core.Models.Table;
using Schema.Core.Reader;
using Schema.UI.Table;
using Schema.UI.TreeViewList;
using Shema.Server.Models;
using CommonHelper = Schema.UI.Helpers.CommonHelper;

namespace Schema.UI.Controls.TreeViewList
{
    public partial class ServersTreeView : UserControl
    {
        private delegate void UpdateProgressDelegate(ServersTreeViewModel serverTreeview);

        public ServersTreeView()
        {
            InitializeComponent();
            StartLoader(false);
        }
       
        public void StartLoader(bool isRefresh)
        {
            ShowLoader();
            ServersTreeViewModel serverTreeview;
            ThreadStart start = delegate
                {
                    serverTreeview = isRefresh ? new ServersTreeViewModel(null) : new ServersTreeViewModel();

                    Dispatcher.Invoke(new UpdateProgressDelegate(UpdateProgress), 
                                      DispatcherPriority.Background,
                                      new object[] { serverTreeview });
                };

            new Thread(start).Start();
        }

        private void OkClick(object sender, RoutedEventArgs e)
        {
            var parent = GetParent((DependencyObject)sender);
            if (parent == null)
            {
                return;
            }

            var dataBase = ((FrameworkElement)sender).DataContext as DataBaseModel;
            var serverModel = parent.Node.Parent.Tag as ServerModel;

            var connectionString = CreateConnectionString(serverModel, dataBase);

            if (!string.IsNullOrEmpty(connectionString) && dataBase != null)
            {
                var db = GetDataBaseModel(dataBase.Name, connectionString);
               
                var mainWindow = MainWindow.ServerWindow.Owner as MainWindow;
                if (mainWindow != null)
                {
                    CommonHelper.InsertInfo(db, mainWindow);
                    mainWindow.graphLayout.Children.Clear();
                    mainWindow._graph.Clear();
                    foreach (var obj1 in db.Tables.Select(table => new TableVertex(table)))
                    {
                        mainWindow._graph.AddVertex(obj1);
                    }

                    var graphLayout = mainWindow.zoomControl.FindName("graphLayout") as GraphSharp.Controls.GraphLayout;
                    AddReferances(graphLayout, db.Tables, mainWindow);

                }
                MainWindow.ServerWindow.Close();
            }
            else
            {
                MessageBox.Show("Sorry.Try again.");
            }
        }

        #region draw diagramm helpers

        private TreeListItem GetParent(DependencyObject dependencyObject)
        {
            var parent = VisualTreeHelper.GetParent(dependencyObject);
            while (!(parent is TreeListItem || parent is TreeList))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }

            return parent as TreeListItem;
        }

        private void AddReferances(GraphSharp.Controls.GraphLayout graphLayout, List<TableModel> tables, MainWindow mainWindow)
        {
            foreach (var referance in tables)
            {
                var name = referance.Name;
                if (referance.Keys == null)
                {
                    continue;
                }

                foreach (var k in referance.Keys)
                {
                    var refName = k.ReferanceTable;
                    if (string.IsNullOrEmpty(refName))
                    {
                        continue;
                    }

                    var el1 = graphLayout.Graph.Vertices.Cast<TableVertex>().FirstOrDefault(s => s.Text == name);
                    var el2 = graphLayout.Graph.Vertices.Cast<TableVertex>().FirstOrDefault(s => s.Text == refName);
                    mainWindow._graph.AddEdge(new Edge<object>(el1, el2));
                }
            }
        }

        private DatabaseModel GetDataBaseModel(string dataBaseName, string connectionString)
        {
            var dataSet = new DataSet("dbDataSet");
            var mssqlReader = new MsSqlSqlReader(dataBaseName, connectionString);
            return ModelFiller.GetModel(mssqlReader, dataSet);
        }

        private string CreateConnectionString(ServerModel serverModel, DataBaseModel dataBase)//todo string.format
        {
            string passwordUsersOrIntegrated;
            if (!string.IsNullOrWhiteSpace(serverModel.Password) && !string.IsNullOrWhiteSpace(serverModel.UserName))
            {
                passwordUsersOrIntegrated = @";User ID=" + serverModel.UserName + @";Password=" + serverModel.Password + @";";
            }
            else
            {
                passwordUsersOrIntegrated = @";Integrated Security=true;";
            }

            if (dataBase == null)
            {
                return string.Empty;
            }

            return @"Data Source=" + serverModel.Name + @";Initial Catalog=" + dataBase.Name + passwordUsersOrIntegrated;
        }

        #endregion

        #region Loader helper methods

        private void UpdateProgress(ServersTreeViewModel serverTreeview)
        {
            _tree.Model = serverTreeview;
            HideLoader();
        }

        private void ShowLoader()
        {
            loader.Visibility = Visibility.Visible;
            loader.StartStopLoader(true);
        }

        private void HideLoader()
        {
            loader.Visibility = Visibility.Collapsed;
            loader.StartStopLoader(false);
            MainWindow.ServerWindow.btnRefresh.Visibility = Visibility.Visible;
        }

        #endregion


       
    }
}
