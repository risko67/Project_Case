using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;

namespace CS2_CaseOpening
{
    public partial class SkinDetailWindow : Window
    {
        private Skin _skin;

        public SkinDetailWindow(Skin skin)
        {
            InitializeComponent();

            _skin = skin;

            txtSkinName.Text = skin.Name;
            txtRarity.Text = skin.Rarity;

            imgSkin.Source = new System.Windows.Media.Imaging.BitmapImage(
                new Uri(skin.ImagePath, UriKind.Relative)
            );
        }
        public SkinDetailWindow(string name, string rarity, string imagePath)
        {
            InitializeComponent();

            txtSkinName.Text = name;
            txtRarity.Text = rarity;

            imgSkin.Source = new System.Windows.Media.Imaging.BitmapImage(
                new Uri(imagePath, UriKind.Relative)
            );

            // farba podľa rarity (voliteľné)
            txtRarity.Foreground = rarity switch
            {
                "Blue" => Brushes.Blue,
                "Red" => Brushes.Red,
                "Gold" => Brushes.Gold,
                "Pink" => Brushes.DeepPink,
                "Purple" => Brushes.Purple,
                _ => Brushes.White
            };
        }
        private void btnSell_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_skin == null)
                    return;

                GameData.MySkins.Remove(_skin);

                MessageBox.Show($"Predal si: {_skin.Name}", "Predaj");

                
                Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    foreach (Window w in Application.Current.Windows)
                    {
                        if (w is Inventory inv)
                        {
                            inv.LoadItems();
                        }
                    }
                }));

                
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR");
            }
        }


        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}