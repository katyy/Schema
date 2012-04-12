namespace Schema.UI
{
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    using Shema.Server;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
           
           treeView1.ItemsSource = ServerGetter.GetMsSqlServerNames();

           
            //TreeViewItem rootItem = new TreeViewItem() { Header = "Root" };
            //TreeViewItem level1 = new TreeViewItem() { Header = "Level1" };
            //TreeViewItem level11 = new TreeViewItem() { Header = "Level11" };
            //level1.Items.Add(level11);
            //rootItem.Items.Add(level1);
            //treeView1.Items.Add(rootItem);

          }

        private void SelectedChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
           var t= ServerGetter.GetDataBases((string)this.treeView1.SelectedValue);
           var item = (ItemsControl)treeView1.SelectedItem;
            item.AddChild("dfs");
         
          // item.Parent.I
         // treeView1.SelectedNode.Nodes.Add(yourChildNode);
           treeView1.Items.Insert(2 , "dsv");
        }
    }
}
