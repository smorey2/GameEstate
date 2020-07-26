using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using GameEstate.Explorer.ViewModel;

namespace GameEstate.Explorer.View
{
    /// <summary>
    /// Interaction logic for FileType.xaml
    /// </summary>
    public partial class FileExplorer : UserControl, INotifyPropertyChanged
    {
        public static FileExplorer Instance;
        public static MainWindow MainWindow => MainWindow.Instance;
        //public static FileInfo FileInfo => FileInfo.Instance;
        public static EngineView EngineView => EngineView.Instance;

        //public static WorldViewer WorldViewer => WorldViewer.Instance; 
        //public static ModelViewer ModelViewer => ModelViewer.Instance;  
        //public static TextureViewer TextureViewer => TextureViewer.Instance; 

        public static List<ExplorerItemNode.Filter> TreeFilters { get; set; }

        public static List<ExplorerItemNode> FilteredNodes { get; set; }

        public FileExplorer()
        {
            InitializeComponent();
            Instance = this;
            DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        void NotifyPropertyChanged([CallerMemberName] string propertyName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        List<ExplorerItemNode> _nodes;
        public List<ExplorerItemNode> Nodes
        {
            get => _nodes;
            set { _nodes = value; NotifyPropertyChanged(nameof(Nodes)); }
        }

        void TreeFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        void FilteredNode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        ExplorerItemNode SelectedItem;

        void Node_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (e.NewValue is TreeViewItem item && item.Items.Count > 0)
                (item.Items[0] as TreeViewItem).IsSelected = true;
            if (e.NewValue is ExplorerItemNode itemNode && (itemNode.PakFile != null || itemNode.Pak2File != null) && SelectedItem != itemNode)
                SelectedItem = itemNode;
            if (SelectedItem == null)
                return;

            FileInfo.Info = SelectedItem.PakFile != null
                ? SelectedItem.PakFile.GetExplorerInfoNodesAsync(MainWindow.Resource, SelectedItem).Result
                : SelectedItem.Pak2File != null
                ? SelectedItem.Pak2File.GetExplorerInfoNodesAsync(MainWindow.Resource, SelectedItem).Result
                : null;
        }
    }
}
