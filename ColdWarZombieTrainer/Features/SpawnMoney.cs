using System;
using BlueRain;

namespace ColdWarZombieTrainer.Features
{
    class SpawnMoney
    {
        private IntPtr _baseAddress;
        private NativeMemory _memory;

        public SpawnMoney(IntPtr baseAddress, NativeMemory memory)
        {
            _baseAddress = baseAddress;
            _memory = memory;
        }

        public void InfiniteMoney()
        {
             _memory.Write<int>(true, 13337, _baseAddress + Offsets.PlayerBase, (IntPtr)Offsets.PlayerCompPtr.Points);
        }
    }
}
