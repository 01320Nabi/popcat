using System;
using System.Collections.Generic;
using System.Media;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PopCat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            ImageSource popcat1 = Imaging.CreateBitmapSourceFromHBitmap(Properties.Resources.popcat1.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            ImageSource popcat2 = Imaging.CreateBitmapSourceFromHBitmap(Properties.Resources.popcat2.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());

            SoundPlayer pop = new SoundPlayer(Properties.Resources.pop);
            pop.LoadAsync();

            bool flipped = false;

            MouseLeftButtonDown += (object sender, MouseButtonEventArgs args) =>
            {
                image.Source = popcat2;
                pop.Play();
            };
            MouseLeftButtonUp += (object sender, MouseButtonEventArgs args) =>
            {
                image.Source = popcat1;
            };

            KeyDown += (object sender, KeyEventArgs args) =>
            {
                if (args.Key == Key.Space)
                {
                    flipped = !flipped;
                    ScaleTransform transform = new ScaleTransform
                    {
                        ScaleX = flipped ? -1 : 1
                    };
                    image.RenderTransform = transform;
                }
            };

            image.Source = popcat1;
        }
    }
}
