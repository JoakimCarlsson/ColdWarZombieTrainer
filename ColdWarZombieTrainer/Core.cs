using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
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
        }

        private void Attach(Process process)
        {
            _memory = new ExternalProcessMemory(process);
            _baseAddress = _memory.GetModule("BlackOpsColdWar.exe").BaseAddress;
            _console.WriteLine($"Attached: BaseAddress: {_baseAddress}");
        }
    }
}
