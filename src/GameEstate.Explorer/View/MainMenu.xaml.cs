using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using GameEstate.Core;
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

        public void OnFirstLoad() => OpenFile_Click(null, null);

        public MultiPakFile PakFile { get; private set; }

        public CoreDatFile DatFile { get; private set; }

        void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            var openDialog = new OpenDialog();
            if (openDialog.ShowDialog() == true)
            {
                PakFile?.Dispose();
                PakFile = null;
                DatFile?.Dispose();
                DatFile = null;
                //
                var estate = (Estate)openDialog.Estate.SelectedItem;
                if (estate == null) return;
                if (openDialog.PakUri != null)
                {
                    MainWindow.Status.WriteLine($"Opening {openDialog.PakUri}");
                    PakFile = estate.OpenPakFile(openDialog.PakUri);
                }
                if (openDialog.DatUri != null)
                {
                    MainWindow.Status.WriteLine($"Opening {openDialog.DatUri}");
                    DatFile = estate.OpenDatFile(openDialog.DatUri);
                }
                MainWindow.Status.WriteLine("Done");
                MainWindow.OnOpened();
            }
        }

        void Options_Click(object sender, RoutedEventArgs e)
        {
            var options = new Options();
            options.ShowDialog();
        }

        void WorldMap_Click(object sender, RoutedEventArgs e)
        {
            //if (DatManager.CellDat == null || DatManager.PortalDat == null)
            //    return;

            //EngineView.ViewMode = ViewMode.Map;
        }

        void About_Click(object sender, RoutedEventArgs e)
        {
            var about = new About();
            about.ShowDialog();
        }

        void Guide_Click(object sender, RoutedEventArgs e)
        {
            //Process.Start(@"docs\index.html");
        }
    }
}
