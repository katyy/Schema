﻿#pragma checksum "..\..\..\..\TreeViewList\ServersTreeView.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "B4B9A49AAA136B8523226F51628BD365"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Aga.Controls.Tree;
using Schema.UI.Converters;
using Schema.UI.EditBox;
using Schema.UI.TreeViewList;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace Schema.UI.TreeViewList {
    
    
    /// <summary>
    /// ServersTreeView
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
    public partial class ServersTreeView : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 38 "..\..\..\..\TreeViewList\ServersTreeView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Aga.Controls.Tree.TreeList _tree;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\..\..\TreeViewList\ServersTreeView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.GridViewColumn color;
        
        #line default
        #line hidden
        
        
        #line 73 "..\..\..\..\TreeViewList\ServersTreeView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.GridViewColumn User;
        
        #line default
        #line hidden
        
        
        #line 83 "..\..\..\..\TreeViewList\ServersTreeView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.GridViewColumn Password;
        
        #line default
        #line hidden
        
        
        #line 93 "..\..\..\..\TreeViewList\ServersTreeView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.GridViewColumn OK;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Schema.UI;component/treeviewlist/serverstreeview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\TreeViewList\ServersTreeView.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this._tree = ((Aga.Controls.Tree.TreeList)(target));
            return;
            case 2:
            this.color = ((System.Windows.Controls.GridViewColumn)(target));
            return;
            case 3:
            this.User = ((System.Windows.Controls.GridViewColumn)(target));
            return;
            case 4:
            this.Password = ((System.Windows.Controls.GridViewColumn)(target));
            return;
            case 5:
            this.OK = ((System.Windows.Controls.GridViewColumn)(target));
            return;
            }
            this._contentLoaded = true;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        void System.Windows.Markup.IStyleConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 6:
            
            #line 97 "..\..\..\..\TreeViewList\ServersTreeView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.OkClick);
            
            #line default
            #line hidden
            break;
            }
        }
    }
}

