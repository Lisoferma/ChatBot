using System;
using System.Windows;

namespace ChatBotGUI
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        // Для передачи логина.
        private readonly MainWindow _mainWindow;

        // Имя пользователя.
        private string _login = "User";

        /// <summary>
        /// Имя пользователя.
        /// </summary>
        public string Login
        {
            get => _login;

            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException(
                        nameof(Login), "Login cannot be null or empty");
                }

                _login = value;
            }
        }


        public LoginWindow(MainWindow window)
        {
            InitializeComponent();
            _mainWindow = window;
        }


        /// <summary>
        /// Обработчик нажатия на кнопку <see cref="Button_login"/>.
        /// Передаёт логин в <see cref="_mainWindow"/>.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_login_Click(object sender, RoutedEventArgs e)
        {
            if (TextBox_login.Text.Length <= 0)
            {
                MessageBox.Show("Введите имя");
                return;
            }

            Login = TextBox_login.Text;
            _mainWindow.Username = Login;

            Close();
        }
    }
}
