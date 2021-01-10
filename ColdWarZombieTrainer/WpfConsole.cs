using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace ColdWarZombieTrainer
{
    class WpfConsole
    {
        private TextBox Console;
        public WpfConsole(TextBox consoleTextBox)
        {
            Console = consoleTextBox;
        }

        public void WriteLine(string text)
        {
            Console.Text += $"\n[{DateTime.Now:T}] {text}";
            Console.ScrollToEnd();
        }

        public void Clear()
        {
            Console.Clear();
        }
    }
}
