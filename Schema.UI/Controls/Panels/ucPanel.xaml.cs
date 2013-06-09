using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Schema.UI.Helpers;

namespace Schema.UI.Controls.Panels
{
    //todo get mainwindow in dock undock event
    public partial class ucPanel : UserControl
    {
        private ColumnDefinition column1CloneForLayer0;
        private ColumnDefinition column2CloneForLayer0;
        private ColumnDefinition column2CloneForLayer1;

        public ucPanel()
        {
            InitializeComponent();
            column1CloneForLayer0 = new ColumnDefinition { SharedSizeGroup = "column1" };
            column2CloneForLayer0 = new ColumnDefinition { SharedSizeGroup = "column2" };
            column2CloneForLayer1 = new ColumnDefinition { SharedSizeGroup = "column2" };
        }

        public void PanePropertiesPinClick(object sender, RoutedEventArgs e)
         {
            var mainWindow = Window.GetWindow(panel) as MainWindow;
            if (panePropertiesButton.Visibility == Visibility.Collapsed)
            {
                UndockPane(1,mainWindow);
            }
            else
            {
                DockPane(1,mainWindow);
            }
        }

        public void PanePropertiesButtonMouseEnter(object sender, RoutedEventArgs e)
        {
            layerProperties.Visibility = Visibility.Visible;
            Panel.SetZIndex(layerProperties, 1);
            Panel.SetZIndex(layer2, 0);
            if (paneInfoButton.Visibility == Visibility.Visible)
            {
                layer2.Visibility = Visibility.Collapsed;
            }
        }

        public void LayerMainMouseEnter(object sender, RoutedEventArgs e)
        {
            if (panePropertiesButton.Visibility == Visibility.Visible)
            {
                layerProperties.Visibility = Visibility.Collapsed;
            }
            if (paneInfoButton.Visibility == Visibility.Visible)
            {
                layer2.Visibility = Visibility.Collapsed;
            }
        }

        public void DockPane(int paneNumber,MainWindow test)
        {
            switch (paneNumber)
            {
                case 1:
                    {
                        panePropertiesButton.Visibility = Visibility.Collapsed;
                        test.layerMain.ColumnDefinitions.Add(column1CloneForLayer0);
                        panePropertiesPinImage.Source = new BitmapImage(CommonHelper.GetUriString("Images/pin.png"));

                        if (paneInfoButton.Visibility == Visibility.Collapsed)
                        {
                            layerProperties.ColumnDefinitions.Add(column2CloneForLayer1);
                        }

                        break;
                    }

                case 2:
                    {
                        paneInfoButton.Visibility = Visibility.Collapsed;
                        pane2PinImage.Source = new BitmapImage(CommonHelper.GetUriString("Images/pin.png"));
                        test.layerMain.ColumnDefinitions.Add(column2CloneForLayer0);
                        if (panePropertiesButton.Visibility == Visibility.Collapsed)
                        {
                            layerProperties.ColumnDefinitions.Add(column2CloneForLayer1);
                        }

                        break;
                    }
            }
        }

        public void UndockPane(int paneNumber,MainWindow test)
        {
            switch (paneNumber)
            {
                case 1:
                    {
                        this.layerProperties.Visibility = Visibility.Visible;
                        this.panePropertiesButton.Visibility = Visibility.Visible;
                        this.panePropertiesPinImage.Source = new BitmapImage(CommonHelper.GetUriString("Images/unpin.png"));
                       test.layerMain.ColumnDefinitions.Remove(this.column1CloneForLayer0);
                        this.layerProperties.ColumnDefinitions.Remove(this.column2CloneForLayer1);
                        break;
                    }

                case 2:
                    {
                        layer2.Visibility = Visibility.Visible;
                        this.paneInfoButton.Visibility = Visibility.Visible;
                        pane2PinImage.Source = new BitmapImage(CommonHelper.GetUriString("Images/unpin.png"));
                        test.layerMain.ColumnDefinitions.Remove(this.column2CloneForLayer0);
                        this.layerProperties.ColumnDefinitions.Remove(this.column2CloneForLayer1);
                        break;
                    }
            }
        }
        private void PaneInfoMouseEnter(object sender, MouseEventArgs e)
        {
            if (panePropertiesButton.Visibility == Visibility.Visible)
            {
                layerProperties.Visibility = Visibility.Collapsed;
            }
        }

        private void PaneInfoPinClick(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(panel) as MainWindow;
            if (this.paneInfoButton.Visibility == Visibility.Collapsed)
            {
                this.UndockPane(2,mainWindow);
            }
            else
            {
                this.DockPane(2,mainWindow);
            }
        }

        public void PaneInfoButtonMouseEnter(object sender, RoutedEventArgs e)
        {
            layer2.Visibility = Visibility.Visible;
            Panel.SetZIndex(layer2, 1);
            Panel.SetZIndex(layerProperties, 0);

            if (panePropertiesButton.Visibility == Visibility.Visible)
            {
                layerProperties.Visibility = Visibility.Collapsed;
            }
        }

        public void PanePropertiesMouseEnter(object sender, RoutedEventArgs e)
        {
            if (this.paneInfoButton.Visibility == Visibility.Visible)
            {
                layer2.Visibility = Visibility.Collapsed;
            }
        }


    }
}
