using System.Collections.Generic;

namespace Schema.UI.TreeViewList
{
    using System.Collections;

    using Aga.Controls.Tree;

    using Shema.Server;
    using Shema.Server.Models;

    public class ServersTreeViewModel : ITreeModel
    {
        public List<string> ServerNames { get; set; }

        public ServersTreeViewModel(List<string> serverNames)
        {
            ServerNames = serverNames;
        }

        public ServersTreeViewModel()
        {
            ServerNames = ServerGetter.GetServerNamesFromConfigFile();
        }

        public IEnumerable GetChildren(object parent)
        {
            var key = parent as ServerModel;
            if (parent == null)
            {
                var serverNames = ServerNames ?? ServerGetter.GetMsSqlServerNames();

                foreach (var name in serverNames)
                {
                    yield return
                        new ServerModel
                            {
                                Name = name,
                            };
                }
            }
            else if (key != null)
            {
                var databases = ServerGetter.GetDataBases(key);
                foreach (var databaseName in databases)
                {
                    yield return new DataBaseModel { Name = databaseName };
                }
            }
        }

        public bool HasChildren(object parent)
        {
            return parent is ServerModel;
        }
    }
}

