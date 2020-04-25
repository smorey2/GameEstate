using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;

namespace GameEstate.Explorer.View
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : UserControl
    {
        public static MainWindow MainWindow => MainWindow.Instance;

        //public static ParticleExplorer Particle => ParticleExplorer.Instance;

        //public static GameView GameView { get => GameView.Instance; }

        public MainMenu()
        {
            InitializeComponent();
        }

        void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            //var openFileDialog = new OpenFileDialog();
            //openFileDialog.Filter = "DAT files (*.dat)|*.dat|All files (*.*)|*.*";
            //if (openFileDialog.ShowDialog() == true)
            //{
            //    var files = openFileDialog.FileNames;
            //    if (files.Length < 1) return;
            //    var file = files[0];

            //    MainWindow.Status.WriteLine("Reading " + file);

            //    await Task.Run(() => ReadDATFile(file));

            //    //Particle.ReadFiles();

            //    //var cellFiles = DatManager.CellDat.AllFiles.Count;
            //    //var portalFiles = DatManager.PortalDat.AllFiles.Count;

            //    //MainWindow.Status.WriteLine($"CellFiles={cellFiles}, PortalFiles={portalFiles}");

            //    MainWindow.Status.WriteLine("Done");

            //    GameView.PostInit();
            //}
        }

        //public void ReadDATFile(string filename)
        //{
        //    var fi = new System.IO.FileInfo(filename);
        //    var di = fi.Directory;

        //    var loadCell = true;
        //    DatManager.Initialize(di.FullName, true, loadCell);
        //}

        void Options_Click(object sender, RoutedEventArgs e)
        {
            var options = new Options();
            options.ShowDialog();
        }

        void WorldMap_Click(object sender, RoutedEventArgs e)
        {
            //if (DatManager.CellDat == null || DatManager.PortalDat == null)
            //    return;

            //GameView.ViewMode = ViewMode.Map;
        }

        void About_Click(object sender, RoutedEventArgs e)
        {
            var about = new About();
            about.ShowDialog();
        }

        void Guide_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(@"docs\index.html");
        }
    }
}
