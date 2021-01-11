using System;
using Memory;

namespace ColdWarZombieTrainer.Features
{
    class SpeedHack
    {
        private IntPtr _baseAddress;
        private NativeMemory _memory;

        public SpeedHack(IntPtr baseAddress, NativeMemory memory)
        {
            _baseAddress = baseAddress;
            _memory = memory;
        }

        public void SetSpeed(float speed)
        {
            _memory.Write(false, speed, _baseAddress + Offsets.PlayerBase, (IntPtr)Offsets.PlayerCompPtr.RunSpeed);
        }
    }
}
