using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Windows.Controls;
using Caliburn.Micro;
using Schema.MVVM.Model;

namespace Schema.MVVM.ViewModels
{
    [Export(typeof(ServersTreeViewModel))]
    public class ServersTreeViewModel : Caliburn.Micro.PropertyChangedBase
    {
        private readonly IWindowManager _windowManager;

        [ImportingConstructor]
        public ServersTreeViewModel(IEventAggregator events)
        {
            events.Subscribe(this);
        }

        [ImportingConstructor]
        public ServersTreeViewModel(IWindowManager windowManager, List<ServerItemModel> serverItemModel)
        {
            _windowManager = windowManager;
            ServerItem = new ObservableCollection<ServerItemModel>(serverItemModel);

        }

       // private ServerItemModel _serverItemModel;
        public ObservableCollection<ServerItemModel> ServerItem { get; set; }
        //public ListView serverListView;


    }
}
