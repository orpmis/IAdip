#pragma checksum "..\..\..\Forms\Registration.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "C8F7B2DDEE88F59BF03BE27E2DBC61745AF1E05CCCA42DBEF3FAE0AD890E0B09"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using InstaArt;
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


namespace InstaArt {
    
    
    /// <summary>
    /// Registration
    /// </summary>
    public partial class Registration : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 39 "..\..\..\Forms\Registration.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button CloseApp;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\..\Forms\Registration.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SetFullScreen;
        
        #line default
        #line hidden
        
        
        #line 65 "..\..\..\Forms\Registration.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button HideWindow;
        
        #line default
        #line hidden
        
        
        #line 76 "..\..\..\Forms\Registration.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Nick;
        
        #line default
        #line hidden
        
        
        #line 82 "..\..\..\Forms\Registration.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Email;
        
        #line default
        #line hidden
        
        
        #line 88 "..\..\..\Forms\Registration.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Pass;
        
        #line default
        #line hidden
        
        
        #line 94 "..\..\..\Forms\Registration.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Repeatpass;
        
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
            System.Uri resourceLocater = new System.Uri("/InstaArt;component/forms/registration.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Forms\Registration.xaml"
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
            
            #line 15 "..\..\..\Forms\Registration.xaml"
            ((InstaArt.Registration)(target)).MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.Window_MouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 2:
            this.CloseApp = ((System.Windows.Controls.Button)(target));
            
            #line 40 "..\..\..\Forms\Registration.xaml"
            this.CloseApp.Click += new System.Windows.RoutedEventHandler(this.CloseApp_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.SetFullScreen = ((System.Windows.Controls.Button)(target));
            
            #line 53 "..\..\..\Forms\Registration.xaml"
            this.SetFullScreen.Click += new System.Windows.RoutedEventHandler(this.SetFullScreen_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.HideWindow = ((System.Windows.Controls.Button)(target));
            
            #line 66 "..\..\..\Forms\Registration.xaml"
            this.HideWindow.Click += new System.Windows.RoutedEventHandler(this.HideWindow_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.Nick = ((System.Windows.Controls.TextBox)(target));
            
            #line 78 "..\..\..\Forms\Registration.xaml"
            this.Nick.GotFocus += new System.Windows.RoutedEventHandler(this.Nick_GotFocus);
            
            #line default
            #line hidden
            
            #line 79 "..\..\..\Forms\Registration.xaml"
            this.Nick.LostFocus += new System.Windows.RoutedEventHandler(this.Nick_LostFocus);
            
            #line default
            #line hidden
            return;
            case 6:
            this.Email = ((System.Windows.Controls.TextBox)(target));
            
            #line 84 "..\..\..\Forms\Registration.xaml"
            this.Email.GotFocus += new System.Windows.RoutedEventHandler(this.Email_GotFocus);
            
            #line default
            #line hidden
            
            #line 85 "..\..\..\Forms\Registration.xaml"
            this.Email.LostFocus += new System.Windows.RoutedEventHandler(this.Email_LostFocus);
            
            #line default
            #line hidden
            return;
            case 7:
            this.Pass = ((System.Windows.Controls.TextBox)(target));
            
            #line 90 "..\..\..\Forms\Registration.xaml"
            this.Pass.GotFocus += new System.Windows.RoutedEventHandler(this.Pass_GotFocus);
            
            #line default
            #line hidden
            
            #line 91 "..\..\..\Forms\Registration.xaml"
            this.Pass.LostFocus += new System.Windows.RoutedEventHandler(this.Pass_LostFocus);
            
            #line default
            #line hidden
            return;
            case 8:
            this.Repeatpass = ((System.Windows.Controls.TextBox)(target));
            
            #line 96 "..\..\..\Forms\Registration.xaml"
            this.Repeatpass.GotFocus += new System.Windows.RoutedEventHandler(this.Repeatpass_GotFocus);
            
            #line default
            #line hidden
            
            #line 97 "..\..\..\Forms\Registration.xaml"
            this.Repeatpass.LostFocus += new System.Windows.RoutedEventHandler(this.Repeatpass_LostFocus);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 103 "..\..\..\Forms\Registration.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            
            #line 110 "..\..\..\Forms\Registration.xaml"
            ((System.Windows.Controls.TextBlock)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.TextBlock_MouseDown);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

