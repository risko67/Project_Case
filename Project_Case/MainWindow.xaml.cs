using System.Collections.Generic;
using System.Windows;

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
                lblError.Text = "Používateľ už existuje!";
            }
            else
            {
                
                users.Add(user, pass);
                lblError.Text = "";
                MessageBox.Show("Registrácia úspešná! Teraz sa môžeš prihlásiť.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);

                
                txtPassword.Clear();
            }
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string user = txtUsername.Text.Trim();
            string pass = txtPassword.Password;

            
            if (users.ContainsKey(user) && users[user] == pass)
            {
                lblError.Text = "";
                MessageBox.Show($"Vitaj, {user}! Otváram inventár...", "Úspech", MessageBoxButton.OK, MessageBoxImage.Information);

               
                InventoryWindow invWin = new InventoryWindow();
                invWin.Show(); 
                this.Close();  
            }
            else
            {
                lblError.Text = "Nesprávne meno alebo heslo!";
                txtPassword.Clear();
            }
        }
    }
}