// <copyright file="Login.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Presentation
{
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
    using System.Windows.Shapes;
    using RecipeOgranizer.Dal.Context;
    using RecipeOgranizer.Dal.Models;
    using RecipeOrganizer.Bll;

    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            this.InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string email = this.EmailTextBox.Text;
            string password = this.PasswordTextBox.Password;

            using (RecipeOrganizerContext context = new RecipeOrganizerContext())
            {
                Bll userService = new Bll(context);

                if (userService.AuthenticateUser(email, password))
                {
                    Books usersBooks = new Books(email);
                    usersBooks.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Unsuccessful login!");
                }
            }
        }
    }
}
