using System;
using System.Collections.Generic;
using System.Text;
using Memory;

namespace ColdWarZombieTrainer.Features
{
    class ZombieHack
    {
        private IntPtr _baseAddress;
        private NativeMemory _memory;

        public ZombieHack(IntPtr baseAddress, NativeMemory memory)
        {
            _baseAddress = baseAddress;
            _memory = memory;
        }

        public void OneHpZombies()
        {
            try
            {
                for (int i = 0; i < 90; i++)
                {
                    _memory.Write(false, 1, (_baseAddress + (0x5F8 * i) + 0x398));
                    _memory.Write(false, 1, (_baseAddress + (0x5F8 * i) + 0x39C));
                }
            }
            catch (Exception e)
            {

            }
        }

        public void TpZombiesToCrossHair()
        {

        }
    }
}
