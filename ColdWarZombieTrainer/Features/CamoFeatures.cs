using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using BlueRain;

namespace ColdWarZombieTrainer.Features
{
    class CamoFeatures
    {
        private readonly IntPtr _baseAddress;
        private readonly IntPtr _zmBotListBase;
        private readonly IntPtr _zmGlobalBase;
        private readonly NativeMemory _memory;

        public CamoFeatures(IntPtr baseAddress, IntPtr zmBotListBase, IntPtr zmGlobalBase, NativeMemory memory)
        {
            _baseAddress = baseAddress;
            _zmBotListBase = zmBotListBase;
            _zmGlobalBase = zmGlobalBase;
            _memory = memory;
        }

        public void OneShotGold()
        {
                
        }
    }
}
