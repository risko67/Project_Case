using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace CS2_CaseOpening
{
    public class Skin
    {
        public string Name { get; set; }
        public string Rarity { get; set; }
        public string ImagePath { get; set; }
    }

   

    public partial class CaseOpening : Window
    {
        private Random rnd = new Random();
        private double itemWidth = 130;

        private Dictionary<string, double> rarityChances = new Dictionary<string, double>()
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
            double cumulative = 0;

            foreach (var kvp in rarityChances)
            {
                cumulative += kvp.Value;

                if (roll <= cumulative)
                    return kvp.Key;
            }

            return "Blue";
        }

        private Skin GetRandomSkinByRarity(string rarity)
        {
            var skins = currentCase.Skins.FindAll(s => s.Rarity == rarity);

            return skins[rnd.Next(skins.Count)];
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

                Border b = CreateSkinElement(skin);
                Canvas.SetLeft(b, i * (itemWidth + 5));
                SkinsCanvas.Children.Add(b);
            }
        }

        private Border CreateSkinElement(Skin skin)
        {
            Brush color = skin.Rarity switch
            {
                "Red" => Brushes.Red,
                "Gold" => Brushes.Gold,
                "Pink" => Brushes.DeepPink,
                "Purple" => Brushes.Purple,
                _ => Brushes.Blue
            };

            StackPanel panel = new StackPanel
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center
            };

            if (!string.IsNullOrEmpty(skin.ImagePath))
            {
                Image img = new Image
                {
                    Width = 100,
                    Height = 70,
                    Stretch = Stretch.Uniform,
                    Source = new BitmapImage(new Uri(skin.ImagePath, UriKind.Relative))
                };

                panel.Children.Add(img);
            }

            panel.Children.Add(new TextBlock
            {
                Text = skin.Name,
                Foreground = Brushes.White,
                HorizontalAlignment = HorizontalAlignment.Center,
                TextWrapping = TextWrapping.Wrap
            });

            return new Border
            {
                Width = itemWidth,
                Height = 130,
                Background = new SolidColorBrush(Color.FromRgb(30, 30, 30)),
                BorderBrush = color,
                BorderThickness = new Thickness(0, 0, 0, 5),
                Margin = new Thickness(2),
                Child = panel
            };
        }
        private void btnSpin_Click(object sender, RoutedEventArgs e)
        {
            btnSpin.IsEnabled = false;

            
            string rarity = RollRarity();
            Skin winningSkin = GetRandomSkinByRarity(rarity);

            
            int winningIndex = rnd.Next(45, 50);

          
            PrepareCrate(winningSkin, winningIndex);

            double targetX = -(winningIndex * (itemWidth + 5) - (this.Width / 2) + (itemWidth / 2));

            DoubleAnimation anim = new DoubleAnimation
            {
                To = targetX,
                Duration = TimeSpan.FromSeconds(5),
                EasingFunction = new QuarticEase { EasingMode = EasingMode.EaseOut }
            };

            anim.Completed += (s, ev) =>
            {
                GameData.MySkins.Add(winningSkin);

                MessageBox.Show($"Vyhral si: {winningSkin.Name}!", "Výhra");

                new Inventory("Skins").Show();
                this.Close();
            };

            TranslateTransform trans = new TranslateTransform();
            SkinsCanvas.RenderTransform = trans;
            trans.BeginAnimation(TranslateTransform.XProperty, anim);
        }
    }
}