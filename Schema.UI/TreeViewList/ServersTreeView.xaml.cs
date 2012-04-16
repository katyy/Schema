namespace Schema.UI.TreeViewList
{
    using System;
    using System.Collections.Generic;
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
              
            string passwordUsersOrIntegrated;
            if (!string.IsNullOrWhiteSpace(serverModel.Password) && !string.IsNullOrWhiteSpace(serverModel.UserName))
            {
                passwordUsersOrIntegrated = @";User Name=" + serverModel.UserName + @";Password=" + serverModel.Password + @";";
            }
            else
            {
                passwordUsersOrIntegrated = @";Integrated Security=true;";
            }

            if (dataBase == null)
            {
                return;
            }

            var connectionString = @"Data Source=" + serverModel.Name + @";Initial Catalog=" + dataBase.Name + passwordUsersOrIntegrated;


            var dataSet = new DataSet("dbDataSet");
            var mssqlReader = new MsSqlReader
                {
                    ConnectionString = connectionString,
                    DbName = dataBase.Name
                };
            var db = ModelFiller.GetModel(mssqlReader, dataSet);
            }

        }
}
