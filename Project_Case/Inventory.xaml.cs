using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace CS2_CaseOpening
{
    public partial class Inventory : Window
    {
        private string _mode;

        public Inventory(string mode)
        {
            InitializeComponent();
            _mode = mode;

            txtTitle.Text = _mode == "Cases" ? "MOJE BEDNE" : "MOJE SKINY";

            LoadItems();
        }

        private void LoadItems()
        {
            InventoryGrid.Children.Clear();

            if (_mode == "Cases")
            {
                InventoryGrid.Children.Add(CreateSlot(
                    "Images/FeverCase.png",
                    "Gold",
                    "Case"

                ));
            }
            else
            {
                foreach (var skin in GameData.MySkins)
                {
                    InventoryGrid.Children.Add(CreateSlot(
                        skin.ImagePath,
                        skin.Rarity,
                        "Skin"
                    ));
                }
            }
        }

        private Button CreateSlot(string imagePath, string rarity, string tag)
        {
            Button btn = new Button
            {
                Style = (Style)this.Resources["InventoryButtonStyle"],
                Tag = rarity,
                Height = 150,
                Width = 150
            };

            try
            {
                btn.Content = new Image
                {
                    Source = new BitmapImage(new Uri($"pack://application:,,,/{imagePath}", UriKind.Absolute)),
                    Margin = new Thickness(10)
                };
            }
            catch
            {
                btn.Content = new TextBlock
                {
                    Text = "No Image",
                    Foreground = System.Windows.Media.Brushes.Gray
                };
            }

            btn.Click += (s, e) =>
            {
                if (tag == "Case")
                {
                    CaseOpening openWin = new CaseOpening();
                    openWin.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Prehliadka skinu...");
                }
            };

            return btn;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MenuWindow menu = new MenuWindow();
            menu.Show();
            this.Close();
        }
    }
}