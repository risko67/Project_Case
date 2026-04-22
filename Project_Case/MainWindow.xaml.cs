using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace CS2_CaseOpening
{
    public partial class MainWindow : Window
    {
        private Dictionary<string, string> users = new Dictionary<string, string>()
        {
            { "admin", "cs2best" }
        };

        public MainWindow()
        {
            InitializeComponent();
            txtUsername.Focus();
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            string user = txtUsername.Text.Trim();
            string pass = txtPassword.Password;

            if (string.IsNullOrWhiteSpace(user) || string.IsNullOrWhiteSpace(pass))
            {
                lblError.Text = "Meno a heslo nesmú byť prázdne!";
                return;
            }

            if (users.ContainsKey(user))
            {
                lblError.Text = "Tento používateľ už existuje!";
            }
            else
            {
                users.Add(user, pass);
                lblError.Text = "";
                MessageBox.Show("Registrácia úspešná!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            HandleLogin();
        }

        private void HandleLogin()
        {
            string user = txtUsername.Text.Trim();
            string pass = txtPassword.Password;

            if (users.TryGetValue(user, out string storedPass) && storedPass == pass)
            {
                // 1. Vytvoríme MENU, nie inventár
                MenuWindow menuWin = new MenuWindow();

                // 2. Zobrazíme menu
                menuWin.Show();

                // 3. Zatvoríme prihlasovacie okno
                this.Close();
            }
            else
            {
                lblError.Text = "Nesprávne meno alebo heslo!";
            }
        }
        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) HandleLogin();
        }
    }
}