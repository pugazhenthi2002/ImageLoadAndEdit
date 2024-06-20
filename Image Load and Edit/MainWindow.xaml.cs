using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Image_Load_and_Edit
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private BitmapImage bitMap;
        private Point offSetPt;
        private bool isSelecting = false;

        private void OnLoadButtonClicked(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image files (*.jpg, *.png, *.jpeg)|*.jpeg||*.jpg||*.png";
            if (dialog.ShowDialog() == true)
            {
                bitMap = new BitmapImage();
                bitMap.BeginInit();
                bitMap.UriSource = new Uri(dialog.FileName);
                bitMap.EndInit();
                imageViewer.Source = bitMap;
                imageViewer.Width = bitMap.Width;
                imageViewer.Height = bitMap.Height;
            }

        }

        private void OnImageViewerMouseDown(object sender, MouseButtonEventArgs e)
        {
            offSetPt = e.GetPosition((sender as Image));
            isSelecting = true;
        }

        private void OnImageViewMouseMove(object sender, MouseEventArgs e)
        {
            if (isSelecting)
            {

            }
        }

        private void OnImageViewerMouseUp(object sender, MouseButtonEventArgs e)
        {
            isSelecting = false;
            Point releasedPoint = e.GetPosition(sender as Image);
            Int32Rect rect = new Int32Rect((int)offSetPt.X, (int)offSetPt.Y, (int)(releasedPoint.X-offSetPt.X), (int)(releasedPoint.Y-offSetPt.Y));
            croppedImageViewer.Source = new CroppedBitmap(bitMap, rect);
        }
    }
}
