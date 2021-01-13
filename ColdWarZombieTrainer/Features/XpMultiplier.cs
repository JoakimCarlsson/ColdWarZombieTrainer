using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Memory;

namespace ColdWarZombieTrainer.Features
{
    class XpMultiplier
    {
        private IntPtr _baseAddress;
        private NativeMemory _memory;

        public XpMultiplier(IntPtr baseAddress, NativeMemory memory)
        {
            _baseAddress = baseAddress;
            _memory = memory;
        }

        public void PlayerXpMultiplier(float multiplier)
        {
            _memory.Write(_baseAddress+Offsets.ZMXPScaleBase+Offsets.ZombieXpScaleBase.XPUserReal, multiplier);
            //_memory.Write(_baseAddress+Offsets.ZMXPScaleBase+Offsets.ZombieXpScaleBase.XPUserFake, multiplier);
        }

        public void GunXpMultiplier(float multiplier)
        {
            _memory.Write(_baseAddress + Offsets.ZMXPScaleBase + Offsets.ZombieXpScaleBase.XPGun, multiplier);
        }
    }
}
