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
using System.Windows.Media.Imaging;

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

            switch (skin.Rarity)
            {
                case "Blue":
                    txtRarity.Foreground = Brushes.DeepSkyBlue;
                    break;

                case "Purple":
                    txtRarity.Foreground = Brushes.MediumPurple;
                    break;

                case "Pink":
                    txtRarity.Foreground = Brushes.DeepPink;
                    break;

                case "Red":
                    txtRarity.Foreground = Brushes.Red;
                    break;

                case "Gold":
                    txtRarity.Foreground = Brushes.Gold;
                    break;

                default:
                    txtRarity.Foreground = Brushes.White;
                    break;
            }

            imgSkin.Source = new BitmapImage(
                new Uri(skin.ImagePath, UriKind.Relative)
            );
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