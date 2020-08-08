using System.Windows;
using System.Windows.Controls;

namespace GameEstate.Explorer.View
{
    public class BindingProxy_ : Freezable
    {
        protected override Freezable CreateInstanceCore() => new BindingProxy_();

        public static readonly DependencyProperty DataProperty = DependencyProperty.Register(nameof(Data), typeof(object), typeof(BindingProxy_), new UIPropertyMetadata(null));
        public object Data
        {
            get => GetValue(DataProperty);
            set => SetValue(DataProperty, value);
        }
    }

    public static class ViewExtensions
    {
        public static void WriteLine(this TextBox textBox, string line)
        {
            if (textBox.Text.Length != 0)
                textBox.AppendText("\n");
            textBox.AppendText(line);
            textBox.ScrollToEnd();
        }
    }
}
