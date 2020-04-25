using Microsoft.Xna.Framework;

namespace WpfTest.Views
{
    /// <summary>
    /// Interaction logic for SimpleWindow.xaml
    /// </summary>
    public partial class SimpleWindow
    {
        public SimpleWindow(WpfGame scene, string title)
        {
            InitializeComponent();

            Title = title;
            RootGrid.Children.Add(scene);
        }
    }
}