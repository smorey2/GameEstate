using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
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
using GameEstate.Explorer.ViewModel;

namespace GameEstate.Explorer.View
{
    /// <summary>
    /// Interaction logic for FileContent.xaml
    /// </summary>
    public partial class FileContent : UserControl, INotifyPropertyChanged
    {
        public static FileContent Instance;

        public FileContent()
        {
            InitializeComponent();
            Instance = this;
            DataContext = this;
            ContentTabs = new[] {
                new ExplorerContentTab
                {
                    Name = "Render Information",
                    EngineType = typeof(string),
                },
                new ExplorerContentTab
                {
                    Name = "REDI",
                    Text = @"Leverage agile frameworks to provide a robust synopsis for high level overviews. Iterative approaches to corporate strategy foster collaborative thinking to further the overall value proposition. Organically grow the holistic world view of disruptive innovation via workplace diversity and empowerment.",
                },
                new ExplorerContentTab
                {
                    Name = "Text #2",
                    Text = @"Leverage agile frameworks to provide a robust synopsis for high level overviews. Iterative approaches to corporate strategy foster collaborative thinking to further the overall value proposition. Organically grow the holistic world view of disruptive innovation via workplace diversity and empowerment.",
                }
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void NotifyPropertyChanged([CallerMemberName] string propertyName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        IList<ExplorerContentTab> _contentTabs;
        public IList<ExplorerContentTab> ContentTabs
        {
            get => _contentTabs;
            set { _contentTabs = value; NotifyPropertyChanged(); }
        }
    }
}
