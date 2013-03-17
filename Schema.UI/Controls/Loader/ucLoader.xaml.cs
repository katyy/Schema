using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Schema.UI.Controls.Loader
{

    public partial class ucLoader : UserControl
    {
        Storyboard LoaderAnimation;

        public string LoaderMessage
        {
            get { return (string)GetValue(LoaderMessageProperty); }
            set { SetValue(LoaderMessageProperty, value); }
        }

        public static readonly DependencyProperty LoaderMessageProperty =
              DependencyProperty.Register("LoaderMessage", typeof(string), typeof(ucLoader), new UIPropertyMetadata(string.Empty));

        public ucLoader()
        {

            InitializeComponent();
            LoaderAnimation = Resources["loader_animation"] as Storyboard;

        }
        public void StartStopLoader(bool operationFlag)
        {
            if (LoaderAnimation == null) return;
            if (operationFlag)
            {
                LoaderAnimation.Begin();
            }
            else
            {
                LoaderAnimation.Stop();
            }
        }
    }
}
