using System;
using System.Diagnostics;
using System.Threading;
using ColdWarZombieTrainer.Features;
using Memory;

namespace ColdWarZombieTrainer
{
    class Core
    {
        private const string GameTitle = "Call of Duty®: Black Ops Cold War";
        private const string ProcessName = "BlackOpsColdWar";
        private IntPtr _hWnd;
        private IntPtr _baseAddress;
        private NativeMemory _memory;
        private WpfConsole _console;

        internal GodMode godMode;
        internal SpeedMultiplier speedMultiplier;
        internal InfiniteAmmo infiniteAmmo;
        internal SpawnMoney moneyHack;
        internal ZombieHack zombieHack;
        internal XpMultiplier xpMultiplier;

        internal IntPtr playerPedPtr;
        internal IntPtr zmGlobalBase;
        internal IntPtr zmBotBase;
        internal IntPtr zmBotListBase;


        public Core(WpfConsole console)
        {
            _console = console;
        }

        public void Start()
        {
            _console.WriteLine($"> Waiting for {GameTitle} to start up...");

            while ((_hWnd = WinAPI.FindWindowByCaption(_hWnd, GameTitle)) == IntPtr.Zero)
            {
                Thread.Sleep(250);
            }

            Process[] processes = Process.GetProcessesByName(ProcessName);
            Attach(processes[0]);

            godMode = new GodMode(_baseAddress, _memory);
            speedMultiplier = new SpeedMultiplier(_baseAddress, _memory);
            infiniteAmmo = new InfiniteAmmo(_baseAddress, _memory);
            moneyHack = new SpawnMoney(_baseAddress, _memory);
            zombieHack = new ZombieHack(playerPedPtr,zmBotListBase, zmGlobalBase, _memory);
            xpMultiplier = new XpMultiplier();
        }

        private void Attach(Process process)
        {
            _memory = new ExternalProcessMemory(process);
            _baseAddress = _memory.GetModule("BlackOpsColdWar.exe").BaseAddress;

            playerPedPtr = _memory.Read<IntPtr>(_baseAddress + Offsets.PlayerBase + 0x8);
            zmGlobalBase = _memory.Read<IntPtr>(_baseAddress + Offsets.PlayerBase + 0x60);
            zmBotBase = _memory.Read<IntPtr>(_baseAddress + Offsets.PlayerBase + 0x68);
            zmBotListBase = _memory.Read<IntPtr>(zmBotBase + 0x8);


            _console.WriteLine($"playerPedPtr: {playerPedPtr}");
            _console.WriteLine($"zmGlobalBase: {zmGlobalBase}");
            _console.WriteLine($"zmBotBase: {zmBotBase}");
            _console.WriteLine($"zmBotListBase: {zmBotListBase}");

            _console.WriteLine($"Attached: BaseAddress: {_baseAddress}");
        }
    }
}
