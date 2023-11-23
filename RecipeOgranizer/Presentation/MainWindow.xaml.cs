// <copyright file="MainWindow.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
        }

        private void LogInButton_Click(object sender, RoutedEventArgs e)
        {
            Login loginPage = new Login();
            loginPage.Show();

            this.Close();
        }

        private void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
            SignUp signUpPage = new SignUp();
            signUpPage.Show();

            this.Close();
        }
    }
}
