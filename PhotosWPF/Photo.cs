using System;
using System.Windows.Media.Imaging;

namespace PhotosWPF
{

    public class Photo
    {
        private readonly Uri _source;

        public Photo(string path)
        {
            Source = path;
            _source = new Uri(path);
            Image = BitmapFrame.Create(_source);
        }

        public override string ToString()
        {
            return _source.ToString();
        }

        public string Source { get; }

        public BitmapFrame Image { get; }
    }

}