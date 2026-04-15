using System.Windows;

namespace CS2_CaseOpening
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Registration is currently unavailable. Please contact support.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        
        
        
        
        ,private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string user = txtUsername.Text;
            string pass = txtPassword.Password;

            if (user == "admin" && pass == "cs2best")
            {
                MessageBox.Show("Login successful! Opening inventory...", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

               
            }
            else
            {
                lblError.Text = "Invalid username or password!";
                txtPassword.Clear();
            }
        }
    }
}