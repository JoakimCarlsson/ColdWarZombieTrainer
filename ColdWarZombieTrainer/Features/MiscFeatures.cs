using System;
using System.Collections.Generic;
using System.Text;
using ColdWarZombieTrainer.Utils;
using Memory;

namespace ColdWarZombieTrainer.Features
{
    class MiscFeatures
    {
        private readonly IntPtr _baseAddress;
        private readonly NativeMemory _memory;

        private bool _infraredVision;
        private bool _critOnly;

        public MiscFeatures(IntPtr baseAddress, NativeMemory memory)
        {
            _baseAddress = baseAddress;
            _memory = memory;
        }

        public void ToggleInfraredVision()
        {
            if (!_infraredVision)
            {
                _infraredVision = !_infraredVision;
                _memory.Write<byte>(false, 0x10, _baseAddress + Offsets.PlayerBase, (IntPtr)Offsets.PlayerCompPtr.InfraredVision);

            }
            else
            {
                _infraredVision = !_infraredVision;
                _memory.Write<byte>(false, 0x0, _baseAddress + Offsets.PlayerBase, (IntPtr)Offsets.PlayerCompPtr.InfraredVision);
            }
        }

        public void DoRapidFire()
        {
            if (KeyUtils.GetKeyDown(0x1))
            {
                _memory.Write(false, 0, _baseAddress + Offsets.PlayerBase, (IntPtr)Offsets.PlayerCompPtr.RapidFire1);
                _memory.Write(false, 0, _baseAddress + Offsets.PlayerBase, (IntPtr)Offsets.PlayerCompPtr.RapidFire2);
            }
        }

        public void ToggleCritOnly()
        {
            if (!_critOnly)
            {
                _critOnly = !_critOnly;
                //Do Crit Only
            }
            else
            {
                _critOnly = !_critOnly;
                //Sthap
            }
        }
    }
}
