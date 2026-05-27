using System.Windows;

namespace CS2_CaseOpening
{
    public partial class MenuWindow : Window
    {
        public MenuWindow()
        {
            InitializeComponent();
        }

      
        private void btnGoToCases_Click(object sender, RoutedEventArgs e)
        {
            Inventory inv = new Inventory("Cases");
            inv.Show();
            this.Close();
        }

       
        private void btnGoToSkins_Click(object sender, RoutedEventArgs e)
        {
            Inventory inv = new Inventory("Skins");
            inv.Show();
            this.Close();
        }

        
        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            MainWindow login = new MainWindow();
            login.Show();
            this.Close();
        }
    }
}