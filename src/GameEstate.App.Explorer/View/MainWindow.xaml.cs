using GameEstate.Core;
using GameEstate.Explorer.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// ExplorerTab
    /// </summary>
    public class ExplorerMainTab
    {
        public string Name { get; set; }
        public EstatePakFile PakFile { get; set; }
        public string Text { get; set; }
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public static MainWindow Instance;

        public MainWindow()
        {
            InitializeComponent();
            Instance = this;
            DataContext = this;
            MainMenu.OnFirstLoad(); // opens OpenFile dialog
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void NotifyPropertyChanged([CallerMemberName] string propertyName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        IList<ExplorerMainTab> _mainTabs;
        public IList<ExplorerMainTab> MainTabs
        {
            get => _mainTabs;
            set { _mainTabs = value; NotifyPropertyChanged(); }
        }

        public Task OnOpenedAsync()
        {
            MainTabs = MainMenu.PakFiles.Select(pakFile => new ExplorerMainTab
            {
                Name = pakFile.Name,
                PakFile = pakFile,
                Text = "Example",
            }).ToList();
            MainTabs.Add(new ExplorerMainTab
            {
                Name = "Information",
                Text = @"Leverage agile frameworks to provide a robust synopsis for high level overviews. Iterative approaches to corporate strategy foster collaborative thinking to further the overall value proposition. Organically grow the holistic world view of disruptive innovation via workplace diversity and empowerment.",
            });
            return Task.CompletedTask;
        }
    }
}
