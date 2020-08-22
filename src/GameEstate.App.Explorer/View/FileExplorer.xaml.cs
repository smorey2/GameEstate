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
using GameEstate.Core;
using System.Threading.Tasks;
using System.Windows.Data;

namespace GameEstate.Explorer.View
{
    /// <summary>
    /// Interaction logic for FileType.xaml
    /// </summary>
    public partial class FileExplorer : UserControl, INotifyPropertyChanged
    {
        public static ExplorerManager Resource = new ResourceManagerProvider();
        public static FileExplorer Instance;
        //public static MainWindow MainWindow => MainWindow.Instance;
        public static FileContent FileContent => FileContent.Instance;

        public FileExplorer()
        {
            InitializeComponent();
            Instance = this;
            DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void NotifyPropertyChanged([CallerMemberName] string propertyName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public static readonly DependencyProperty PakFileProperty = DependencyProperty.Register(nameof(PakFile), typeof(AbstractPakFile), typeof(FileExplorer),
            new PropertyMetadata((d, e) =>
            {
                if (!(d is FileExplorer fileExplorer) || !(e.NewValue is AbstractPakFile pakFile))
                    return;
                fileExplorer.NodeFilters = pakFile.GetExplorerItemFiltersAsync(Resource).Result;
                fileExplorer.Nodes = fileExplorer.PakNodes = pakFile.GetExplorerItemNodesAsync(Resource).Result;
                fileExplorer.OnFileInfo(null);
            }));

        public AbstractPakFile PakFile
        {
            get => GetValue(PakFileProperty) as AbstractPakFile;
            set => SetValue(PakFileProperty, value);
        }

        List<ExplorerItemNode.Filter> _nodeFilters;
        public List<ExplorerItemNode.Filter> NodeFilters
        {
            get => _nodeFilters;
            set { _nodeFilters = value; NotifyPropertyChanged(); }
        }

        void NodeFilter_KeyUp(object sender, KeyEventArgs e)
        {
            if (string.IsNullOrEmpty(NodeFilter.Text)) Nodes = PakNodes;
            else Nodes = PakNodes.Select(x => x.Search(y => y.Name.Contains(NodeFilter.Text))).ToList();
            //var view = (CollectionView)CollectionViewSource.GetDefaultView(Node.ItemsSource);
            //view.Filter = o =>
            //{
            //    if (string.IsNullOrEmpty(NodeFilter.Text)) return true;
            //    else return (o as ExplorerItemNode).Name.Contains(NodeFilter.Text);
            //};
            //view.Refresh();
        }

        void NodeFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count <= 0)
                return;
            var filter = e.AddedItems[0] as ExplorerItemNode.Filter;
            if (string.IsNullOrEmpty(NodeFilter.Text)) Nodes = PakNodes;
            else Nodes = PakNodes.Select(x => x.Search(y => y.Name.Contains(filter.Description))).ToList();
        }

        List<ExplorerItemNode> PakNodes;

        List<ExplorerItemNode> _nodes;
        public List<ExplorerItemNode> Nodes
        {
            get => _nodes;
            set { _nodes = value; NotifyPropertyChanged(); }
        }

        public void OnFileInfo(List<ExplorerInfoNode> infos)
        {
            FileContent.OnFileInfo(infos?.Where(x => x.Name == null).ToList());
            FileInfo.Infos = infos?.Where(x => x.Name != null).ToList();
        }

        ExplorerItemNode SelectedItem;

        void Node_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (e.NewValue is TreeViewItem item && item.Items.Count > 0)
                (item.Items[0] as TreeViewItem).IsSelected = true;
            if (e.NewValue is ExplorerItemNode itemNode && (itemNode.PakFile != null) && SelectedItem != itemNode)
                SelectedItem = itemNode;
            if (SelectedItem != null)
                OnFileInfo(SelectedItem.PakFile?.GetExplorerInfoNodesAsync(Resource, SelectedItem).Result);
        }
    }
}
