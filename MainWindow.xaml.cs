using System.IO;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace WPFTreeView
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        /// 
        #endregion

        #region On Loaded

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Get every logical drive on the machine
            foreach (var drive in Directory.GetLogicalDrives())
            {
                //Create a new item for it
                var item = new TreeViewItem()
                {
                    //Set the header and full path
                    Header = drive,
                    Tag = drive
                };

                //Add a dummy item
                item.Items.Add(null);

                //Listen out for item being expanded
                item.Expanded += Folder_Expanded;

                //Add it to the main tree-view
                FolderView.Items.Add(item);
            }

        }

        #endregion

        #region Folder Expanded
        private void Folder_Expanded(object sender, RoutedEventArgs e)
        {

            #region Initial Check
            var item = (TreeViewItem)sender;

            //If item doesn't contain only dummy data
            if (item.Items.Count != 1 || item.Items[0] != null)
            { return; }

            //Clear dummy data
            item.Items.Clear();

            #endregion

            #region Get Folders
            var fullPath = (string)item.Tag;

            var directories = new List<string>();

            try
            {
                var dirs = Directory.GetDirectories(fullPath);
                if (dirs.Length > 0)
                {
                    directories.AddRange(dirs);
                }

            }
            catch { }

            directories.ForEach(directoryPath =>
            {
                var subItem = new TreeViewItem()
                {
                    Header = GetFileFolderName(directoryPath),
                    Tag = directoryPath

                };

                //Add dummy item
                subItem.Items.Add(null);

                subItem.Expanded += Folder_Expanded;

                item.Items.Add(subItem);
            });

            #endregion

            #region Get Files
            var files = new List<string>();

            try
            {
                var fs = Directory.GetFiles(fullPath);
                if (fs.Length > 0)
                {
                    files.AddRange(fs);
                }

            }
            catch { }

            files.ForEach(filePath =>
            {
                var subItem = new TreeViewItem()
                {
                    Header = GetFileFolderName(filePath),
                    Tag = filePath

                };

                item.Items.Add(subItem);
            });
            #endregion
        }
        #endregion


        public static string GetFileFolderName(string path)
        {
            if (string.IsNullOrEmpty(path))
                return string.Empty;

            var normalizedPath = path.Replace('/', '\\');
            // Find the last backslash
            var lastIndex = normalizedPath.LastIndexOf('\\');

            //if we don't find a backslash, return the path itself
            if (lastIndex <= 0)
            { return path; }

            //Return name after last backslash
            return path.Substring(lastIndex + 1);
        }
    }
}


