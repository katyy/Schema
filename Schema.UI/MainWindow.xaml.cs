namespace Schema.UI
{
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Controls;

    using Schema.Core.Models;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public static ChooseMsSqlServerWindow serverWindow ;
        public static DatabaseModel Model;
        public TextBox textbox = new TextBox{Text = "111"};
       


        public MainWindow()
        {
           InitializeComponent();
           Stack.Children.Add(textbox);
        }
        
        private void OpenMySqlServers(object sender, RoutedEventArgs e)
        {
           
            var t = Model;
            MessageBox.Show("TODO");
        }

        private void OpenMsSqlServers(object sender, RoutedEventArgs e)
        {
            serverWindow = new ChooseMsSqlServerWindow();
            serverWindow.Owner = this;
            serverWindow.Show();
        }

        private void ExitClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           
          
        }

       
    }
}
