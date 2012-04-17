namespace Aga.Controls.Tree
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;

    public class TreeList : ListView
    {
        #region Properties
     
        private ITreeModel model;

        public ITreeModel Model
        {
            get
            {
                return this.model;
            }

            set
            {
                if (this.model == value)
                {
                    return;
                }

                this.model = value;
                this.Root.Children.Clear();
                this.Rows.Clear();
                this.CreateChildrenNodes(this.Root);
            }
        }
       
        public ReadOnlyCollection<TreeNode> Nodes
        {
            get
            {
                return this.Root.Nodes;
            }
        }
        
        public ICollection<TreeNode> SelectedNodes
        {
            get
            {
                return SelectedItems.Cast<TreeNode>().ToArray();
            }
        }

        public TreeNode SelectedNode
        {
            get
            {
                if (SelectedItems.Count > 0)
                {
                    return SelectedItems[0] as TreeNode;
                }

                return null;
            }
        }

        internal ObservableCollectionAdv<TreeNode> Rows { get; private set; }

        internal TreeNode Root { get; private set; }

        internal TreeNode PendingFocusNode { get; set; }


        public TreeList()
        {
            this.Rows = new ObservableCollectionAdv<TreeNode>();
            this.Root = new TreeNode(this, null) { IsExpanded = true };
            this.ItemsSource = this.Rows;
            this.ItemContainerGenerator.StatusChanged += this.ItemContainerGeneratorStatusChanged;
        }
        #endregion
     


        internal void DropChildrenRows(TreeNode node, bool removeParent)
        {
            var start = this.Rows.IndexOf(node);

            // ignore invisible nodes
            if (start < 0 && node != this.Root)
            {
                return;
            }

            var count = node.VisibleChildrenCount;
            if (removeParent)
            {
                count++;
            }
            else
            {
                start++;
            }

            this.Rows.RemoveRange(start, count);
        }
        
        internal void InsertNewNode(TreeNode parent, object tag, int rowIndex, int index)
        {
            var node = new TreeNode(this, tag);
            if (index >= 0 && index < parent.Children.Count)
            {
                parent.Children.Insert(index, node);
            }
            else
            {
                index = parent.Children.Count;
                parent.Children.Add(node);
            }

            this.Rows.Insert(rowIndex + index + 1, node);
        }

        internal void SetIsExpanded(TreeNode node, bool value)
        {
            if (value)
            {
                if (!node.IsExpandedOnce)
                {
                    node.IsExpandedOnce = true;
                }

                node.AssignIsExpanded(value);
                this.CreateChildrenRows(node);
            }
            else
            {
                this.DropChildrenRows(node, false);
                node.AssignIsExpanded(value);
            }
        }

        internal void CreateChildrenNodes(TreeNode node)
        {
            var children = this.GetChildren(node);
            if (children == null)
            {
                return;
            }

            var rowIndex = this.Rows.IndexOf(node);
            node.ChildrenSource = children as INotifyCollectionChanged;
            foreach (var obj in children)
            {
                var child = new TreeNode(this, obj);
                child.HasChildren = this.HasChildren(child);
                node.Children.Add(child);
            }

            this.Rows.InsertRange(rowIndex + 1, node.Children.ToArray());
        }

        private void ItemContainerGeneratorStatusChanged(object sender, EventArgs e)
        {
            if (this.ItemContainerGenerator.Status != GeneratorStatus.ContainersGenerated
                || this.PendingFocusNode == null)
            {
                return;
            }

            var item = this.ItemContainerGenerator.ContainerFromItem(this.PendingFocusNode) as TreeListItem;
            if (item != null)
            {
                item.Focus();
            }

            this.PendingFocusNode = null;
        }

        private void CreateChildrenRows(TreeNode node)
        {
            var index = this.Rows.IndexOf(node);
            if (index >= 0 || node == this.Root) // ignore invisible nodes
            {
                var nodes = node.AllVisibleChildren.ToArray();
                this.Rows.InsertRange(index + 1, nodes);
            }
        }

        private IEnumerable GetChildren(TreeNode parent)
        {
            return this.Model != null ? this.Model.GetChildren(parent.Tag) : null;
        }

        private bool HasChildren(TreeNode parent)
        {
            if (parent == this.Root)
            {
                return true;
            }

            return this.Model != null && this.Model.HasChildren(parent.Tag);
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new TreeListItem();
        }

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is TreeListItem;
        }

        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            var ti = element as TreeListItem;
            var node = item as TreeNode;
            if (ti == null || node == null)
            {
                return;
            }

            ti.Node = item as TreeNode;
            base.PrepareContainerForItemOverride(element, node.Tag);
        }
    }
}