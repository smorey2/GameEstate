using System.Diagnostics;
using System.Windows;
using System.Windows.Documents;

namespace WpfTest
{
    /// <summary>
    /// Extension that allows launching a hyperlink via click without having to create a custom event handler for every single hyperlink element.
    /// From: http://stackoverflow.com/a/11433814
    /// </summary>
    public static class HyperlinkExtensions
    {
        public static bool GetLaunchInBrowser(DependencyObject obj) => (bool)obj.GetValue(LaunchInBrowserProperty);

        public static void SetLaunchInBrowser(DependencyObject obj, bool value) => obj.SetValue(LaunchInBrowserProperty, value);

        public static readonly DependencyProperty LaunchInBrowserProperty = DependencyProperty.RegisterAttached("LaunchInBrowser", typeof(bool), typeof(HyperlinkExtensions), new UIPropertyMetadata(false, OnIsChanged));

        static void OnIsChanged(object sender, DependencyPropertyChangedEventArgs args)
        {
            if (!(sender is Hyperlink hyperlink))
                return;
            if ((bool)args.NewValue) hyperlink.RequestNavigate += Hyperlink_RequestNavigate;
            else hyperlink.RequestNavigate -= Hyperlink_RequestNavigate;
        }

        static void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }
    }
}