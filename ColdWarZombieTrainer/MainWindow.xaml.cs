using System.ComponentModel;
using System.Threading;
using System.Windows;
using ColdWarZombieTrainer.Utils;

namespace ColdWarZombieTrainer
{
    public partial class MainWindow : Window
    {
        private WpfConsole _console;
        private bool _started = false;
        private Core _core;
        private KeyUtils _keyUtils;

        private readonly BackgroundWorker _worker = new BackgroundWorker();


        public MainWindow()
        {
            InitializeComponent();
            _console = new WpfConsole(Console);
            _keyUtils = new KeyUtils();
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
            _console.WriteLine("Infinite Ammo Enabled");
            _worker.DoWork += worker_DoWork;
            _worker.WorkerSupportsCancellation = true;
            _worker.RunWorkerAsync();
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (!_started)
                return;

            while (true)
            {
                if (_worker.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }

                _core.infiniteAmmo.DoInfiniteAmmo();
                Thread.Sleep(10);
            }
        }

        private void InfiniteAmmoDisable(object sender, RoutedEventArgs e)
        {
            _console.WriteLine("Infinite Ammo Disabled");
            _worker.DoWork -= worker_DoWork;
            _worker.CancelAsync();
        }

        private void InfiniteMoneyHack(object sender, RoutedEventArgs e)
        {
            if (_started)
            {
                _core.moneyHack.InfiniteMoney();
            }
        }
    }
}
