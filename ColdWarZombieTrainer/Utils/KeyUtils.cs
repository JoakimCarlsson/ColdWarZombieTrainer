using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ColdWarZombieTrainer.Utils
{
    //Credits too who ever made this.
    public class KeyUtils
    {
        #region Fields

        private readonly Hashtable _keys;
        private Hashtable _prevKeys;
        private readonly short[] _allKeys;

        #endregion

        #region Constructor

        public KeyUtils()
        {
            this._keys = new Hashtable();
            _prevKeys = new Hashtable();
            var _keys = (WinAPI.VirtualKeyShort[])Enum.GetValues(typeof(WinAPI.VirtualKeyShort));
            _allKeys = new short[_keys.Length];
            for (var i = 0; i < _allKeys.Length; i++)
                _allKeys[i] = (short)_keys[i];

            Init();
        }

        ~KeyUtils()
        {
            _keys.Clear();
            _prevKeys.Clear();
        }

        #endregion

        #region Methods
        public static bool GetKeyDown(WinAPI.VirtualKeyShort key)
        {
            return GetKeyDown((int)key);
        }

        public static void LMouseClick(int sleeptime)
        {
            WinAPI.mouse_event(WinAPI.MOUSEEVENTF.LEFTDOWN, 0, 0, 0, 0);
            Thread.Sleep(sleeptime);
            WinAPI.mouse_event(WinAPI.MOUSEEVENTF.LEFTUP, 0, 0, 0, 0);
        }

        public static bool GetKeyDown(int key)
        {
            return Convert.ToBoolean(WinAPI.GetKeyState(key) & WinAPI.KEY_PRESSED);
        }

        public static bool GetKeyDownAsync(int key)
        {
            return GetKeyDownAsync((WinAPI.VirtualKeyShort)key);
        }

        public static bool GetKeyDownAsync(WinAPI.VirtualKeyShort key)
        {
            return Convert.ToBoolean(WinAPI.GetAsyncKeyState(key) & WinAPI.KEY_PRESSED);
        }
        /// <summary>
        ///     Initializes and fills the hashtables
        /// </summary>
        private void Init()
        {
            foreach (int key in _allKeys)
            {
                if (!_prevKeys.ContainsKey(key))
                {
                    _prevKeys.Add(key, false);
                    _keys.Add(key, false);
                }
            }
        }

        /// <summary>
        ///     Updates the key-states
        /// </summary>
        public void Update()
        {
            _prevKeys = (Hashtable)_keys.Clone();
            foreach (int key in _allKeys)
            {
                _keys[key] = GetKeyDown(key);
            }
        }

        /// <summary>
        ///     Returns an array of all keys that went up since the last Update-call
        /// </summary>
        /// <returns></returns>
        public WinAPI.VirtualKeyShort[] KeysThatWentUp()
        {
            var keys = new List<WinAPI.VirtualKeyShort>();
            foreach (WinAPI.VirtualKeyShort key in _allKeys)
            {
                if (KeyWentUp(key))
                    keys.Add(key);
            }
            return keys.ToArray();
        }

        /// <summary>
        ///     Returns an array of all keys that went down since the last Update-call
        /// </summary>
        /// <returns></returns>
        public WinAPI.VirtualKeyShort[] KeysThatWentDown()
        {
            var keys = new List<WinAPI.VirtualKeyShort>();
            foreach (WinAPI.VirtualKeyShort key in _allKeys)
            {
                if (KeyWentDown(key))
                    keys.Add(key);
            }
            return keys.ToArray();
        }

        /// <summary>
        ///     Returns an array of all keys that went are down since the last Update-call
        /// </summary>
        /// <returns></returns>
        public WinAPI.VirtualKeyShort[] KeysThatAreDown()
        {
            var keys = new List<WinAPI.VirtualKeyShort>();
            foreach (WinAPI.VirtualKeyShort key in _allKeys)
            {
                if (KeyIsDown(key))
                    keys.Add(key);
            }
            return keys.ToArray();
        }

        /// <summary>
        ///     Returns whether the given key went up since the last Update-call
        /// </summary>
        /// <param name="key">Key to check</param>
        /// <returns></returns>
        public bool KeyWentUp(WinAPI.VirtualKeyShort key)
        {
            return KeyWentUp((int)key);
        }

        /// <summary>
        ///     Returns whether the given key went up since the last Update-call
        /// </summary>
        /// <param name="key">Key to check</param>
        /// <returns></returns>
        public bool KeyWentUp(int key)
        {
            if (!KeyExists(key))
                return false;
            return (bool)_prevKeys[key] && !(bool)_keys[key];
        }

        /// <summary>
        ///     Returns whether the given key went down since the last Update-call
        /// </summary>
        /// <param name="key">Key to check</param>
        /// <returns></returns>
        public bool KeyWentDown(WinAPI.VirtualKeyShort key)
        {
            return KeyWentDown((int)key);
        }

        /// <summary>
        ///     Returns whether the given key went down since the last Update-call
        /// </summary>
        /// <param name="key">Key to check</param>
        /// <returns></returns>
        public bool KeyWentDown(int key)
        {
            if (!KeyExists(key))
                return false;
            return !(bool)_prevKeys[key] && (bool)_keys[key];
        }

        /// <summary>
        ///     Returns whether the given key was down at time of the last Update-call
        /// </summary>
        /// <param name="key">Key to check</param>
        /// <returns></returns>
        public bool KeyIsDown(WinAPI.VirtualKeyShort key)
        {
            return KeyIsDown((int)key);
        }

        /// <summary>
        ///     Returns whether the given key was down at time of the last Update-call
        /// </summary>
        /// <param name="key">Key to check</param>
        /// <returns></returns>
        public bool KeyIsDown(int key)
        {
            if (!KeyExists(key))
                return false;
            return (bool)_prevKeys[key] || (bool)_keys[key];
        }

        /// <summary>
        ///     Returns whether the given key is contained in the used hashtables
        /// </summary>
        /// <param name="key">Key to check</param>
        /// <returns></returns>
        private bool KeyExists(int key)
        {
            return _prevKeys.ContainsKey(key) && _keys.ContainsKey(key);
        }

        #endregion
    }
}
