// <copyright file="Recipes.xaml.cs" company="PlaceholderCompany">
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
    /// Interaction logic for Recipes.xaml
    /// </summary>
    public partial class Recipes : Window
    {
        public int BookId;

        public Recipes(int bookid)
        {
            this.InitializeComponent();
            this.Loaded += this.Recipes_Loaded;
            this.BookId = bookid;
        }

        private void Recipes_Loaded(object sender, RoutedEventArgs e)
        {
            using (RecipeOrganizerContext context = new RecipeOrganizerContext())
            {

                Bll bll = new Bll(context);
                var recipes = bll.GetRecipes(this.BookId);

                var bookInfo = bll.GetBookById(this.BookId);
                this.DataContext = bookInfo;

                this.RecipesListBox.ItemsSource = recipes;
            }
        }

        private void AddRecipeConfirm_Click(object sender, RoutedEventArgs e)
        {
            string recipeName = NewRecipeNameTextBox.Text;
            string recipeIngredients = NewIngredientsTextBox.Text;
            string recipeProcess = NewProcessTextBox.Text;

            AddRecipe(recipeName, recipeIngredients, recipeProcess);

            RefreshRecipesList();

            AddRecipePanel.Visibility = Visibility.Collapsed;
        }

        private void DeleteRecipe_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            int resipeId = (int)button.Tag;

            DeleteRecipe(resipeId);

            RefreshRecipesList();
        }

        private void RefreshRecipesList()
        {
            using (RecipeOrganizerContext context = new RecipeOrganizerContext())
            {
                Bll bll = new Bll(context);
                List<Recipe> recipes = bll.GetRecipes(BookId);
                RecipesListBox.ItemsSource = recipes;
            }
        }

        private void AddRecipe(string name, string ingredients, string process)
        {
            using (RecipeOrganizerContext context = new RecipeOrganizerContext())
            {
                Bll bll = new Bll(context);
                bll.AddRecipe(name, ingredients, process);
            }
        }

        private void DeleteRecipe(int recipeId)
        {
            using (RecipeOrganizerContext context = new RecipeOrganizerContext())
            {
                Bll bll = new Bll(context);
                bll.DeleteRecipe(recipeId);
            }
        }
    }
}
