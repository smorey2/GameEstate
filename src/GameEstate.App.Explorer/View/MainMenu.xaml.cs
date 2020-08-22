using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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

        public MainMenu()
        {
            InitializeComponent();
        }

        public void OnFirstLoad() => OpenFile_Click(null, null);

        public IList<EstatePakFile> PakFiles { get; private set; } = new List<EstatePakFile>();

        void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            var openDialog = new OpenDialog();
            if (openDialog.ShowDialog() == true)
            {
                foreach (var pakFile in PakFiles)
                    pakFile?.Dispose();
                PakFiles.Clear();
                //
                var estate = (Estate)openDialog.Estate.SelectedItem;
                if (estate == null) return;
                foreach (var pakUri in openDialog.PakUris)
                {
                    MainWindow.Status.WriteLine($"Opening {pakUri}");
                    PakFiles.Add(estate.OpenPakFile(pakUri));
                }
                MainWindow.Status.WriteLine("Done");
                MainWindow.OnOpenedAsync().Wait();
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
