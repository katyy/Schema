// -----------------------------------------------------------------------
// <copyright file="RegistryModel.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Schema.UI.TreeViewList
{
    using System.Collections;
    using System.Collections.Generic;

    using Aga.Controls.Tree;

    using Schema.UI.TreeViewList.Combobox;

    using Shema.Server;
    using Shema.Server.Models;

    public class ServersTreeViewModel : ITreeModel
    {
        public IEnumerable GetChildren(object parent)
        {
            var key = parent as ServerModel;
            if (parent == null)
            {
                foreach (var s in ServerGetter.GetMsSqlServerNames())
                {
                    yield return
                        new ServerModel
                            {
                                Name = s,
                                Options =
                                    new List<DbOption>
                                        {
                                            new DbOption { OptionId = 1, OptionText = "Option 1" },
                                            new DbOption { OptionId = 2, OptionText = "Option 2" },
                                            new DbOption { OptionId = 3, OptionText = "Option 3" },
                                        }
                            };
                }
            }
            else if (key != null)
            {
                foreach (var databaseName in ServerGetter.GetDataBases(key.Name))
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

