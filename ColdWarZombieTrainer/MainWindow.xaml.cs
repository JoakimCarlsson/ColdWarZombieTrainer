using System.ComponentModel;
using System.Threading;
using System.Windows;

namespace ColdWarZombieTrainer
{
    public partial class MainWindow : Window
    {
        private WpfConsole _console;
        private bool _started = false;
        private Core _core;

        private readonly BackgroundWorker _backgroundWorker = new BackgroundWorker();

        private bool _infiniteAmmo = false;
        private bool _infiniteMoney = false;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            if (!_started)
            {
                _started = !_started;
                _core = new Core(_console);
                _core.Start();
            }
        }

        private void TestButton_Click(object sender, RoutedEventArgs e)
        {
            _console.WriteLine("I pressed a test button, woho.");
        }

        private void GodModeEnable(object sender, RoutedEventArgs e)
        {
            if (_started)
            {
                _console.WriteLine("God Mode Enabled");
                _core.godMode.EnableGodMode();
            }
        }

        private void GodModeDisable(object sender, RoutedEventArgs e)
        {
            if (_started)
            {
                _console.WriteLine("God Mode Disabled");
                _core.godMode.DisableGodMode();
            }
        }

        private void SpeedHackEnabled(object sender, RoutedEventArgs e)
        {
            if (_started)
            {
                _console.WriteLine("Speed Hack Enabled");
                _core.speedHack.SetSpeed((float)SpeedHackValueSlider.Value);
            }
        }

        private void SpeedHackDisable(object sender, RoutedEventArgs e)
        {
            if (_started)
            {
                _console.WriteLine("Speed Hack Disabled");
                _core.speedHack.SetSpeed(1f);
            }
        }

        private void InfiniteAmmoEnable(object sender, RoutedEventArgs e)
        {
            if (_started)
            {
                _console.WriteLine("Infinite Ammo Enabled");
                _infiniteAmmo = true;
            }

        }

        private void BackgroundWorkerDoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                Thread.Sleep(100);

                if (!_started)
                    continue;

                if (_infiniteAmmo)
                    _core.infiniteAmmo.DoInfiniteAmmo();

                if (_infiniteMoney)
                    _core.moneyHack.InfiniteMoney();
            }
        }

        private void InfiniteAmmoDisable(object sender, RoutedEventArgs e)
        {
            if (_started)
            {
                _console.WriteLine("Infinite Ammo Disabled");
                _infiniteAmmo = false;
            }

        }

        private void InfiniteMoneyHack(object sender, RoutedEventArgs e)
        {
            if (_started)
            {
                _console.WriteLine("Infinite Money Enabled");
                _infiniteMoney = true;
            }
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            _console = new WpfConsole(Console);
            _backgroundWorker.DoWork += BackgroundWorkerDoWork;
            _backgroundWorker.RunWorkerAsync();
        }

        private void InfiniteMoneyDisable(object sender, RoutedEventArgs e)
        {
            if (_started)
            {
                _console.WriteLine("Infinite Money Disabled");
                _infiniteMoney = false;
            }
        }
    }
}
