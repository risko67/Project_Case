using System;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Xml.Linq;
using CS2_CaseOpening;

namespace CS2_CaseOpening
{
    public partial class SkinDetailWindow : Window
    {
        private Skin _skin;

        public SkinDetailWindow(Skin skin)
        {
            InitializeComponent();
            _skin = skin;

            LoadSkin();
        }

        private void LoadSkin()
        {
            if (_skin == null) return;

            txtSkinName.Text = _skin.Name;
            txtRarity.Text = _skin.Rarity;

            if (!string.IsNullOrEmpty(_skin.ImagePath))
            {
                imgSkin.Source = new BitmapImage(new Uri(_skin.ImagePath, UriKind.RelativeOrAbsolute));
            }

            var priceValue = Convert.ToDouble(_skin.Price);
            txtPrice.Text = $"Cena: {priceValue:F2} €";
        }

        private void btnSell_Click(object sender, RoutedEventArgs e)
        {
            if (_skin == null) return;

            var item = GameData.MySkins.Find(x => x.Id == _skin.Id);
            if (item == null) return;

            GameData.Balance += Convert.ToDouble(item.Price);
            GameData.MySkins.Remove(item);

            MessageBox.Show($"Predali ste: {item.Name}", "Info", MessageBoxButton.OK, MessageBoxImage.Information);

            this.Close();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}