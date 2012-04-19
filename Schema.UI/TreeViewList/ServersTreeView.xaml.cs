namespace Schema.UI.TreeViewList
{
    using System.Data;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    using Aga.Controls.Tree;

    using Schema.Core.Helpers;
    using Schema.Core.Reader;

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
                var dataSet = new DataSet("dbDataSet");
                var mssqlReader = new MsSqlReader(dataBase.Name, connectionString);
                var db = ModelFiller.GetModel(mssqlReader, dataSet);
                MainWindow.Model = db;
                var mainWindow = MainWindow.serverWindow.Owner as MainWindow;
                if (mainWindow != null)
                {
                    mainWindow.MainCanvas.Children.Clear();
                    var tables = MainWindow.Model.Tables;
                    var count = (int)(mainWindow.Width / 110);
                    var width = 0;
                    var height = 10;
                    for (var i = 0; i < tables.Count; i++)
                    {
                        if (i % count == 0)
                        {
                            width = 10;
                            height += HeightEl + 20;
                        }
                        else
                        {
                            width += WidthEl + 20;
                        }

                        this.AddTable(tables[i].Name, mainWindow, width, height);
                    }
                }

               MainWindow.serverWindow.Close();
            }
            else
            {
                MessageBox.Show("Sorry.Try again.");
            }

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

        private void AddTable(string name, MainWindow mainWindow, int w, int h)
        {
            var blackBrush = new SolidColorBrush { Color = Colors.Black };
            //var blueBrush = new SolidColorBrush { Color = Colors.LightBlue };
            //var whiteSmokeBrush = new SolidColorBrush { Color = Colors.WhiteSmoke };
            //var blueBorder = new Border
            //    {
            //        CornerRadius = new CornerRadius(5),
            //        Background = blueBrush,
            //        BorderThickness = new Thickness(0, 1, 0, 0)
            //    };
            //var whiteBorder = new Border
            //    {
            //        Background = whiteSmokeBrush,
            //        CornerRadius = new CornerRadius(5),
            //        Margin = new Thickness(5, 5, 5, 25)
            //    };


            var text = new TextBox
            {
                Text = name,
                FontSize = 11,
                Margin = new Thickness(1, 1, 0, 0),
                Foreground = blackBrush,
                TextWrapping = TextWrapping.Wrap,
                AcceptsReturn = true,
                BorderThickness = new Thickness(0),
            };

            var border = new Border
                 {
                    BorderBrush = blackBrush,
                     BorderThickness = new Thickness(1, 1, 1, 1),
                     CornerRadius = new CornerRadius(5),
                     Margin = new Thickness(15, 15, 15, 15),
                     Child = text,
                     Background = blackBrush
                 };
            //var grid = new Grid();
            //grid.Children.Add(blueBorder);
            //grid.Children.Add(whiteBorder);
            //grid.Children.Add(border);
            var contentControl = new ContentControl
            {
                MinWidth = 100,
                Height = HeightEl,
                MinHeight = 50,
                Width = WidthEl,
                Template = (ControlTemplate)mainWindow.Resources["DesignerItemTemplate"],
                Content = border
            };

            Canvas.SetLeft(contentControl, w);
            Canvas.SetTop(contentControl, h);
            mainWindow.MainCanvas.Children.Add(contentControl);
        }
    }
}
