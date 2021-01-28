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
using System.Windows.Threading;

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
            ImageSource loading = Imaging.CreateBitmapSourceFromHBitmap(Properties.Resources.spinner.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());

            SoundPlayer pop = new SoundPlayer(Properties.Resources.pop);
            pop.LoadAsync();

            bool flipped = false;
            bool load = false;

            int t = 0;
            DispatcherTimer timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(0.05),
                IsEnabled = false
            };
            timer.Tick += (object sender, EventArgs args) =>
            {
                if (++t >= 12)
                    t = 0;
                RotateTransform transform = new RotateTransform
                {
                    Angle = t * 30
                };
                spinner.RenderTransform = transform;
            };

            MouseLeftButtonDown += (object sender, MouseButtonEventArgs args) =>
            {
                if (!load)
                {
                    image.Source = popcat2;
                    pop.Play();
                }
            };
            MouseLeftButtonUp += (object sender, MouseButtonEventArgs args) =>
            {
                if (!load)
                {
                    image.Source = popcat1;
                }
            };
            MouseRightButtonDown += (object sender, MouseButtonEventArgs args) =>
            {
                spinner.Visibility = Visibility.Visible;
                image.Source = popcat2;
                load = true;
                timer.IsEnabled = true;
            };
            MouseRightButtonUp += (object sender, MouseButtonEventArgs args) =>
            {
                spinner.Visibility = Visibility.Hidden;
                image.Source = popcat1;
                load = false;
                timer.IsEnabled = false;
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

            timer.Start();

            image.Source = popcat1;
            spinner.Source = loading;
        }
    }
}
