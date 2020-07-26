using GameEstate.Core;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;

// https://www.wpf-tutorial.com/panels/grid-usage-example-contact-form/
namespace GameEstate.Explorer.View
{
    /// <summary>
    /// Interaction logic for OpenDialog.xaml
    /// </summary>
    public partial class OpenDialog : Window, INotifyPropertyChanged
    {
        public OpenDialog()
        {
            InitializeComponent();
            DataContext = this;
            //Estate.Items
            if (!string.IsNullOrEmpty(EstateManager.DefaultEstateKey))
                Estate.SelectedIndex = EstateManager.Estates.Keys.ToList().IndexOf(EstateManager.DefaultEstateKey);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));

        public ICollection<Estate> Estates { get; } = EstateManager.Estates.Values; //.Where(x => x.FileManager.FoundGames).ToList();

        ICollection<Estate.EstateGame> _estateGames;
        public ICollection<Estate.EstateGame> EstateGames
        {
            get => _estateGames;
            set { _estateGames = value; NotifyPropertyChanged(nameof(EstateGames)); }
        }

        Uri _pakUri;
        public Uri PakUri
        {
            get => _pakUri;
            set { _pakUri = value; NotifyPropertyChanged(nameof(PakUri)); }
        }

        Uri _pak2Uri;
        public Uri Pak2Uri
        {
            get => _pak2Uri;
            set { _pak2Uri = value; NotifyPropertyChanged(nameof(Pak2Uri)); }
        }

        void Estate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = (Estate)Estate.SelectedItem;
            EstateGames = selected?.Games.Values;
            EstateGame.SelectedIndex = 0;
        }

        void EstateGame_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = (Estate.EstateGame)EstateGame.SelectedItem;
            PakUri = selected?.DefaultPak;
            Pak2Uri = selected?.DefaultPak2;
        }

        void PakUriFile_Click(object sender, RoutedEventArgs e)
        {
            var openDialog = new OpenFileDialog
            {
                Filter = "PAK files|*.*"
            };
            if (openDialog.ShowDialog() == true)
            {
                var files = openDialog.FileNames;
                if (files.Length < 1) return;
                var file = files[0];
                var selected = (Estate.EstateGame)EstateGame.SelectedItem;
                PakUri = new UriBuilder(file)
                {
                    Fragment = selected?.Game ?? "Unknown"
                }.Uri;
            }
        }

        void Pak2UriFile_Click(object sender, RoutedEventArgs e)
        {
            var openDialog = new OpenFileDialog
            {
                Filter = "PAK files|*.*"
            };
            if (openDialog.ShowDialog() == true)
            {
                var files = openDialog.FileNames;
                if (files.Length < 1) return;
                var file = files[0];
                var selected = (Estate.EstateGame)EstateGame.SelectedItem;
                Pak2Uri = new UriBuilder(file)
                {
                    Fragment = selected?.Game ?? "Unknown"
                }.Uri;
            }
        }

        void Cancel_Click(object sender, RoutedEventArgs e) => Close();

        void Open_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}
