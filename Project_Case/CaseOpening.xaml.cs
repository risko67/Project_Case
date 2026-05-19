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
            var skins = currentCase.Skins.FindAll(s => s.Rarity == rarity);

            var baseSkin = skins[rnd.Next(skins.Count)];

            Skin newSkin = new Skin
            {
                Id = Guid.NewGuid(),
                Name = baseSkin.Name,
                Rarity = baseSkin.Rarity,
                ImagePath = baseSkin.ImagePath,
                Price = baseSkin.Price
            };

            // assign float and wear
            GenerateFloat(newSkin);

            // compute price based on rarity and float
            newSkin.Price = ComputePriceByRarityAndFloat(newSkin.Rarity, newSkin.Float);

            return newSkin;
        }

        private int ComputePriceByRarityAndFloat(string rarity, double fl)
        {
            // Price ranges per rarity (min, max)
            (double min, double max) range = rarity switch
            {
                "Blue" => (1.0, 3.0),
                "Purple" => (3.0, 7.0),
                "Pink" => (7.0, 30.0),
                "Red" => (30.0, 90.0),
                "Gold" => (90.0, 400.0),
                _ => (1.0, 3.0)
            };

            // lower float -> higher price, so invert float when interpolating
            double t = Math.Clamp(1.0 - fl, 0.0, 1.0);

            double price = range.min + t * (range.max - range.min);

            // Round to nearest integer dollar
            return (int)Math.Round(price);
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
            // cost to open a case
            const double caseCost = 2.50;

            if (GameData.Balance < caseCost)
            {
                MessageBox.Show("Nedostatok prostriedkov. Potrebujete $2.50.", "Nedostatok", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Deduct cost
            GameData.Balance -= caseCost;

            btnSpin.IsEnabled = false;

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
    }
}