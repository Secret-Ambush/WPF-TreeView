using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace WPFTreeView
{

    ///<summary>
    /// Converts a full path to a specific image type of a drive or a folder
    ///</summary>
    ///
    [ValueConversion(typeof(string), typeof(BitmapImage))]
    public class HeaderToImageConverter : IValueConverter
    {
        public static HeaderToImageConverter Instance = new HeaderToImageConverter();
        
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var path = (String)value;

            //If path is null, ignore
            if (path == null) { return null; }

            var image = "Images/file.png";

            //Get the name of the file/folder
            var name = MainWindow.GetFileFolderName(path);

            //If the name is blank, then it will be a drive
            if (string.IsNullOrEmpty(name))
                image = "Images/harddrive.png";
            else if (new FileInfo(path).Attributes.HasFlag(FileAttributes.Directory))
                image = "Images/folder.png";

            return new BitmapImage(new Uri($"pack://application:,,,/{image}"));

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
