using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows;

namespace CS2_CaseOpening
{
    public partial class SkinDetailWindow : Window
    {
        public SkinDetailWindow(string name, string rarity)
        {
            InitializeComponent();
            txtSkinName.Text = name;
            txtRarity.Text = rarity;
            // Tu by si neskôr nastavil farbu textu podľa rarity
        }

        private void btnSell_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Skin bol predaný za 150 €!", "Predaj");
            this.Close();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}