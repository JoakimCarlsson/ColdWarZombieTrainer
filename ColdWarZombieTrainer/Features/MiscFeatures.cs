using System;
using System.Collections.Generic;
using System.Text;
using Memory;

namespace ColdWarZombieTrainer.Features
{
    class MiscFeatures
    {
        private readonly IntPtr _baseAddress;
        private readonly NativeMemory _memory;

        private bool _InfraredVision;

        public MiscFeatures(IntPtr baseAddress, NativeMemory memory)
        {
            _baseAddress = baseAddress;
            _memory = memory;
        }

        public void ToggleInfraredVision()
        {
            if (!_InfraredVision)
            {
                _InfraredVision = !_InfraredVision;
                _memory.Write<byte>(false, 0x10, _baseAddress + Offsets.PlayerBase, (IntPtr)Offsets.PlayerCompPtr.InfraredVision);

            }
            else
            {
                _InfraredVision = !_InfraredVision;
                _memory.Write<byte>(false, 0x0, _baseAddress + Offsets.PlayerBase, (IntPtr)Offsets.PlayerCompPtr.InfraredVision);
            }
        }
    }
}
