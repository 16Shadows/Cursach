﻿#pragma checksum "..\..\..\..\..\UserControls\DatedPageView\DatedPageView.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "C42D7BD685C5E7340A8BB1A283A9C68E4326EFD2"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using ApplicationProject.UserControls;
using ApplicationProject.UserControls.DatedPageView;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
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


namespace ApplicationProject.UserControls.DatedPageView {
    
    
    /// <summary>
    /// DatedPageView
    /// </summary>
    public partial class DatedPageView : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 26 "..\..\..\..\..\UserControls\DatedPageView\DatedPageView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.ToggleButton DateRangeSelector;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\..\..\UserControls\DatedPageView\DatedPageView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox DateRangeTypeSelector;
        
        #line default
        #line hidden
        
        
        #line 56 "..\..\..\..\..\UserControls\DatedPageView\DatedPageView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ContentControl ActiveView;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.13.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/ApplicationProject;component/usercontrols/datedpageview/datedpageview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\UserControls\DatedPageView\DatedPageView.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.13.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 9 "..\..\..\..\..\UserControls\DatedPageView\DatedPageView.xaml"
            ((ApplicationProject.UserControls.DatedPageView.DatedPageView)(target)).SizeChanged += new System.Windows.SizeChangedEventHandler(this.CurrentPage_SizeChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.DateRangeSelector = ((System.Windows.Controls.Primitives.ToggleButton)(target));
            
            #line 26 "..\..\..\..\..\UserControls\DatedPageView\DatedPageView.xaml"
            this.DateRangeSelector.Click += new System.Windows.RoutedEventHandler(this.DateRangeSelector_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.DateRangeTypeSelector = ((System.Windows.Controls.ComboBox)(target));
            
            #line 31 "..\..\..\..\..\UserControls\DatedPageView\DatedPageView.xaml"
            this.DateRangeTypeSelector.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.DateRangeTypeSelector_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 51 "..\..\..\..\..\UserControls\DatedPageView\DatedPageView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ButtonPreviousDateRange_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.ActiveView = ((System.Windows.Controls.ContentControl)(target));
            return;
            case 6:
            
            #line 57 "..\..\..\..\..\UserControls\DatedPageView\DatedPageView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ButtonNextDateRange_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
