using Microsoft.Xna.Framework;
using System.Windows;
using WpfTest.Scenes;

namespace WpfTest.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Opens the window once.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        static void OpenWindow<T>() where T : Window, new()
        {
            var w = new T();
            w.Show();
        }

        static void OpenCustomWindow(WpfGame game, string title)
        {
            var w = new SimpleWindow(game, title);
            w.Show();
        }

        void OpenNewWindow(object sender, RoutedEventArgs e) => OpenCustomWindow(new CubeDemoScene(), "Cube demo scene");

        void OpenTextInputWindow(object sender, RoutedEventArgs e) => OpenWindow<TextInputWindow>();

        void OpenMultipleGameWindow(object sender, RoutedEventArgs e) => OpenWindow<MultiSceneWindow>();

        void OpenTabbedGameWindow(object sender, RoutedEventArgs e)
        {
            // manually reset counters so we always have the same id's per tab
            TabScene.Counter = 0;
            OpenWindow<TabWindow>();
        }

        void OpenRendertargetGameWindow(object sender, RoutedEventArgs e) => OpenCustomWindow(new RenderTargetScene(), "Rendertarget scene");

        void OpenCloseableTabWindow(object sender, RoutedEventArgs e)
        {
            // manually reset counters so we always have the same id's per tab
            TabScene.Counter = 0;
            OpenWindow<CloseableTabWindow>();
        }

        void OpenModelWindow(object sender, RoutedEventArgs e) => OpenCustomWindow(new ModelScene(), "Model scene");

        void OpenMsaaWindow(object sender, RoutedEventArgs e) => OpenCustomWindow(new MsaaScene(), "MSAA scene");

        void OpenMultipleMsaaWindow(object sender, RoutedEventArgs e) => OpenWindow<MultiMsaaWindow>();

        void OpenDpiScalingWindow(object sender, RoutedEventArgs e) => OpenWindow<DpiScalingWindow>();

        void OpenSpriteWindow(object sender, RoutedEventArgs e) => OpenCustomWindow(new SpriteScene(), "Sprite scene");
    }
}
