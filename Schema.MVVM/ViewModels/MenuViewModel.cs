using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Dynamic;
using System.Windows;
using Caliburn.Micro;
using Schema.MVVM.Helpers;
using Schema.MVVM.Model;

namespace Schema.MVVM.ViewModels
{
    [Export(typeof(MenuViewModel))]
    public class MenuViewModel
    {
        private readonly IEventAggregator _events;

        private readonly IWindowManager _windowManager;

        [ImportingConstructor]
        public MenuViewModel(IEventAggregator events, IWindowManager windowManager)
        {
            _events = events;
            _windowManager = windowManager;
        }

        public void OpenMsSqlServers()
        {
            var t = new ServerItemModel {DataBaseName = "db", UserName = "usrsssssssss", Password = "pwdcccccccccccc"};
            var t1 = new ServerItemModel { DataBaseName = "aa", UserName = "aa", Password = "aa" };
            _windowManager.ShowDialog(new ServersTreeViewModel(_windowManager, new List<ServerItemModel> {t,t1}));
        }
    }
}
