using System.ComponentModel.Composition;
using Caliburn.Micro;

namespace Schema.MVVM.ViewModels
{
    [Export(typeof(MainViewModel))]
    public class MainViewModel : PropertyChangedBase
    {
        private readonly IWindowManager _windowManager;

        [ImportingConstructor]
        public MainViewModel(GraphViewModel graphModel, MenuViewModel menuModel, IEventAggregator events)
        {
            GraphModel = graphModel;
            MenuModel = menuModel;
            events.Subscribe(this);
        }

        public MainViewModel(IWindowManager windowManager)
        {
            _windowManager = windowManager;
        }

        public GraphViewModel GraphModel { get; private set; }

        public MenuViewModel MenuModel { get; private set; }
    }
}
