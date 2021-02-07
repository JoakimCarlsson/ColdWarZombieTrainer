using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Media;
using BlueRain;
using ColdWarZombieTrainer.Features;

namespace ColdWarZombieTrainer
{
    class Core
    {
        public GodMode GodMode { get; private set; }
        public SpeedMultiplier SpeedMultiplier { get; private set; }
        public InfiniteAmmo InfiniteAmmo { get; private set; }
        public SpawnMoney MoneyHack { get; private set; }
        public ZombieHack ZombieHack { get; private set; }
        public XpMultiplier XpMultiplier { get; private set; }
        public MiscFeatures MiscFeatures { get; private set; }
        public CamoFeatures CamoFeatures { get; private set; }

        private const string GameTitle = "Call of Duty®: Black Ops Cold War";
        private const string ProcessName = "BlackOpsColdWar";

        private IntPtr _hWnd;
        private IntPtr _baseAddress;
        private NativeMemory _memory;
        private WpfConsole _console;
        private IntPtr _playerPedPtr;
        private IntPtr _zmGlobalBase;
        private IntPtr _zmBotBase;
        private IntPtr _zmBotListBase;

        public Core(WpfConsole console)
        {
            _console = console;
        }

        public bool Start()
        {
            if ((_hWnd = WinAPI.FindWindowByCaption(_hWnd, GameTitle)) == IntPtr.Zero)
                return false;

            Process[] processes = Process.GetProcessesByName(ProcessName);
            bool temp = Attach(processes[0]);

            if (temp)
            {
                GodMode = new GodMode(_baseAddress, _memory);
                SpeedMultiplier = new SpeedMultiplier(_baseAddress, _memory);
                InfiniteAmmo = new InfiniteAmmo(_baseAddress, _memory);
                MoneyHack = new SpawnMoney(_baseAddress, _memory);
                MiscFeatures = new MiscFeatures(_baseAddress, _memory);
                ZombieHack = new ZombieHack(_playerPedPtr, _zmBotListBase, _zmGlobalBase, _memory);
                XpMultiplier = new XpMultiplier(_baseAddress, _memory);
                CamoFeatures = new CamoFeatures(_baseAddress, _zmBotListBase, _zmGlobalBase, _memory);

                return true;
            }

            return false;

        }

        private bool Attach(Process process)
        {
            _memory = new ExternalProcessMemory(process);
            _baseAddress = _memory.GetModule("BlackOpsColdWar.exe").BaseAddress;

            _playerPedPtr = _memory.Read<IntPtr>(_baseAddress + Offsets.PlayerBase + 0x8);
            _zmGlobalBase = _memory.Read<IntPtr>(_baseAddress + Offsets.PlayerBase + 0x60);
            _zmBotBase = _memory.Read<IntPtr>(_baseAddress + Offsets.PlayerBase + 0x68);
            _zmBotListBase = _memory.Read<IntPtr>(_zmBotBase + 0x8);

            if (_playerPedPtr == IntPtr.Zero || _zmGlobalBase == IntPtr.Zero || _zmBotBase == IntPtr.Zero || _zmBotListBase == IntPtr.Zero)
            {
                _console.WriteLine("Make sure you are inside a match before you press start.", Brushes.Red);
                return false;
            }

            _console.WriteLine($"playerPedPtr: {_playerPedPtr}", Brushes.Green);
            _console.WriteLine($"zmGlobalBase: {_zmGlobalBase}", Brushes.Green);
            _console.WriteLine($"zmBotBase: {_zmBotBase}", Brushes.Green);
            _console.WriteLine($"zmBotListBase: {_zmBotListBase}", Brushes.Green);
            _console.WriteLine($"Attached: BaseAddress: {_baseAddress}", Brushes.Green);

            return true;
        }
    }
}
