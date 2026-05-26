using System;
using System.Windows;
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

            LoadSkin();
        }

        private void LoadSkin()
        {
            if (_skin == null) return;

            txtSkinName.Text = _skin.Name;
            txtRarity.Text = _skin.Rarity;
            txtRarity.Foreground = GetRarityBrush(_skin.Rarity);

            // show float (4 decimal places)
            txtFloat.Text = $"Float: {_skin.Float:F4}";

            if (!string.IsNullOrEmpty(_skin.ImagePath))
            {
                imgSkin.Source = new BitmapImage(new Uri(_skin.ImagePath, UriKind.RelativeOrAbsolute));
            }

            txtPrice.Text = $"Cena: {_skin.Price:F2} €";
        }

        private Brush GetRarityBrush(string rarity)
        {
            return rarity switch
            {
                "Blue" => Brushes.DeepSkyBlue,
                "Purple" => Brushes.MediumPurple,
                "Pink" => Brushes.DeepPink,
                "Red" => Brushes.OrangeRed,
                "Gold" => Brushes.Gold,
                _ => Brushes.White
            };
        }

        private void btnSell_Click(object sender, RoutedEventArgs e)
        {
            if (_skin == null) return;

            var item = GameData.MySkins.Find(x => x.Id == _skin.Id);
            if (item == null) return;

            GameData.Balance += item.Price;
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