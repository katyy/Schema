﻿namespace Schema.UI
{
    using System;
    using System.Windows;
    using System.Windows.Controls;

    using Shema.Server;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        
        //private void Window_Loaded(object sender, RoutedEventArgs e)
        //{
        //    foreach (var name in ServerGetter.GetMsSqlServerNames())
        //    {
        //        var item = new TreeViewItem
        //            {
        //                Header = name,
        //                Tag = name + ",", 
        //                FontWeight = FontWeights.Normal
        //               };
        //       item.Expanded += this.ServerExpanded;
        //      foldersItem.Items.Add(item);
        //    }
        // }

        //private void ServerExpanded(object sender, RoutedEventArgs e)
        //{
        //    var item = (TreeViewItem)sender;
            
        //    if (item.Items.Count >= 1 )
        //    {
        //        return;
        //    }

        //    try
        //    {
        //        foreach (var databaseName in ServerGetter.GetDataBases(item.Header.ToString()))
        //        {
        //            var subitem = new TreeViewItem
        //                {
        //                    Header = databaseName,
        //                    Tag = databaseName, 
        //                    FontWeight = FontWeights.Normal
        //                };
        //            subitem.Expanded += this.DataBaseExpanded;
        //            item.Items.Add(subitem);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        this.lblError.Content = ex.Message;
        //    }
        //}

        //private void DataBaseExpanded(object sender, RoutedEventArgs e)
        //{
        //    var item = (TreeViewItem)sender;
            
        //    var dataBaseName = item.Header;
        //    var ph = (TreeViewItem)item.Parent;
        //   var serverName = ph.Header;
          
        //    string connectionString = @"Data Source=" + serverName + @";Initial Catalog=" + dataBaseName + @";Integrated Security=True;";
        //    lblError.Content = connectionString;
        //}
        private void OpenMySqlServers(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("TODO");
        }

        private void OpenMsSqlServers(object sender, RoutedEventArgs e)
        {
            var serverWindow = new ChooseMsSqlServerWindow();
            serverWindow.Show();
        }

        private void ExitClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();

        }
    }
}