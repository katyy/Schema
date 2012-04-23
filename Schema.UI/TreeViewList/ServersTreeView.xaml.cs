namespace Schema.UI.TreeViewList
{
    using System.Data;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Shapes;

    using Aga.Controls.Tree;

    using QuickGraph;

    using Schema.Core.Helpers;
    using Schema.Core.Models;
    using Schema.Core.Reader;
    using Schema.UI.Table;

    using Shema.Server.Models;

    /// <summary>
    /// Interaction logic for ServersTreeView.xaml
    /// </summary>
    public partial class ServersTreeView : UserControl
    {
        public const int WidthEl = 100;

        public const int HeightEl = 70;

        public ServersTreeView()
        {
            InitializeComponent();
            _tree.Model = new ServersTreeViewModel();
        }

        private TreeListItem GetParent(DependencyObject dependencyObject)
        {
            var parent = VisualTreeHelper.GetParent(dependencyObject);
            while (!(parent is TreeListItem || parent is TreeList))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }

            return parent as TreeListItem;
        }

        private void OkClick(object sender, RoutedEventArgs e)
        {
            var parent = this.GetParent((DependencyObject)sender);
            if (parent == null)
            {
                return;
            }

            var dataBase = ((FrameworkElement)sender).DataContext as DataBaseModel;
            var serverModel = parent.Node.Parent.Tag as ServerModel;

            var connectionString = this.CreateConnectionString(serverModel, dataBase);

            if (!string.IsNullOrEmpty(connectionString) && dataBase != null)
            {
                var db = this.GetDataBaseModel(dataBase.Name, connectionString);

                var mainWindow = MainWindow.serverWindow.Owner as MainWindow;
                if (mainWindow != null)
                {
                    InsertInfo(db, mainWindow);
                    mainWindow.graphLayout.Children.Clear();
                    mainWindow._graph.Clear();
                    foreach (var obj1 in db.Tables.Select(table => new TableVertex(table)))
                    {
                        mainWindow._graph.AddVertex(obj1);
                    }

                    var graphLayout = mainWindow.zoomControl.FindName("graphLayout") as GraphSharp.Controls.GraphLayout;


                    foreach (var referance in db.Tables)
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

                MainWindow.serverWindow.Close();
            }
            else
            {
                MessageBox.Show("Sorry.Try again.");
            }
        }

        private DatabaseModel GetDataBaseModel(string dataBaseName, string connectionString)
        {
            var dataSet = new DataSet("dbDataSet");
            var mssqlReader = new MsSqlReader(dataBaseName, connectionString);
            return ModelFiller.GetModel(mssqlReader, dataSet);
        }

        private string CreateConnectionString(ServerModel serverModel, DataBaseModel dataBase)
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

        private void InsertInfo(DatabaseModel db, MainWindow mainWindow)
        {
            mainWindow.InfoTree.Children.Clear();
            var tree = new TreeView();
            var folderUri = mainWindow.GetUriString("Images/folder.png");
            if (db.Tables != null)
            {
                var tableItem = mainWindow.GetTreeViewItem("Tables", folderUri);
                foreach (var table in db.Tables)
                {
                    var tableNameItem = mainWindow.GetTreeViewItem(table.Name, folderUri);
                    if (table.Columns != null)
                    {
                        tableNameItem.Items.Add(mainWindow.GetColumnItem(table.Columns, folderUri));
                    }
                    if (table.Keys != null)
                    {
                        tableNameItem.Items.Add(mainWindow.GetKeyItem(table.Keys, folderUri));
                    }
                    if (table.Trigers != null)
                    {
                        tableNameItem.Items.Add(mainWindow.GetTriggerItem(table.Trigers, folderUri));
                    }
                    if (table.Indexes != null)
                    {
                        tableNameItem.Items.Add(mainWindow.GetIndexItem(table.Indexes, folderUri));
                   }
                        tableItem.Items.Add(tableNameItem);
                }

                tree.Items.Add(tableItem);
            }

            if (db.Views != null)
            {
                var viewItem = mainWindow.GetTreeViewItem("Views", folderUri);
                foreach (var view in db.Views)
                {
                    var viewNameItem = mainWindow.GetTreeViewItem(view.Name, folderUri);
                    if (view.Columns != null)
                    {
                        viewNameItem.Items.Add(mainWindow.GetColumnItem(view.Columns, folderUri));
                    }
                    if (view.Trigers != null)
                    {
                        viewNameItem.Items.Add(mainWindow.GetTriggerItem(view.Trigers, folderUri));
                    }
                     if (view.Indexes != null)
                     {
                         viewNameItem.Items.Add(mainWindow.GetIndexItem(view.Indexes, folderUri));
                     }
                    viewItem.Items.Add(viewNameItem);
                }

                tree.Items.Add(viewItem);
            }

            if (db.Functions != null)
            {
                var functionItem = mainWindow.GetTreeViewItem("Functions", folderUri);
                foreach (var function in db.Functions)
                {
                    var item = mainWindow.GetTreeViewItem(function.Key, folderUri);
                    foreach (var parametr in function.Value)
                    {
                        var value = string.Format(
                            "{0} ( {1}, {2})", parametr.Parametr, parametr.DataType, parametr.TypeDescription);
                        item.Items.Add(mainWindow.GetTreeViewItem(value, folderUri));
                    }


                    functionItem.Items.Add(item);
                }

                tree.Items.Add(functionItem);
            }

            if (db.Procedures != null)
            {
                var procedureItem = mainWindow.GetTreeViewItem("Procedures", folderUri);
                foreach (var procedure in db.Procedures)
                {
                    var item = mainWindow.GetTreeViewItem(procedure.Key, folderUri);
                    foreach (var parametr in procedure.Value)
                    {
                        var value = string.Format(
                            "{0} ( {1}, {2})", parametr.Parametr, parametr.DataType, parametr.TypeDescription);
                        item.Items.Add(mainWindow.GetTreeViewItem(value, folderUri));
                    }
                    
                    procedureItem.Items.Add(item);
                }

                tree.Items.Add(procedureItem);
            }
            
            mainWindow.InfoTree.Children.Add(tree);
        }

    }
}
