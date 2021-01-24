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
            //todo check me.

            for (int i = 0; i < 4; i++)
            {
                _memory.Write<byte>(false, 0xA0, _baseAddress + Offsets.PlayerBase + (Offsets.PlayerCompPtr.ArraySizeOffset * i), (IntPtr)Offsets.PlayerCompPtr.GodMode);
            }

        }

        public void DisableGodMode()
        {
            for (int i = 0; i < 4; i++)
            {
                _memory.Write<byte>(false, 0x20, _baseAddress + Offsets.PlayerBase + (Offsets.PlayerCompPtr.ArraySizeOffset * i), (IntPtr)Offsets.PlayerCompPtr.GodMode);
            }
        }
    }
}
