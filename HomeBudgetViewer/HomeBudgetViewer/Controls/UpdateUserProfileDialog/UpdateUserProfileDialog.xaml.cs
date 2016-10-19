﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using HomeBudgetViewer.Controls.AddUserProfileDialog;
using HomeBudgetViewer.Database.Engine.Entities;

// The Content Dialog item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace HomeBudgetViewer.Controls.UpdateUserProfileDialog
{
    public sealed partial class UpdateUserProfileDialog : ContentDialog
    {
        public UserProfileDialogResult Result { get; set; }
        public User UpdatedUser { get; set; }
        public UpdateUserProfileDialog(User user)
        {
            this.InitializeComponent();
            this.DataContext = new UpdateUserProfileDialogViewModel(this, user); ;
        }

       
    }
}