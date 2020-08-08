using GameEstate.Core;
using Microsoft.Win32;
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
            if (!string.IsNullOrEmpty(EstateManager.DefaultEstateKey))
                Estate.SelectedIndex = EstateManager.Estates.Keys.ToList().IndexOf(EstateManager.DefaultEstateKey);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void NotifyPropertyChanged([CallerMemberName] string propertyName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public ICollection<Estate> Estates { get; } = EstateManager.Estates.Values; //.Where(x => x.FileManager.FoundGames).ToList();

        public IList<Uri> PakUris
        {
            get => new[] { _pakUri, _pak2Uri, _pak3Uri }.Where(x => x != null).ToList();
            set
            {
                var idx = 0;
                _pakUri = _pak2Uri = _pak3Uri = null;
                if (value != null)
                    foreach (var uri in value)
                    {
                        if (uri == null)
                            continue;
                        switch (++idx)
                        {
                            case 1: _pakUri = uri; break;
                            case 2: _pak2Uri = uri; break;
                            case 3: _pak3Uri = uri; break;
                        }
                    }
            }
        }

        ICollection<Estate.EstateGame> _estateGames;
        public ICollection<Estate.EstateGame> EstateGames
        {
            get => _estateGames;
            set { _estateGames = value; NotifyPropertyChanged(); }
        }

        Uri _pakUri;
        public Uri PakUri
        {
            get => _pakUri;
            set { _pakUri = value; NotifyPropertyChanged(); }
        }

        Uri _pak2Uri;
        public Uri Pak2Uri
        {
            get => _pak2Uri;
            set { _pak2Uri = value; NotifyPropertyChanged(); }
        }

        Uri _pak3Uri;
        public Uri Pak3Uri
        {
            get => _pak3Uri;
            set { _pak3Uri = value; NotifyPropertyChanged(); }
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
            PakUris = selected?.DefaultPaks;
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

        void Pak3UriFile_Click(object sender, RoutedEventArgs e)
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
                Pak3Uri = new UriBuilder(file)
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
