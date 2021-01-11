using System;
using Memory;

namespace ColdWarZombieTrainer.Features
{
    class InfiniteAmmo
    {
        private IntPtr _baseAddress;
        private NativeMemory _memory;
        public InfiniteAmmo(IntPtr baseAddress, NativeMemory memory)
        {
            _baseAddress = baseAddress;
            _memory = memory;
        }

        public void DoInfiniteAmmo()
        {
            try
            {
                for (int i = 1; i < 6; i++)
                {
                    _memory.Write(false, 5, _baseAddress + Offsets.PlayerBase, (IntPtr)Offsets.Ammo + (i * 0x4));
                }

            }
            catch
            {
            }

        }
    }
}
