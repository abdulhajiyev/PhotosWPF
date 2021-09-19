using System.Collections.ObjectModel;
using System.IO;

namespace PhotosWPF
{

    public class PhotoCollection : ObservableCollection<Photo>
    {
        DirectoryInfo _directory;

        public string Path
        {
            set
            {
                _directory = new DirectoryInfo(value);
                Update();
            }
        }

        private void Update()
        {
            Clear();
            var fs = _directory.GetFiles("*.*");
            for (var index = 0; index < fs.Length; index++)
            {
                var f = fs[index];
                if (f.FullName.EndsWith(".jpg") || f.FullName.EndsWith(".png") || f.FullName.EndsWith(".bmp"))
                {
                    Add(new Photo(f.FullName));
                }
            }
        }

    }

}