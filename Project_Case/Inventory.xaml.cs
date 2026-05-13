using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using CS2_CaseOpening;

namespace CS2_CaseOpening
{
    public partial class Inventory : Window
    {
        private string _mode;
        private CaseOpening _openCaseWindow;

        public static Inventory Instance;

        public Inventory(string mode)
        {
            if (Instance != null)
            {
                Instance.Close();
            }

            Instance = this;

            InitializeComponent();

            _mode = mode;

            txtTitle.Text = _mode == "Cases"
                ? "MOJE BEDNE"
                : "MOJE SKINY";

            UpdateBalanceText();
            LoadItems();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            Instance = null;
        }

        private void UpdateBalanceText()
        {
            txtBalance.Text = "Balance: $" + GameData.Balance;
        }

        private Brush GetRarityColor(string rarity)
        {
            if (rarity == "Blue") return Brushes.Blue;
            if (rarity == "Purple") return Brushes.Purple;
            if (rarity == "Pink") return Brushes.DeepPink;
            if (rarity == "Red") return Brushes.Red;
            if (rarity == "Gold") return Brushes.Gold;

            return Brushes.Gray;
        }

        private UIElement CreateSlot(string imagePath, string rarity, string name, object data)
        {
            Border slot = new Border
            {
                Width = 120,
                Height = 120,
                Margin = new Thickness(5),
                Background = new SolidColorBrush(Color.FromRgb(30, 30, 30)),
                BorderThickness = new Thickness(2),
                BorderBrush = GetRarityColor(rarity),
                Cursor = System.Windows.Input.Cursors.Hand
            };

            StackPanel panel = new StackPanel
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                IsHitTestVisible = false
            };

            if (!string.IsNullOrWhiteSpace(imagePath))
            {
                panel.Children.Add(new Image
                {
                    Width = 80,
                    Height = 60,
                    Stretch = Stretch.Uniform,
                    Source = new BitmapImage(new Uri(imagePath, UriKind.RelativeOrAbsolute))
                });
            }

            panel.Children.Add(new TextBlock
            {
                Text = name,
                Foreground = Brushes.White,
                TextAlignment = TextAlignment.Center
            });

            // SELL
            slot.MouseRightButtonUp += (s, e) =>
            {
                if (data is Skin skin)
                {
                    var item = GameData.MySkins
                        .FirstOrDefault(x => x.Id == skin.Id);

                    if (item == null) return;

                    GameData.Balance += item.Price;
                    GameData.MySkins.Remove(item);

                    UpdateBalanceText();
                    LoadItems();
                }
            };

            slot.Child = panel;
            return slot;
        }

        public void LoadItems()
        {
            InventoryGrid.Children.Clear();

            if (_mode == "Cases")
            {
                foreach (var c in GameData.Cases)
                {
                    InventoryGrid.Children.Add(CreateSlot(c.ImagePath, "Gold", c.Name, c));
                }
            }
            else
            {
                foreach (var skin in GameData.MySkins.ToList())
                {
                    InventoryGrid.Children.Add(CreateSlot(skin.ImagePath, skin.Rarity, skin.Name, skin));
                }
            }

            UpdateBalanceText();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            new MenuWindow().Show();
            this.Close();
        }
    }
}