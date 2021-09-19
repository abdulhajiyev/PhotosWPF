using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using Microsoft.Win32;

namespace PhotosWPF
{
    [MarkupExtensionReturnType(typeof(DrawingImage))]
    public class IconImageExtension : StaticResourceExtension
    {
        private static readonly FontFamily FontFamily = new FontFamily("Segoe MDL2 Assets");

        public int SymbolCode { get; set; }
        public double SymbolSize { get; set; } = 16;

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            var textBlock = new TextBlock
            {
                FontFamily = FontFamily,
                Text = char.ConvertFromUtf32(SymbolCode)
            };

            var brush = new VisualBrush
            {
                Visual = textBlock,
                Stretch = Stretch.Uniform
            };

            var drawing = new GeometryDrawing
            {
                Brush = brush,
                Geometry = new RectangleGeometry(
                    new Rect(0, 0, SymbolSize, SymbolSize))
            };

            return new DrawingImage(drawing);
        }
    }

    public partial class MainWindow : Window
    {
        public PhotoCollection Photos;
        public Photo Photo;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Refresh()
        {
            if (PhotosListBox.SelectedItem == null) return;
            Photo = (Photo)PhotosListBox.SelectedItem;
            Gallery.Title = Photo.Source;
        }

        private void OnPhotoMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Refresh();
        }

        private void Update(string filePath)
        {
            Photos.Path = filePath.Remove(filePath.LastIndexOf('\\') + 1);
            foreach (var p in Photos)
            {
                if (!p.Source.Equals(filePath))
                {
                    continue;
                }

                PhotosListBox.SelectedItem = p; break;
            }
            Refresh();
        }


        private void OpenFile(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Images|*.jpg; *.jpeg; *.png; *.bmp|All files (*.*)|*.*"
            };
            if (openFileDialog.ShowDialog() == true)
            {
                Update(openFileDialog.FileName);
            }
        }
    }
}
