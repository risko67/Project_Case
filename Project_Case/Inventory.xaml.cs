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

            txtTitle.Text = _mode == "Cases" ? "MOJE BEDNE" : "MOJE SKINY";

            LoadItems();
        }
        protected override void OnClosed(EventArgs e)
        {
            if (Instance != null)
            {
                Instance.Focus();
                return;
            }
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
                VerticalAlignment = VerticalAlignment.Center,
                IsHitTestVisible = false
            };

            if (!string.IsNullOrWhiteSpace(imagePath))
            {
                try
                {
                    Image img = new Image
                    {
                        Width = 80,
                        Height = 60,
                        Stretch = Stretch.Uniform,
                        Source = new BitmapImage(
                            new Uri(imagePath, UriKind.Relative)
                        )
                    };

                    panel.Children.Add(img);
                }
                catch
                {
                    panel.Children.Add(new TextBlock
                    {
                        Text = "IMG ERROR",
                        Foreground = Brushes.Gray,
                        HorizontalAlignment = HorizontalAlignment.Center
                    });
                }
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
                    if (_openCaseWindow != null)
                        _openCaseWindow.Close();

                    _openCaseWindow = new CaseOpening(c);
                    _openCaseWindow.Show();
                }
                else if (data is Skin skin)
                {
                    Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        SkinDetailWindow win = new SkinDetailWindow(skin);
                        win.Owner = Application.Current.MainWindow;
                        win.Show();
                    }));

                }
              
            };

            return slot;
        }

        public void LoadItems()
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
                foreach (var skin in GameData.MySkins.ToList())
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

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MenuWindow menu = new MenuWindow();
            menu.Show();
            
        }
    }
}