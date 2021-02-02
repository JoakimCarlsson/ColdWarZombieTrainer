using System;
using System.Collections.Generic;
using System.Text;
using BlueRain;
using ColdWarZombieTrainer.Utils;

namespace ColdWarZombieTrainer.Features
{
    class MiscFeatures
    {
        private readonly IntPtr _baseAddress;
        private readonly NativeMemory _memory;

        private bool _infraredVision;
        private bool _critOnly;

        public Dictionary<int, string> weapons = new Dictionary<int, string>
        {
            {0, "Remove Weapon"},
            {1, "Default Weapon"},
            {2, "Stoner 63"},
            {3, "M82"},
            {4, "AK - 47"},
            {5, "Täuchgranate"},
            {6, "M79"},
            {7, "RPG 7"},
            {8, "Krig 6"},
            {9, "Knife"},
            {10, "Tomahawk(Granade)"},
            {11, "Bullfrog"},
            {12, "1911"},
            {14, "KSP 45"},
            {15, "Granade"},
            {16, "Magnum"},
            {17, "No Granade"},
            {18, "Cigma 2"},
            {19, "Semtex Granade"},
            {20, "D.I.E. - Shockwave"},
            {21, "Sledgehammer"},
            {22, "Diamatti"},
            {23, "Fear 1"},
            {24, "Wakizashi"},
            {25, "Pelington 703"},
            {26, "No Granade"},
            {27, "DMR 14"},
            {28, "TYPE 63"},
            {29, "Hauer 77"},
            {30, "AUG"},
            {31, "M16"},
            {32, "MP5"},
            {33, "Flash Granade"},
            {34, "AK - 74u"},
            {35, "Molotow Granade"},
            {36, "MAC - 10"},
            {37, "RPD"},
            {38, "LW3 - Tundra"},
            {39, "Groza"},
            {40, "QBZ - 83"},
            {41, "Gallo SA12"},
            {42, "XM4"},
            {43, "C4 Granade"},
            {44, "Railgun"},
            {47, "Milano 821"},
            {48, "Monkey Granade"},
            {49, "Streetsweeper"},
            {50, "M60"},
            {51, "Gunship - Bomb1"},
            {53, "Gunship - Bomb2"},
            {54, "Gunship - MG"},
            {55, "Sentry"},
            {57, "Attack Helicopter - MG"},
            {58, "Drone Squad"},
            {59, "Unamed Weapon - RPG"},
            {60, "Unamed Weapon - NoDamage"},
            {62, "Snipers Nest"},
            {63, "Unamed Weapon - MG"},
            {64, "Unamed Weapon - MG"},
            {65, "Shopper Gunner"},
            {67, "Attack Helicopter - MG"},
            {68, "Unamed Weapon - NoDamage"},
            {69, "Unamed Weapon - NoDamage"},
            {70, "Big Punch(Granade)"},
            {99, "Magnum"},
            {100, "Big Punch(Granade)"},
            {101, "Big Punch(Granade)"},
            {109, "Mobile Shield Unit"},
            {110, "Fire Granade Small"},
            {111, "Smoke Granade"},
            {113, "Fire Granade Medium"},
            {114, "Frag Granade"},
            {115, "Mine(no Damage)"},
            {119, "Big Punch(Granade)"},
            {121, "Special ?"},
            {123, "Cruise Missile"},
            {124, "Cruise Missile"},
            {125, "Reset to 0"},
            {131, "Reset to 0"},
            {135, "1911"},
            {136, "Frag Granade"},
            {145, "Snowball"},
            {146, "Stacheldraht"},
            {147, "Fire Granade Medium"},
            {149, "Unamed Weapon"},
            {152, "Flash Granade"},
            {153, "Broken Fists"},
            {155, "Big Punch(Granade)"},
            {156, "Knife(with nice design)"},
            {157, "Throphy"},
            {158, "Diamatti"},
            {162, "Fire Granade Big"},
            {163, "Trigger Fog and Snowfall"},
            {165, "Unamed Weapon"},
            {167, "Gasbomb"},
            {176, "Fire Granade Big"},
            {177, "Fire Bomb"},
            {179, "Fire Bomb 2"},
            {181, "Explosion without Damage"},
            {182, "Explosion without Effects"},
            {186, "Explosion without Effects"},
            {206, "Road Rage"},
            {207, "Field Upgrade: Frost Blast"},
            {208, "Field Upgrade: Aether Shroud"},
            {209, "Field Upgrade: IDN"},
            {210, "Field Upgrade: Aether Shroud"},
            {211, "Field Upgrade: Aether Shroud"},
            {212, "AUG - M3NT"},
            {213, "Unamed Weapon NoDamage"},
            {214, "Shotgun Shells"},
            {215, "Reset to 0"},
            {216, "D.I.E. - Nova - 5 NoDamage"},
            {218, "Field Upgrade: Ring of Fire"},
            {223, "Field Upgrade: Frost Blast"},
            {224, "Private Eye"},
            {225, "Permafrost"},
            {226, "Pain & Suffering"},
            {228, "Blitzkrig 99"},
            {229, "Frag Granade"},
            {230, "Pain"},
            {231, "Reset 2 0"},
            {232, "Demolisher K14"},
            {233, "Sniper Rounds"},
            {236, "Xeno Matter 4000"},
            {237, "CR17 - D20"},
            {239, "Die - a - Lotti & Die - a - Mori"},
            {241, "Anathema"},
            {242, "Small Caliber"},
            {243, "Slow Burn"},
            {244, "Reset to 0"},
            {245, "Rasputins Retribution"},
            {247, "Yaoguai"},
            {248, "Winnower"},
            {256, "Wardens Shotgun"},
            {257, "Yamikirimaru"},
            {258, "High Anxiety"},
            {259, "Field Upgrade: Healing Aura"},
            {264, "Omega 3FA"},
            {265, "Pellegrino Della Morte"},
            {266, "Reset to 0"},
            {269, "Field Upgrade: Energy Mine"},
            {270, "Tugarin"},
            {271, "Psychotropic Thunder"},
            {272, "Ammunation Fists"},
            {273, "Broken Fists(NoDamage)"},
            {274, "Large Caliber"},
            {275, "Special Ammo"},
            {276, "Field Upgrade: Energy Mine"},
            {281, "Reset to 0"},
            {282, "Glitter Knife"},
            {283, "Assault Rounds"},
            {284, "Private Eye & Femme Fatale"},
            {286, "Soccubus Stinger"},
            {288, "Zjolnir"},
            {292, "Orion 777"},
            {293, "Mystic Pony Express"},
            {294, "Hell Vetica"},
            {295, "D.I.E. - Electrobolt"},
            {296, "Reset to 0"},
            {298, "Porters X2 Ray Gun"},
            {299, "Matter Dismantler"},
            {300, "Reset to 0"},
            {301, "D.I.E. - Cryo - Emitter"},
            {305, "Herald of Woe"},
            {307, "Ruinous Pain Distributor"},
            {308, "D.I.E. - Nova - 5"},
            {309, "Wardens Shotgun"},
            {310, "Frag Granade"},
            {311, "Skullsplitter"},
            {312, "D.I.E. - Thermophasic"},
            {313, "Ray Gun"},
            {314, "AK - 74Nofu"},
            {315, "Royale with Lead"},
            {316, "Stone"},
            {317, "Cleymore(NoDamage)"},
            {318, "Closing Argument(Knife)"},
            {319, "Field Upgrade: Healing Aura"},
            {321, "Field Upgrade: Energy Mine"},
            {322, "H - NGM - N"},
            {323, "Die - a - Lotti"},
            {326, "Error Nora 410"},
            {328, "Child of Gaia"},
            {330, "Golden Bullet"},
            {331, "Frag Granade"},
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
                _memory.Write(false, -1, _baseAddress + Offsets.PlayerBase, (IntPtr)Offsets.PlayerCompPtr.RapidFire1);
                _memory.Write(false, -1, _baseAddress + Offsets.PlayerBase, (IntPtr)Offsets.PlayerCompPtr.RapidFire2);
            }
        }

        public void CritOnly()
        {
            if (!_critOnly)
            {
                //wtf is this shite, copy pate much?
                _critOnly = !_critOnly;
                _memory.Write(false, -1, _baseAddress + Offsets.PlayerBase, (IntPtr) Offsets.PlayerCompPtr.CritKill1);
                _memory.Write(false, -1, _baseAddress + Offsets.PlayerBase, (IntPtr) Offsets.PlayerCompPtr.CritKill2);
                _memory.Write(false, -1, _baseAddress + Offsets.PlayerBase, (IntPtr) Offsets.PlayerCompPtr.CritKill3);
                _memory.Write(false, -1, _baseAddress + Offsets.PlayerBase, (IntPtr) Offsets.PlayerCompPtr.CritKill4);
                _memory.Write(false, -1, _baseAddress + Offsets.PlayerBase, (IntPtr) Offsets.PlayerCompPtr.CritKill5);
                _memory.Write(false, -1, _baseAddress + Offsets.PlayerBase, (IntPtr) Offsets.PlayerCompPtr.CritKill6);
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

        public void AutomaticWeaponSwitch()
        {

        }
    }
}
