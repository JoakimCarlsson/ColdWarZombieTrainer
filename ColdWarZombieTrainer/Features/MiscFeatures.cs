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

        public Dictionary<string, int> weapons = new Dictionary<string, int>
        {
            {"Default Weapon", 1},
            {"Stoner 63", 2},
            {"M82", 3},
            {"AK - 47", 4},
            {"Täuchgranate", 5},
            {"M79", 6},
            {"RPG 7", 7},
            {"Krig 6", 8},
            {"Knife", 9},
            {"Tomahawk(Granade)", 10},
            {"Bullfrog", 11},
            {"1911", 12},
            {"KSP 45", 14},
            {"Granade", 15},
            {"Magnum", 16},
            {"No Granade", 17},
            {"Cigma 2", 18},
            {"Semtex Granade", 19},
            {"D.I.E. - Shockwave", 20},
            {"Sledgehammer", 21},
            {"Diamatti", 22},
            {"Fear 1", 23},
            {"Wakizashi", 24},
            {"Pelington 703", 25},
            {"No Granade", 26},
            {"DMR 14", 27},
            {"TYPE 63", 28},
            {"Hauer 77", 29},
            {"AUG", 30},
            {"M16", 31},
            {"MP5", 32},
            {"Flash Granade", 33},
            {"AK - 74u", 34},
            {"Molotow Granade", 35},
            {"MAC - 10", 36},
            {"RPD", 37},
            {"LW3 - Tundra", 38},
            {"Groza", 39},
            {"QBZ - 83", 40},
            {"Gallo SA12", 41},
            {"XM4", 42},
            {"C4 Granade", 43},
            {"Railgun", 44},
            {"Milano 821", 47},
            {"Monkey Granade", 48},
            {"Streetsweeper", 49},
            {"M60", 50},
            {"Gunship - Bomb1", 51},
            {"Gunship - Bomb2", 53},
            {"Gunship - MG", 54},
            {"Sentry", 55},
            {"Attack Helicopter - MG", 57},
            {"Drone Squad", 58},
            {"Unamed Weapon - RPG", 59},
            {"Unamed Weapon - NoDamage", 60},
            {"Snipers Nest", 62},
            {"Unamed Weapon - MG", 63},
            {"Unamed Weapon - MG", 64},
            {"Shopper Gunner", 65},
            {"Attack Helicopter - MG", 67},
            {"Unamed Weapon - NoDamage", 68},
            {"Unamed Weapon - NoDamage", 69},
            {"98 = Big Punch(Granade)", 70},
            {"Magnum", 99},
            {"Big Punch(Granade)", 100},
            {"Big Punch(Granade)", 101},
            {"Mobile Shield Unit", 109},
            {"Fire Granade Small", 110},
            {"Smoke Granade", 111},
            {"Fire Granade Medium", 113},
            {"Frag Granade", 114},
            {"Mine(no Damage)", 115},
            {"120 = Big Punch(Granade)", 119},
            {"Special ?", 121},
            {"Cruise Missile", 123},
            {"Cruise Missile", 124},
            {"Reset to 0", 125},
            {"Reset to 0", 131},
            {"1911", 135},
            {"Frag Granade", 136},
            {"Snowball", 145},
            {"Stacheldraht", 146},
            {"Fire Granade Medium", 147},
            {"Unamed Weapon", 149},
            {"Flash Granade", 152},
            {"Broken Fists", 153},
            {"Big Punch(Granade)", 155},
            {"Knife(with nice design)", 156},
            {"Throphy", 157},
            {"Diamatti", 158},
            {"Fire Granade Big", 162},
            {"164 = Trigger Fog and Snowfall", 163},
            {"Unamed Weapon", 165},
            {"Gasbomb", 167},
            {"Fire Granade Big", 176},
            {"Fire Bomb", 177},
            {"Fire Bomb 2", 179},
            {"Explosion without Damage", 181},
            {"185 = Explosion without Effects", 182},
            {"Explosion without Effects", 186},
            {"Road Rage", 206},
            {"Field Upgrade: Frost Blast", 207},
            {"Field Upgrade: Aether Shroud", 208},
            {"Field Upgrade: IDN", 209},
            {"Field Upgrade: Aether Shroud", 210},
            {"Field Upgrade: Aether Shroud", 211},
            {"AUG - M3NT", 212},
            {"Unamed Weapon NoDamage", 213},
            {"Shotgun Shells", 214},
            {"Reset to 0", 215},
            {"D.I.E. - Nova - 5 NoDamage", 216},
            {"222 = Field Upgrade: Ring of Fire", 218},
            {"Field Upgrade: Frost Blast", 223},
            {"Private Eye", 224},
            {"Permafrost", 225},
            {"Pain & Suffering", 226},
            {"Blitzkrig 99", 228},
            {"Frag Granade", 229},
            {"Pain", 230},
            {"Reset 2 0", 231},
            {"Demolisher K14", 232},
            {"Sniper Rounds", 233},
            {"Xeno Matter 4000", 236},
            {"CR17 - D20", 237},
            {"Die - a - Lotti & Die - a - Mori", 239},
            {"Anathema", 241},
            {"Small Caliber", 242},
            {"Slow Burn", 243},
            {"Reset to 0", 244},
            {"Rasputins Retribution", 245},
            {"Yaoguai", 247},
            {"Winnower", 248},
            {"Wardens Shotgun", 256},
            {"Yamikirimaru", 257},
            {"High Anxiety", 258},
            {"263 = Field Upgrade: Healing Aura", 259},
            {"Omega 3FA", 264},
            {"Pellegrino Della Morte", 265},
            {"Reset to 0", 266},
            {"Field Upgrade: Energy Mine", 269},
            {"Tugarin", 270},
            {"Psychotropic Thunder", 271},
            {"Ammunation Fists", 272},
            {"Broken Fists(NoDamage)", 273},
            {"Large Caliber", 274},
            {"Special Ammo", 275},
            {"280 = Field Upgrade: Energy Mine", 276},
            {"Reset to 0", 281},
            {"Glitter Knife", 282},
            {"Assault Rounds", 283},
            {"Private Eye & Femme Fatale", 284},
            {"Soccubus Stinger", 286},
            {"Zjolnir", 288},
            {"Orion 777", 292},
            {"Mystic Pony Express", 293},
            {"Hell Vetica", 294},
            {"D.I.E. - Electrobolt", 295},
            {"Reset to 0", 296},
            {"Porters X2 Ray Gun", 298},
            {"Matter Dismantler", 299},
            {"Reset to 0", 300},
            {"D.I.E. - Cryo - Emitter", 301},
            {"Herald of Woe", 305},
            {"Ruinous Pain Distributor", 307},
            {"D.I.E. - Nova - 5", 308},
            {"Wardens Shotgun", 309},
            {"Frag Granade", 310},
            {"Skullsplitter", 311},
            {"D.I.E. - Thermophasic", 312},
            {"Ray Gun", 313},
            {"AK - 74Nofu", 314},
            {"Royale with Lead", 315},
            {"Stone", 316},
            {"Cleymore(NoDamage)", 317},
            {"Closing Argument(Knife)", 318},
            {"Field Upgrade: Healing Aura", 319},
            {"Field Upgrade: Energy Mine", 321},
            {"H - NGM - N", 322},
            {"Die - a - Lotti", 323},
            {"Error Nora 410", 326},
            {"Child of Gaia", 328},
            {"Golden Bullet", 330},
            {"Frag Granade", 331},
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
                //todo fix me, probaly does not work.
                _memory.Write(false, -1, _baseAddress + Offsets.PlayerBase, (IntPtr)4300);
                _memory.Write(false, -1, _baseAddress + Offsets.PlayerBase, (IntPtr)4304);
                _memory.Write(false, -1, _baseAddress + Offsets.PlayerBase, (IntPtr)4324);
                _memory.Write(false, -1, _baseAddress + Offsets.PlayerBase, (IntPtr)4328);
                _memory.Write(false, -1, _baseAddress + Offsets.PlayerBase, (IntPtr)4292);
                _memory.Write(false, -1, _baseAddress + Offsets.PlayerBase, (IntPtr)4296);
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
