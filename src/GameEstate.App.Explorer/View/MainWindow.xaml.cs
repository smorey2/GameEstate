using GameEstate.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GameEstate.Explorer.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static ExplorerManager Resource = new ResourceManagerProvider();
        public static MainWindow Instance;
        //public EngineView EngineView => EngineView.Instance;

        public MainWindow()
        {
            InitializeComponent();
            Instance = this;
            MainMenu.OnFirstLoad();
        }

        public async Task OnOpenedAsync()
        {
            PakFileExplorer.Nodes = MainMenu.PakFile != null ? await MainMenu.PakFile.GetExplorerItemNodesAsync(Resource) : null;
            DatFileExplorer.Nodes = MainMenu.Pak2File != null ? await MainMenu.Pak2File.GetExplorerItemNodesAsync(Resource) : null;
        }
    }
}
