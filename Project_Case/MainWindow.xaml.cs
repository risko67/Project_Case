using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace CS2_CaseOpening
{
    public partial class MainWindow : Window
    {
        // kept for compatibility with older logic; real storage is in AccountsManager
        private Dictionary<string, string> users = new Dictionary<string, string>()
        {
            { "admin", "cs2best" }
        };

        public MainWindow()
        {
            InitializeComponent();

            // Load persisted accounts
            AccountsManager.LoadAccounts();

            // Ensure at least default admin exists (optional)
            if (!AccountsManager.TryGetAccount("admin", out var _))
            {
                var admin = new Account { Username = "admin", Password = "cs2best", Balance = 100.0 };
                AccountsManager.AddAccount(admin);
            }

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

            // create new account with default starting balance and empty inventory
            var account = new Account
            {
                Username = user,
                Password = pass,
                Balance = 100.0,
                MySkins = new System.Collections.Generic.List<Skin>()
            };

            if (!AccountsManager.AddAccount(account))
            {
                lblError.Text = "Tento používateľ už existuje!";
            }
            else
            {
                lblError.Text = "";
                MessageBox.Show("Registrácia úspešná!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);

                // Automatically log in the new user and populate GameData
                AccountsManager.Login(user, pass);

                // Clear inputs so user can immediately use the login fields (or proceed)
                txtUsername.Clear();
                txtPassword.Clear();
                txtUsername.Focus();
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

            if (AccountsManager.Login(user, pass))
            {
                // GameData is now synced from account
                MenuWindow menuWin = new MenuWindow();
                menuWin.Show();
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