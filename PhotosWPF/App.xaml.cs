using System.Windows;
using System.Windows.Data;

namespace PhotosWPF
{
    public partial class App : Application
    {
        void OnApplicationStartup(object sender, StartupEventArgs args)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            mainWindow.Photos = (PhotoCollection)(this.Resources["Photos"] as ObjectDataProvider).Data;
        }
    }
}
