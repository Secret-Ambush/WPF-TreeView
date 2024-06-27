using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
        #region On Loaded

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (var drive in Directory.GetLogicalDrives())
            {
                var item = new TreeViewItem();
                item.Header = drive;
                FolderView.Items.Add(item);
            }

        }
        #endregion
        #endregion
    }
}