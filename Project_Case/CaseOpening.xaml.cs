using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace CS2_CaseOpening
{
    public partial class CaseOpening : Window
    {
        Random rnd = new Random();

        double itemWidth = 130;

        Dictionary<string, double> rarityChances = new Dictionary<string, double>()
        {
            { "Blue", 50.0 },
            { "Purple", 20.0 },
            { "Pink", 15.0 },
            { "Red", 10.0 },
            { "Gold", 5.0 }
        };

        private Case currentCase;

        public CaseOpening(Case selectedCase)
        {
            InitializeComponent();
            currentCase = selectedCase;

            // Update the button text to show the actual case price (fallback to 2.50)
            double displayPrice = currentCase?.Price ?? 2.50;
            btnSpin.Content = $"OPEN CASE ({GameData.CurrencySymbol}{displayPrice:0.00})";
        }

        private string RollRarity()
        {
            double roll = rnd.NextDouble() * 100;
            double total = 0;

            foreach (var item in rarityChances)
            {
                total += item.Value;

                if (roll <= total)
                    return item.Key;
            }

            return "Blue";
        }

        private void GenerateFloat(Skin skin)
        {
            // Generate a float between 0.0 and 1.0
            skin.Float = Math.Round(rnd.NextDouble(), 4);

            // Derive wear from float (lower float => better condition)
            if (skin.Float <= 0.07) skin.Wear = "Factory New";
            else if (skin.Float <= 0.15) skin.Wear = "Minimal Wear";
            else if (skin.Float <= 0.38) skin.Wear = "Field-Tested";
            else if (skin.Float <= 0.45) skin.Wear = "Well-Worn";
            else skin.Wear = "Battle-Scarred";
        }

        private Skin GetRandomSkinByRarity(string rarity)
        {
            var skins = currentCase.Skins.FindAll(
                s => s.Rarity.Equals(rarity, StringComparison.OrdinalIgnoreCase)
            );

            // If rarity doesn't exist -> pick random skin from case
            if (skins.Count == 0)
            {
                skins = currentCase.Skins;
            }

            var baseSkin = skins[rnd.Next(skins.Count)];

            Skin newSkin = new Skin
            {
                Id = Guid.NewGuid(),
                Name = baseSkin.Name,
                Rarity = baseSkin.Rarity,
                ImagePath = baseSkin.ImagePath,
                Price = baseSkin.Price // seed with base initializer
            };

            GenerateFloat(newSkin);

            // ALWAYS compute final price using base initializer + float + rarity
            newSkin.Price = ComputePriceFromBaseAndFloat(baseSkin.Price, newSkin.Rarity, newSkin.Float);

            return newSkin;
        }

        // new: compute final sell price from base initializer and float for ALL skins
        private double ComputePriceFromBaseAndFloat(double basePrice, string rarity, double fl)
        {
            // small safeguards
            if (basePrice < 0.01) basePrice = Math.Max(basePrice, 0.05);

            // rarity influence on how strongly float affects price
            double rarityBoost = rarity switch
            {
                "Blue" => 0.6,
                "Purple" => 0.85,
                "Pink" => 1.0,
                "Red" => 1.2,
                "Gold" => 1.4,
                _ => 1.0
            };

            // lower float => higher price
            double t = Math.Clamp(1.0 - fl, 0.0, 1.0);

            // multiplier ranges from ~0.5 (poor float) up to higher depending on rarityBoost
            double multiplier = 0.5 + t * (1.5 * rarityBoost);

            double price = Math.Round(Math.Max(0.05, basePrice * multiplier), 2);

            return price;
        }

        private void PrepareCrate(Skin forcedWinner, int winningIndex)
        {
            SkinsCanvas.Children.Clear();

            for (int i = 0; i < 60; i++)
            {
                Skin skin;

                if (i == winningIndex)
                    skin = forcedWinner;
                else
                    skin = GetRandomSkinByRarity(RollRarity());

                Border border = CreateSkinElement(skin);

                Canvas.SetLeft(border, i * (itemWidth + 5));

                SkinsCanvas.Children.Add(border);
            }
        }

        private Border CreateSkinElement(Skin skin)
        {
            Brush color = skin.Rarity switch
            {
                "Purple" => Brushes.Purple,
                "Pink" => Brushes.DeepPink,
                "Red" => Brushes.Red,
                "Gold" => Brushes.Gold,
                _ => Brushes.Blue
            };

            StackPanel panel = new StackPanel();

            if (!string.IsNullOrEmpty(skin.ImagePath))
            {
                panel.Children.Add(new Image
                {
                    Width = 100,
                    Height = 70,
                    Stretch = Stretch.Uniform,
                    Source = new BitmapImage(new Uri(skin.ImagePath, UriKind.Relative))
                });
            }

            panel.Children.Add(new TextBlock
            {
                Text = skin.Name,
                Foreground = Brushes.White,
                HorizontalAlignment = HorizontalAlignment.Center
            });

            panel.Children.Add(new TextBlock
            {
                Text = skin.Wear,
                Foreground = Brushes.LightGray,
                HorizontalAlignment = HorizontalAlignment.Center
            });

            return new Border
            {
                Width = itemWidth,
                Height = 150,
                Background = new SolidColorBrush(Color.FromRgb(30, 30, 30)),
                BorderBrush = color,
                BorderThickness = new Thickness(0, 0, 0, 5),
                Margin = new Thickness(2),
                Child = panel
            };
        }

        private void btnSpin_Click(object sender, RoutedEventArgs e)
        {
            // Use the current case price (fallback to 2.50)
            double caseCost = currentCase?.Price ?? 2.50;

            if (GameData.Balance < caseCost)
            {
                MessageBox.Show($"Nedostatok prostriedkov. Potrebujete {GameData.CurrencySymbol}{caseCost:0.00}.", "Nedostatok", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Deduct cost
            GameData.Balance -= caseCost;

            // persist change immediately for autosave
            AccountsManager.SaveCurrentAccount();

            // disable controls while animation runs
            btnSpin.IsEnabled = false;
            btnBackToInventory.IsEnabled = false;

            string rarity = RollRarity();

            Skin winningSkin = GetRandomSkinByRarity(rarity);

            int winningIndex = rnd.Next(45, 50);

            PrepareCrate(winningSkin, winningIndex);

            double targetX =
                -(winningIndex * (itemWidth + 5)
                - (this.Width / 2)
                + (itemWidth / 2));

            DoubleAnimation anim = new DoubleAnimation
            {
                To = targetX,
                Duration = TimeSpan.FromSeconds(5),
                EasingFunction = new QuarticEase { EasingMode = EasingMode.EaseOut }
            };

            anim.Completed += (s, ev) =>
            {
                GameData.MySkins.Add(winningSkin);

                // persist inventory change
                AccountsManager.SaveCurrentAccount();

                MessageBox.Show(
                    $"Vyhral si:\n{winningSkin.Name}\n{winningSkin.Wear}"
                );

                new Inventory("Skins").Show();
                this.Close();
            };

            SkinsCanvas.RenderTransform = new TranslateTransform();
            SkinsCanvas.RenderTransform.BeginAnimation(
                TranslateTransform.XProperty,
                anim
            );
        }

        private void btnBackToInventory_Click(object sender, RoutedEventArgs e)
        {
            // Prevent going back while a spin is running
            if (!btnSpin.IsEnabled)
            {
                MessageBox.Show("Can't go back while the case is opening.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            // Return to cases list
            new Inventory("Cases").Show();
            this.Close();
        }
    }
}