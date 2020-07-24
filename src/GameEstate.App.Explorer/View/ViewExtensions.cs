using System.Windows.Controls;

namespace GameEstate.Explorer.View
{
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
