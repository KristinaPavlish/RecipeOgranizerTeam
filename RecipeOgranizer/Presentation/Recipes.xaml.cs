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
    /// Interaction logic for Recipes.xaml
    /// </summary>
    public partial class Recipes : Window
    {
        public int BookId;
        public Recipes(int bookid)
        {
            InitializeComponent();
            Loaded += Recipes_Loaded;
            BookId = bookid;
        }

        private void Recipes_Loaded(object sender, RoutedEventArgs e)
        {
            using (RecipeOrganizerContext context = new RecipeOrganizerContext())
            {

                Bll bll = new Bll(context);
                var recipes = bll.GetRecipes(BookId);

                var bookInfo = bll.GetBookById(BookId);
                DataContext = bookInfo;

                RecipesListBox.ItemsSource = recipes;
            }
        }
    }
}
