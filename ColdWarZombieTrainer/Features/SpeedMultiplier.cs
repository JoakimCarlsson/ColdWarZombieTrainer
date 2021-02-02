using System;
using BlueRain;

namespace ColdWarZombieTrainer.Features
{
    class SpeedMultiplier
    {
        private IntPtr _baseAddress;
        private NativeMemory _memory;

        public SpeedMultiplier(IntPtr baseAddress, NativeMemory memory)
        {
            _baseAddress = baseAddress;
            _memory = memory;
        }

        public void SetSpeed(float speed)
        {
            _memory.Write(false, speed, _baseAddress + Offsets.PlayerBase, (IntPtr)Offsets.PlayerCompPtr.RunSpeed);
        }

        public void SetTimeScale(float speed)
        {
            _memory.Write((IntPtr)_baseAddress+Offsets.TimeScaleBase, speed);
        }
    }
}
