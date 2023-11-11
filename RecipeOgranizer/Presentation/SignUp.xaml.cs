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
    /// Interaction logic for SignUp.xaml
    /// </summary>
    public partial class SignUp : Window
    {
        public SignUp()
        {
            InitializeComponent();
        }
        private void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
            string email = EmailTextBox.Text;
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            using (RecipeOrganizerContext context = new RecipeOrganizerContext())
            {
                Bll bll = new Bll(context);
                string message = "";

                if (bll.RegisterUser(email, username, password, ref message))
                {
                    Books usersBooks = new Books(email);
                    usersBooks.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show(message);
                }
            }
        }
    }
}
