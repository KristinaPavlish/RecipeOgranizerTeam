using System.Windows;
using RecipeOgranizer.Dal.Context;
using RecipeOrganizer.BLL;

namespace View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordTextBox.Password;

            using (RecipeOrganizerContext context = new RecipeOrganizerContext())
            {
                UserService userService = new UserService(context);

                if (userService.AuthenticateUser(username, password))
                {
                    MessageBox.Show("Успішно авторизовано.");
                }
                else
                {
                    MessageBox.Show("Не вдалося авторизуватися. Перевірте ім'я користувача та пароль.");
                }
            }
        }
    }
}
