using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Controls;

namespace CS2_CaseOpening
{
    // Pomocná trieda pre dáta skinu
    public class Skin
    {
        public string Name { get; set; }
        public string Rarity { get; set; }
        public string ImagePath { get; set; }
    }

    // Statická trieda, ktorá drží tvoje vyhraté skiny (aby nezmizli)
    public static class GameData
    {
        public static List<Skin> MySkins = new List<Skin>();
    }

    public partial class CaseOpening : Window
    {
        private Random rnd = new Random();
        private double itemWidth = 130;

        // ZOZNAM TVOJICH SKINOV (Pripravené na tvoj folder s fotkami)
        private List<Skin> availableSkins = new List<Skin>()
        {
            new Skin { Name = "Glock-18 | Skin 1", Rarity = "Blue", ImagePath = "Images/skin1.png" },
            new Skin { Name = "AK-47 | Skin 2", Rarity = "Red", ImagePath = "Images/skin2.png" },
            new Skin { Name = "M4A4 | Skin 3", Rarity = "Red", ImagePath = "Images/skin3.png" },
            new Skin { Name = "AWP | Skin 4", Rarity = "Gold", ImagePath = "Images/skin4.png" },
            new Skin { Name = "USP-S | Skin 5", Rarity = "Pink", ImagePath = "Images/skin5.png" }
        };

        public CaseOpening()
        {
            InitializeComponent();
            PrepareCrate();
        }

        private void PrepareCrate()
        {
            SkinsCanvas.Children.Clear();
            for (int i = 0; i < 60; i++)
            {
                var randomSkin = availableSkins[rnd.Next(availableSkins.Count)];
                Border b = CreateSkinElement(randomSkin, i);
                Canvas.SetLeft(b, i * (itemWidth + 5));
                SkinsCanvas.Children.Add(b);
            }
        }

        private Border CreateSkinElement(Skin skin, int index)
        {
            Brush color = skin.Rarity switch
            {
                "Red" => Brushes.Red,
                "Gold" => Brushes.Gold,
                "Pink" => Brushes.DeepPink,
                "Purple" => Brushes.Purple,
                _ => Brushes.Blue
            };

            return new Border
            {
                Width = itemWidth,
                Height = 130,
                Background = new SolidColorBrush(Color.FromRgb(30, 30, 30)),
                BorderBrush = color,
                BorderThickness = new Thickness(0, 0, 0, 5),
                Margin = new Thickness(2),
                Child = new TextBlock { Text = skin.Name, Foreground = Brushes.White, HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center }
            };
        }

        private void btnSpin_Click(object sender, RoutedEventArgs e)
        {
            btnSpin.IsEnabled = false;
            int winningIndex = rnd.Next(45, 50); // Vyberieme víťazný slot

            // Zistíme, aký skin je na tom slote
            Border winnerBorder = (Border)SkinsCanvas.Children[winningIndex];
            string wonSkinName = ((TextBlock)winnerBorder.Child).Text;
            Skin wonSkin = availableSkins.Find(s => s.Name == wonSkinName);

            double targetX = -(winningIndex * (itemWidth + 5) - (this.Width / 2) + (itemWidth / 2));

            DoubleAnimation anim = new DoubleAnimation
            {
                To = targetX, 
                Duration = TimeSpan.FromSeconds(5),
                EasingFunction = new QuarticEase { EasingMode = EasingMode.EaseOut }
            };

            anim.Completed += (s, ev) => {
                GameData.MySkins.Add(wonSkin); // ULOŽENIE VÝHRY
                MessageBox.Show($"Vyhral si: {wonSkin.Name}!", "Výhra");
                new Inventory("Skins").Show();
                this.Close();
            };

            TranslateTransform trans = new TranslateTransform();
            SkinsCanvas.RenderTransform = trans;
            trans.BeginAnimation(TranslateTransform.XProperty, anim);
        }
    }
}