#pragma checksum "..\..\..\..\Forms\Pages\DialogPage.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "DF2A979A85E599548FD1D1B2537637E348FA416D6163242CD124979F5D9D4A54"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using InstaArt.Forms.Pages;
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


namespace InstaArt.Forms.Pages {
    
    
    /// <summary>
    /// DialogPage
    /// </summary>
    public partial class DialogPage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 13 "..\..\..\..\Forms\Pages\DialogPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid MainGrid;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\..\Forms\Pages\DialogPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel Messages;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\..\..\Forms\Pages\DialogPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid MessageInputGrid;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\..\..\Forms\Pages\DialogPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image MessageUpload;
        
        #line default
        #line hidden
        
        
        #line 61 "..\..\..\..\Forms\Pages\DialogPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SetSendMode;
        
        #line default
        #line hidden
        
        
        #line 72 "..\..\..\..\Forms\Pages\DialogPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox MessageInputBox;
        
        #line default
        #line hidden
        
        
        #line 86 "..\..\..\..\Forms\Pages\DialogPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button RedactMessage;
        
        #line default
        #line hidden
        
        
        #line 94 "..\..\..\..\Forms\Pages\DialogPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button DeleteMessage;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/InstaArt;component/forms/pages/dialogpage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Forms\Pages\DialogPage.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.MainGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 2:
            this.Messages = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 3:
            this.MessageInputGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 4:
            this.MessageUpload = ((System.Windows.Controls.Image)(target));
            
            #line 51 "..\..\..\..\Forms\Pages\DialogPage.xaml"
            this.MessageUpload.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.MessageUpload_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.SetSendMode = ((System.Windows.Controls.Button)(target));
            
            #line 69 "..\..\..\..\Forms\Pages\DialogPage.xaml"
            this.SetSendMode.Click += new System.Windows.RoutedEventHandler(this.SetSendMode_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.MessageInputBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 80 "..\..\..\..\Forms\Pages\DialogPage.xaml"
            this.MessageInputBox.TextInput += new System.Windows.Input.TextCompositionEventHandler(this.MessageInputBox_TextInput);
            
            #line default
            #line hidden
            return;
            case 7:
            this.RedactMessage = ((System.Windows.Controls.Button)(target));
            
            #line 89 "..\..\..\..\Forms\Pages\DialogPage.xaml"
            this.RedactMessage.Click += new System.Windows.RoutedEventHandler(this.RedactMessage_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.DeleteMessage = ((System.Windows.Controls.Button)(target));
            
            #line 97 "..\..\..\..\Forms\Pages\DialogPage.xaml"
            this.DeleteMessage.Click += new System.Windows.RoutedEventHandler(this.DeleteMEssage_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

