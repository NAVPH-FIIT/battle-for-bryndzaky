using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using UnityEngine;

namespace Bryndzaky.General.Common
{
    public static class StateManager
    {
        public class GameState
        {
            public class Consumable
            {
                public string name;
                public int count;
            }

            public int skillpoints = 0;
            public int max_health = 100;
            public int move_speed = 10;
            public int dash_speed = 10;
            public int gold = 0;
            public List<string> activeWeapons = new();
            public List<Consumable> consumables = new();
        }


        private static object saveLock;
        private static readonly string path = Application.persistentDataPath + "/gameState.json";
        private static GameState state;
        public static GameState State { 
            get {
                if (state == null)
                {
                    LoadState();
                    AutoSave();
                }
                return state;
            }
        }
        public static string LastSaved { get; private set; }
        public static bool autosaveActive = true;


        private static void SaveState()
        {
            if (state != null)
                File.WriteAllText(path, JsonUtility.ToJson(state));
        }


        private static void LoadState()
        {
            state = File.Exists(path) ? JsonUtility.FromJson<GameState>(File.ReadAllText(path)) : new GameState();
        }


        private static void AutoSave()
        {
            new Thread(() => {
                while (autosaveActive)
                {
                    lock (saveLock)
                        SaveState();
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    Thread.Sleep(10_000);
                }
            }).Start();
        }


        public static void ManualSave(bool quit = false)
        {
            autosaveActive = !quit;
            lock (saveLock)
                SaveState();
        }
    }
}
