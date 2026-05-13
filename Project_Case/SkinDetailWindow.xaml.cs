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
          
            
        }
    }
}