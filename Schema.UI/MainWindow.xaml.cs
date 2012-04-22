namespace Schema.UI
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Input;
    using System.Windows.Media;

    using Schema.Core.Models;
    using Schema.UI.MoveResize;

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
            //connectors.Children.Add(myThumb1.LinkTo(myThumb2));
            //connectors.Children.Add(myThumb2.LinkTo(myThumb3));
        }

        //public void OnDragDelta(object sender, DragDeltaEventArgs e)
        //{
        //    var thumb = e.Source as MoveThumb;

        //    if (thumb == null)
        //    {
        //        return;
        //    }

        //    var left = Canvas.GetLeft(thumb);
        //    var top = Canvas.GetTop(thumb);

        //    Canvas.SetLeft(thumb, left + e.HorizontalChange);
        //    Canvas.SetTop(thumb, top + e.VerticalChange);
        //    thumb.UpdateLinks();
        //}
    }
}
