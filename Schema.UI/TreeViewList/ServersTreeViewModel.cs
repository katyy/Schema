﻿// -----------------------------------------------------------------------
// <copyright file="RegistryModel.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Schema.UI.TreeViewList
{
    using System.Collections;
    using System.Collections.Generic;

    using Aga.Controls.Tree;

   
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
                                Name = s
                             };
                }
            }
            else if (key != null)
            {
                //foreach (var databaseName in ServerGetter.GetDataBases(key))
                //{
                //    yield return new DataBaseModel { Name = databaseName };
                //}
              }
        }

        public bool HasChildren(object parent)
        {
            return parent is ServerModel;
        }
    }
}

