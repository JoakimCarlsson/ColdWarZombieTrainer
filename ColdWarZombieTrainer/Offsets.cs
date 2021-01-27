using System;

namespace ColdWarZombieTrainer
{
    class Offsets
    {
        public static int PlayerBase = 0xFC297B8;
        public static int ZMXPScaleBase = 0xFC517B0;
        public static int TimeScaleBase = 0xECF9C74 + 0x7C;

        public static class PlayerCompPtr
        {
            public static int ArraySizeOffset = 0xB830;
            public static int InfraredVision = 0xE66; //On=0x10|Off=0x0
            public static int GodMode = 0xE67;
            public static int RunSpeed = 0x5C30;
            public static int Ammo = 0x13D4;
            public static int Points = 0x5CE4;
            public static int RapidFire1 = 0xE6C;
            public static int RapidFire2 = 0xE80;
            public static int Crit = 0x10CC;
            public static int Name = 0x5BDA;
            public static int CurrentUsedWeaponID = 0x28;
            public static int SetWeaponID = 0xB0;
            public static int JumpHeight = 0xFE63458; //gamebase + 0xFE63458 (pointer) + 0x130 (default value 39.0float)
        }


        public class PlayerPedPtr
        {
            public static int Coords = 0x2D4;
            public static int HeadingXY = 0x38;
            public static int HeadingZ = 0x34;
            public static int ArraySizeOffset = 0x5F8;
        }

        public class ZombieBotListBase
        {
            public static int BotMaxHealth = 0x39C;
            public static int BotHealth = 0x398;
            public static int BotArraySizeOffset = 0x5F8;
            public static int Coords = 0x2D4;
        }

        public class ZombieGlobalClass
        {
            public static int ZombieLeftCount = 0x3C;
        }

        public class ZombieXpScaleBase
        {
            public static int XPGun = 0x30; //XPGun_Offset
            public static int XPUserFake = 0x20; //Fake XPEP_InGame_Offset
            public static int XPUserReal = 0x28; //Real XPEP_RealAdd_Offset
        }
    }
}
