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
        public class GameState
        {
            public class Consumable
            {
                public string name;
                public int count;
            }

            public class Stat
            {
                public string name;
                public int value;
                public int increment;
            }

            public class AvailableWeapon
            {
                public string name;
                public int grade;
            }

            public int skillpoints = 0;
            // public int maxHealth = 100;
            // public int moveSpeed = 10;
            // public int dashSpeed = 10;
            public int level = 1;
            public int gold = 0;
            public int xp = 0;
            public List<string> activeWeapons = new();
            public List<Consumable> consumables = new();
            public List<Stat> stats = new List<Stat> {
                new Stat {name="max_health", value=100, increment=50},
                new Stat {name="move_speed", value=10, increment=1},
                new Stat {name="dash_speed", value=10, increment=1}
            };
            public List<AvailableWeapon> availableWeapons = new();

            public int GetWeaponGrade(string name)
            {
                var weapon = this.availableWeapons.Find(w => w.name == name);
                return weapon == null ? -1 : weapon.grade;
            }
        }


        private static object saveLock = new();
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
                    LastSaved = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    Thread.Sleep(10_000);
                }
            }).Start();
        }

        public static void ManualSave(bool quit=false)
        {
            autosaveActive = !quit;
            lock (saveLock)
                SaveState();
        }
    }
}
