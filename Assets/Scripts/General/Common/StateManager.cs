using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

namespace Bryndzaky.General.Common
{
    public static class StateManager
    {
        [Serializable]
        public class GameState
        {
            [Serializable]
            public class Consumable
            {
                public string name;
                public int count;
            }

            [Serializable]
            public class Stat
            {
                public string name;
                public int value;
                public int increment;
            }

            [Serializable]
            public class AvailableWeapon
            {
                public string name;
                public int grade;
            }

            [Serializable]
            public class Quest
            {
                public string name;
                public List<string> milestonesReached;
                public bool completed;

                public Quest(string name)
                {
                    this.name = name;
                    this.milestonesReached = new();
                    this.completed = false;
                }
            }

            public int skillpoints = 0;
            public int level = 1;
            public int NextLevel {get { return 100 * (int) Math.Pow(2, level -1); }}
            public int gold = 100;
            public int xp = 0;
            public List<string> activeWeapons = new List<string> { "Sword" };
            public List<Consumable> consumables = new();
            public List<Stat> stats = new List<Stat> {
                new Stat {name="max_health", value=100, increment=50},
                new Stat {name="move_speed", value=10, increment=1},
                new Stat {name="dash_speed", value=15, increment=5}
            };
            public List<AvailableWeapon> availableWeapons = new List<AvailableWeapon> {
                new AvailableWeapon {name="Sword", grade=0}
            };
            public List<Quest> quests = new();
            public string entryScene = "level_1";
            // public int entryScene = 1;

            public int GetWeaponGrade(string name)
            {
                var weapon = this.availableWeapons.Find(w => w.name == name);
                return weapon == null ? -1 : weapon.grade;
            }
        }


        private static object saveLock = new();
        private static readonly string path = Application.persistentDataPath + "/gameState_V2.json";
        private static GameState state;
        private static Thread autosaveThread;
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
                lock (saveLock)
                    File.WriteAllText(path, JsonUtility.ToJson(state));
        }


        private static void LoadState()
        {
            // Debug.LogError(path);
            state = File.Exists(path) ? JsonUtility.FromJson<GameState>(File.ReadAllText(path)) : new GameState();
            // Debug.LogError(JsonUtility.ToJson(state));
        }


        private static void AutoSave()
        {
            if (autosaveThread != null)
                return;

            autosaveThread = new Thread(() => {
                while (autosaveActive)
                {
                    SaveState();
                    LastSaved = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    Thread.Sleep(10_000);
                }
            });
            autosaveThread.Start();
        }

        public static void ManualSave(bool quit=false)
        {
            if (quit)
            {
                autosaveActive = false;
                autosaveThread.Join(1000);
            }
            SaveState();
        }

        public static void ClearSave()
        {
            lock (saveLock)
            {
                if (File.Exists(path))
                    File.Delete(path);
                state = null;
            }
        }
    }
}
