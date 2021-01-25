using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace ColdWarZombieTrainer
{
    class WpfConsole
    {
        private TextBox Console;
        public WpfConsole(TextBox consoleTextBox)
        {
            Console = consoleTextBox;
        }

        public void WriteLine(string text, SolidColorBrush color)
        {
            Console.Text += $"\n[{DateTime.Now:T}] {text}";
            Console.Foreground = color;
            Console.ScrollToEnd();
        }

        public void Clear()
        {
            Console.Clear();
        }
    }
}
