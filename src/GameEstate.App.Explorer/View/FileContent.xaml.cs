﻿using System;
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

// https://stackoverflow.com/questions/2783378/wpf-byte-array-to-hex-view-similar-to-notepad-hex-editor-plugin
namespace GameEstate.Explorer.View
{
    /// <summary>
    /// Interaction logic for FileContent.xaml
    /// </summary>
    public partial class FileContent : UserControl, INotifyPropertyChanged
    {
        public static FileContent Instance;
        public static MainWindow MainWindow => MainWindow.Instance;

        public FileContent()
        {
            InitializeComponent();
            Instance = this;
            DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void NotifyPropertyChanged([CallerMemberName] string propertyName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        IEstateGraphic _graphic;
        public IEstateGraphic Graphic
        {
            get => _graphic;
            set { _graphic = value; NotifyPropertyChanged(); }
        }

        IList<ExplorerContentTab> _contentTabs;
        public IList<ExplorerContentTab> ContentTabs
        {
            get => _contentTabs;
            set { _contentTabs = value; NotifyPropertyChanged(); }
        }

        public void OnFileInfo(EstatePakFile pakFile, List<ExplorerInfoNode> infos)
        {
            if (ContentTabs != null)
                foreach (var dispose in ContentTabs.Where(x => x.Dispose != null).Select(x => x.Dispose))
                    dispose.Dispose();

            Graphic = pakFile.Graphic;
            ContentTabs = infos?.Select(x => x.Tag as ExplorerContentTab).Where(x => x != null).ToList();
            ContentTab.SelectedIndex = ContentTabs != null ? 0 : -1;
        }
    }
}
