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

        public Dictionary<string, int> weapons = new Dictionary<string, int>
        {
            //Assault Rifles:
            {"XM4", 42},
            {"AK47", 4},
            {"Krig6",8},
            {"QBZ83",40},
            {"FFAR1",23},
            {"Groza",39},
            //Submachine Guns
            {"MP5",32},
            {"Milano821",47},
            {"AK74u",34},
            {"KSP45",14},
            {"Bullfrog",11},
            {"Mac10",36},
            //Tactical Rifles
            {"Type63",28},
            {"M16",31},
            {"AUG",30},
            {"DMR14",27 },
            //Light Machine Guns
            {"Stoner63",2},
            {"RPD",37},
            {"M60",50},
            //Sniper Rifles
            {"Pelington703",25},
            {"LW3Yundra",38},
            {"M82",3},
            //Pistols
            {"P1911",12},
            {"Magnum",16},
            {"Diamatti",22},
            //Shotguns
            {"Hauer77",29},
            {"GalloSA12",41},
            {"Streetsweeper",49},
            //Launcher
            {"Cigma2",18},
            {"RPG7",7},
            //Melee
            {"Knife",9},
            {"Sledgehammer",21},
            {"Wakizashi",24},
            //Special:
            {"M79",6},
            {"Wonder",20},
            {"raygun",44},
        };


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

        public void CritOnly()
        {
            if (!_critOnly)
            {
                _critOnly = !_critOnly;
                _memory.Write(false, -1, _baseAddress + Offsets.PlayerBase, (IntPtr)Offsets.PlayerCompPtr.Crit);
            }
            else
            {
                _critOnly = !_critOnly;
            }
        }

        public void SetWeapon(int id)
        {
            _memory.Write<int>(false, id, _baseAddress + Offsets.PlayerBase, (IntPtr)Offsets.PlayerCompPtr.SetWeaponID /*+ 0x40*/);
        }
    }
}
