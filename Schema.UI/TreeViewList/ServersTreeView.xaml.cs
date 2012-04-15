namespace Schema.UI.TreeViewList
{
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    using Schema.UI.TreeViewList.Combobox;

    /// <summary>
    /// Interaction logic for ServersTreeView.xaml
    /// </summary>
    public partial class ServersTreeView : UserControl
    {
        public List<DbCustomer> Customers { get; set; }
        public List<DbOption> Options { get; set; }
        public ServersTreeView()
        {
            InitializeComponent();
            _tree.Model = new ServersTreeViewModel();
          
            DataContext = this;
        }

        private void AuthenicationSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
           
            var comboBox = sender as ComboBox;
           var tyuo= comboBox.SelectedValue;
          var yr=  VisualTreeHelper.GetParent(comboBox);
         
            var typeItem = (ComboBoxItem)comboBox.SelectedItem;
            string value = typeItem.Content.ToString();

            if (comboBox != null)
            {
                var t = comboBox.SelectedValue;
                var k = (e.AddedItems[0] as ComboBoxItem).Content.ToString();
               
                var ty=((ComboBox)e.OriginalSource).Text;
            }
        }
    }
}
