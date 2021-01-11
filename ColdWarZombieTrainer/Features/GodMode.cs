using System;
using Memory;

namespace ColdWarZombieTrainer.Features
{
    class GodMode
    {
        private IntPtr _baseAddress;
        private NativeMemory _memory;

        public GodMode(IntPtr baseAddress, NativeMemory memory)
        {
            _baseAddress = baseAddress;
            _memory = memory;
        }

        public void EnableGodMode()
        {
            _memory.Write<byte>(false, 0xA0, _baseAddress + Offsets.PlayerBase, (IntPtr)Offsets.GodMode);
        }

        public void DisableGodMode()
        {
            _memory.Write<byte>(false, 0x20, _baseAddress + Offsets.PlayerBase, (IntPtr)Offsets.GodMode);
        }
    }
}
