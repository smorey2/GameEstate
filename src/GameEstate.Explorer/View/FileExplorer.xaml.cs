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
        public static FileInfo FileInfo => FileInfo.Instance;
        public static EngineView EngineView => EngineView.Instance; 

        //public static WorldViewer WorldViewer => WorldViewer.Instance; 
        //public static ModelViewer ModelViewer => ModelViewer.Instance;  
        //public static TextureViewer TextureViewer => TextureViewer.Instance; 

        public static List<TreeNode.Filter> TreeFilters { get; set; }

        public FileExplorer()
        {
            InitializeComponent();
            Instance = this;
            DataContext = this;
            var sub = new List<TreeNode> {
                new TreeNode { Name = "TEST" },
                new TreeNode { Name = "TEST 1" },
                new TreeNode { Name = "TEST 2" },
            };
            Nodes = new List<TreeNode> { 
                new TreeNode { Name = "TEST", Items = sub },
                new TreeNode { Name = "TEST 1" },
                new TreeNode { Name = "TEST 2" },
            };

            FileInfo.SetInfo(sub[0]);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        void NotifyPropertyChanged([CallerMemberName] string propertyName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        List<TreeNode> _nodes;
        public List<TreeNode> Nodes
        {
            get => _nodes;
            set
            {
                _nodes = value;
                NotifyPropertyChanged(nameof(Nodes));
            }
        }

        void TreeFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        void Node_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            //if (e.NewValue is TreeViewItem item && item.Items.Count > 0)
            //    (item.Items[0] as TreeViewItem).IsSelected = true;

            //if (e.NewValue is Item)
            //{
            //    Item item = e.NewValue as Item;
            //    if (Item != SelectedItem)
            //    {
            //        //keep SelectedItem in sync with Treeview.SelectedItem
            //        SelectedItem = e.NewValue as Item;
            //    }
            //}
            //else
            //{
            //    //if the user tries to select an object that isn't an Item (i.e. a group) reselect the first Item in that group
            //    //This will then cause stack overflow in methods I've tried so far
            //}
        }
    }
}
