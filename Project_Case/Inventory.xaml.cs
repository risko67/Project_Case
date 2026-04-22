using System.Windows;
using System.Windows.Controls;

namespace CS2_CaseOpening
{
    public partial class InventoryWindow : Window
    {
        public InventoryWindow()
        {
            InitializeComponent();
        }

        private void Slot_Click(object sender, RoutedEventArgs e)
        {
            Button kliknuteTlacidlo = sender as Button;

            if (kliknuteTlacidlo != null)
            {
                MessageBox.Show("Klikol si na " + kliknuteTlacidlo.Name);
            }
        }
    }
}
       