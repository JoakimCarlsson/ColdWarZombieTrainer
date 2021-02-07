using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using BlueRain;
using ColdWarZombieTrainer.Utils;

namespace ColdWarZombieTrainer.Features
{
    class ZombieHack
    {
        private IntPtr _playerPedPtr;
        private IntPtr _zmBotListBase;
        private IntPtr _zmGlobalBase;
        private NativeMemory _memory;

        private Vector3 _playerPosition;

        public ZombieHack(IntPtr playerPedPtr, IntPtr zmBotListBase, IntPtr zmGlobalBase, NativeMemory memory)
        {
            _playerPedPtr = playerPedPtr;
            _zmBotListBase = zmBotListBase;
            _zmGlobalBase = zmGlobalBase;
            _memory = memory;
        }

        public void OneHpZombies()
        {
            for (int i = 0; i < 120; i++)
            {
                _memory.Write(false, 1, (_zmBotListBase + (Offsets.ZombieBotListBase.BotArraySizeOffset * i) + Offsets.ZombieBotListBase.BotHealth));
                _memory.Write(false, 1, (_zmBotListBase + (Offsets.ZombieBotListBase.BotArraySizeOffset * i) + Offsets.ZombieBotListBase.BotMaxHealth));
            }
        }

        public void TeleportZombies(bool TeleportCrosshair, int distance = 150)
        {
            //this is the position we teleport the zombie too.
            //we could just set newEnemyPosition too another vector3 and they would teleport too that.
            Vector3 newEnemyPosition = TeleportCrosshair ? NewEnemyPosition(distance) : _playerPosition;

            byte[] enemyPosBuffer = new byte[12];

            Buffer.BlockCopy(BitConverter.GetBytes(newEnemyPosition.X), 0, enemyPosBuffer, 0, 4);
            Buffer.BlockCopy(BitConverter.GetBytes(newEnemyPosition.Y), 0, enemyPosBuffer, 4, 4);
            Buffer.BlockCopy(BitConverter.GetBytes(newEnemyPosition.Z), 0, enemyPosBuffer, 8, 4);

            for (int i = 0; i < 120; i++)
                _memory.WriteBytes(_zmBotListBase + (Offsets.ZombieBotListBase.BotArraySizeOffset * i) + Offsets.ZombieBotListBase.Coords, enemyPosBuffer);
        }

        private Vector3 NewEnemyPosition(int distance)
        {
            Vector3 enemyPosition;
            Vector3 playerPosition = GetPlayerPosition();

            byte[] playerHeadingXY = _memory.ReadBytes(_playerPedPtr + Offsets.PlayerPedPtr.HeadingXY, 4);
            byte[] playerHeadingZ = _memory.ReadBytes(_playerPedPtr + Offsets.PlayerPedPtr.HeadingZ, 4);

            double pitch = -MathUtils.ConvertToRadians(BitConverter.ToSingle(playerHeadingZ, 0));
            double yaw = MathUtils.ConvertToRadians(BitConverter.ToSingle(playerHeadingXY, 0));

            enemyPosition.X = Convert.ToSingle(Math.Cos(yaw) * Math.Cos(pitch));
            enemyPosition.Y = Convert.ToSingle(Math.Sin(yaw) * Math.Cos(pitch));
            enemyPosition.Z = Convert.ToSingle(Math.Sin(pitch));

            return playerPosition + (enemyPosition * distance);
        }

        private Vector3 GetPlayerPosition()
        {
            byte[] playerCoords = _memory.ReadBytes(_playerPedPtr + Offsets.PlayerPedPtr.Coords, 12);
            Vector3 playerPosition;

            playerPosition.X = BitConverter.ToSingle(playerCoords, 0);
            playerPosition.Y = BitConverter.ToSingle(playerCoords, 4);
            playerPosition.Z = BitConverter.ToSingle(playerCoords, 8);

            return new Vector3((float)Math.Round(playerPosition.X, 4), (float)Math.Round(playerPosition.Y, 4), (float)Math.Round(playerPosition.Z, 4));
        }

        public Vector3 SetPosition()
        {
            _playerPosition = GetPlayerPosition();
            return _playerPosition;
        }
    }
}
