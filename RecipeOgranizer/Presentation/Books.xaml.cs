// <copyright file="Books.xaml.cs" company="PlaceholderCompany">
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
using System.Windows.Shapes;

using RecipeOgranizer.Dal.Context;
using RecipeOgranizer.Dal.Models;
using RecipeOrganizer.Bll;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for Books.xaml.
    /// </summary>
    public partial class Books : Window
    {
        public string CurrentEmail { get; set; }

        public Books(string currentEmail)
        {
            this.InitializeComponent();
            this.Loaded += this.UsersBooks_Loaded;
            this.CurrentEmail = currentEmail;
        }

        private void UsersBooks_Loaded(object sender, RoutedEventArgs e)
        {
            using RecipeOrganizerContext context = new ();
            Bll bll = new (context);
            List<Cookerybook> books = bll.GetUserCookerybooks(this.CurrentEmail);
            this.BooksListView.ItemsSource = books;
        }

        private void ShowDetails_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            int bookId = (int)button.Tag;

            var recipesWindow = new Recipes(bookId);
            recipesWindow.Show();
            this.Close();
        }

        private void AddBookConfirm_Click(object sender, RoutedEventArgs e)
        {
            string bookName = this.NewBookNameTextBox.Text;
            string bookDescription = this.NewBookDescriptionTextBox.Text;

            AddBook(bookName, bookDescription);

            this.RefreshBooksList();

            this.AddBookPanel.Visibility = Visibility.Collapsed;
        }

        private void DeleteBook_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            int bookId = (int)button.Tag;

            this.DeleteBook(bookId);

            this.RefreshBooksList();
        }

        private void RefreshBooksList()
        {
            using (RecipeOrganizerContext context = new RecipeOrganizerContext())
            {
                Bll bll = new Bll(context);
                List<Cookerybook> books = bll.GetUserCookerybooks(this.CurrentEmail);
                this.BooksListView.ItemsSource = books;
            }
        }

        private static void AddBook(string name, string description)
        {
            using (RecipeOrganizerContext context = new RecipeOrganizerContext())
            {
                Bll bll = new Bll(context);
                bll.AddBook(name, description);
            }
        }

        private void DeleteBook(int bookId)
        {
            using (RecipeOrganizerContext context = new RecipeOrganizerContext())
            {
                Bll bll = new Bll(context);
                bll.DeleteBook(bookId);
            }
        }


    }
}