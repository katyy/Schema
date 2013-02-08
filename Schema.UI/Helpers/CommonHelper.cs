namespace Schema.UI.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media.Imaging;

    using Core.Models;
    using Core.Models.Table;

    public class CommonHelper
    {
        public static TreeView PropertiesTreeView(TableModel model)
        {
            var tree = new TreeView();
            var folderUri = GetUriString("Images/folder.png");
            if (model.Columns != null)
            {
                tree.Items.Add(GetColumnItem(model.Columns, folderUri));
            }

            if (model.Keys != null)
            {
                tree.Items.Add(GetKeyItem(model.Keys, folderUri));
            }

            if (model.Trigers != null)
            {
                tree.Items.Add(GetTriggerItem(model.Trigers, folderUri));
            }

            if (model.Indexes != null)
            {
                tree.Items.Add(GetIndexItem(model.Indexes, folderUri));
            }

            return tree;
        }

        public static TreeViewItem GetTreeViewItem(string text, Uri imageUri)
        {
            var item = new TreeViewItem();
            var stackPanel = new StackPanel { Orientation = Orientation.Horizontal };
            var image = new Image { Source = new BitmapImage(imageUri) };
            var textBlock = new TextBlock
            {
                Text = text,
                Margin = new Thickness(20, 5, 5, 10)
            };
            stackPanel.Children.Add(image);
            stackPanel.Children.Add(textBlock);
            item.Header = stackPanel;
            return item;
        }


        public static TreeViewItem GetColumnItem(List<ColumnModel> columns, Uri mainImageUri)
        {
            if (columns == null) return null;
            var columnItem = GetTreeViewItem("Columns", mainImageUri);
            foreach (var column in columns)
            {
                var allowNull = column.AllowNull ? "null" : "not null";
                var imageUri = GetUriString("Images/table.png");
                var item = GetTreeViewItem(string.Format("{0} ({1} , {2})", column.ColumnName, column.TypeName, allowNull), imageUri);
                columnItem.Items.Add(item);
            }
            return columnItem;
        }

        public static TreeViewItem GetKeyItem(List<KeyModel> keys, Uri mainImageUri)
        {
            if (keys == null) return null;
            var keyItem = GetTreeViewItem("Keys", mainImageUri);
            foreach (var key in keys)
            {
                var imageUri = GetUriString("Images/key.png");
                var item = GetTreeViewItem(key.Name, imageUri);
                keyItem.Items.Add(item);
            }
            return keyItem;
        }

        public static TreeViewItem GetTriggerItem(List<TriggerModel> triggerModels, Uri folderUri)
        {
            if (triggerModels == null) return null;
            var triggerItem = GetTreeViewItem("Triggers", folderUri);
            foreach (var trigger in triggerModels)
            {
                var imageUri = GetUriString("Images/trigger.png");
                var item = GetTreeViewItem(string.Format("{0} ({1})", trigger.TrigerName, trigger.Event), imageUri);

                triggerItem.Items.Add(item);
            }
            return triggerItem;
        }
        public static TreeViewItem GetIndexItem(List<IndexModel> indexModels, Uri folderUri)
        {
            if (indexModels == null) return null;
            var indexItem = GetTreeViewItem("Indexes", folderUri);
            foreach (var index in indexModels)
            {
                var imageUri = GetUriString("Images/index.png");
                var item = GetTreeViewItem(
                    string.Format("{0} ({1})", index.Name, index.IsDescending), imageUri);
                indexItem.Items.Add(item);
            }
            return indexItem;
        }

        public static void InsertInfo(DatabaseModel db, MainWindow mainWindow)
        {
            mainWindow.panels.InfoTree.Children.Clear();
            var tree = new TreeView();
            var folderUri = GetUriString("Images/folder.png");
            if (db.Tables != null)
            {
                var tableItem = GetTreeViewItem("Tables", folderUri);
                foreach (var table in db.Tables)
                {
                    var tableNameItem = GetTreeViewItem(table.Name, folderUri);
                    if (table.Columns != null)
                    {
                        tableNameItem.Items.Add(GetColumnItem(table.Columns, folderUri));
                    }

                    if (table.Keys != null)
                    {
                        tableNameItem.Items.Add(GetKeyItem(table.Keys, folderUri));
                    }

                    if (table.Trigers != null)
                    {
                        tableNameItem.Items.Add(GetTriggerItem(table.Trigers, folderUri));
                    }

                    if (table.Indexes != null)
                    {
                        tableNameItem.Items.Add(GetIndexItem(table.Indexes, folderUri));
                    }

                    tableItem.Items.Add(tableNameItem);
                }

                tree.Items.Add(tableItem);
            }

            if (db.Views != null)
            {
                var viewItem = GetTreeViewItem("Views", folderUri);
                foreach (var view in db.Views)
                {
                    var viewNameItem = GetTreeViewItem(view.Name, folderUri);
                    if (view.Columns != null)
                    {
                        viewNameItem.Items.Add(GetColumnItem(view.Columns, folderUri));
                    }

                    if (view.Trigers != null)
                    {
                        viewNameItem.Items.Add(GetTriggerItem(view.Trigers, folderUri));
                    }

                    if (view.Indexes != null)
                    {
                        viewNameItem.Items.Add(GetIndexItem(view.Indexes, folderUri));
                    }

                    viewItem.Items.Add(viewNameItem);
                }

                tree.Items.Add(viewItem);
            }

            if (db.Functions != null)
            {
                var functionItem = GetTreeViewItem("Functions", folderUri);
                foreach (var function in db.Functions)
                {
                    var item = GetTreeViewItem(function.Key, folderUri);
                    foreach (var parametr in function.Value)
                    {
                        var value = string.Format(
                            "{0} ( {1}, {2})", parametr.Parametr, parametr.DataType, parametr.TypeDescription);
                        item.Items.Add(GetTreeViewItem(value, folderUri));
                    }

                    functionItem.Items.Add(item);
                }

                tree.Items.Add(functionItem);
            }

            if (db.Procedures != null)
            {
                var procedureItem = GetTreeViewItem("Procedures", folderUri);
                foreach (var procedure in db.Procedures)
                {
                    var item = GetTreeViewItem(procedure.Key, folderUri);
                    foreach (var parametr in procedure.Value)
                    {
                        var value = string.Format(
                            "{0} ( {1}, {2})", parametr.Parametr, parametr.DataType, parametr.TypeDescription);
                        item.Items.Add(GetTreeViewItem(value, folderUri));
                    }

                    procedureItem.Items.Add(item);
                }

                tree.Items.Add(procedureItem);
            }

            mainWindow.panels.InfoTree.Children.Add(tree);
        }


        public static Uri GetUriString(string imagePath)
        {
            return new Uri("pack://application:,,,/Schema.UI;component/" + imagePath);
        }
    }
}
