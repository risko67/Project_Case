using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
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

        private UIElement CreateSlot(string imagePath, string rarity, string name, object data)
        {
            Border slot = new Border
            {
                Width = 120,
                Height = 120,
                Margin = new Thickness(5),
                Background = new SolidColorBrush(Color.FromRgb(30, 30, 30)),
                BorderThickness = new Thickness(2),
                BorderBrush = Brushes.Gray,
                Cursor = System.Windows.Input.Cursors.Hand
            };

            StackPanel panel = new StackPanel
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            if (!string.IsNullOrEmpty(imagePath))
            {
                Image img = new Image
                {
                    Width = 80,
                    Height = 60,
                    Stretch = System.Windows.Media.Stretch.Uniform,
                    Source = new System.Windows.Media.Imaging.BitmapImage(new Uri(imagePath, UriKind.Relative))
                };

                panel.Children.Add(img);
            }

            panel.Children.Add(new TextBlock
            {
                Text = name,
                Foreground = Brushes.White,
                TextAlignment = TextAlignment.Center,
                TextWrapping = TextWrapping.Wrap
            });

            slot.Child = panel;

            slot.MouseLeftButtonUp += (s, e) =>
            {
                if (data is Case c)
                {
                    CaseOpening openWin = new CaseOpening(c);
                    openWin.Show();
                }
                else if (data is Skin skin)
                {
                    MessageBox.Show($"Skin: {skin.Name}");
                }
            };

            return slot;
        }
        private void LoadItems()
        {
            InventoryGrid.Children.Clear();

            if (_mode == "Cases")
            {
                foreach (var c in GameData.Cases)
                {
                    InventoryGrid.Children.Add(CreateSlot(
                        c.ImagePath,
                        "Gold",
                        c.Name,
                        c
                    ));
                }
            }
            else
            {
                foreach (var skin in GameData.MySkins)
                {
                    InventoryGrid.Children.Add(CreateSlot(
                        skin.ImagePath,
                        skin.Rarity,
                        skin.Name,
                        skin
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
                    CaseOpening openWin = new CaseOpening(GameData.Cases[0]);
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