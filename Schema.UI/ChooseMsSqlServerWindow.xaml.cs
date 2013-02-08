namespace Schema.UI
{
    using System.Windows;
    
    public partial class ChooseMsSqlServerWindow : Window
    {
        public ChooseMsSqlServerWindow()
        {
            InitializeComponent();
        }

        private void RefreshClick(object sender, RoutedEventArgs e)
        {
           treeView.StartLoader(true);
        }
   }
}
