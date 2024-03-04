using System.Collections.Generic;

namespace Data
{
    public static class Storage
    {
        public struct Mob
        {
            public string Name { get; private set; }
            public string ScenePath { get; private set; }
            public float Health { get; private set; }
            public float AttackPower { get; private set; }

            public Mob(string name, float health, float attackPower, string scenePath)
            {
                Name = name;
                Health = health;
                ScenePath = scenePath;
                AttackPower = attackPower;
            }
        }

        public struct Level
        {
            public string Name { get; private set; }
            public string ScenePath { get; private set; }
            public bool IsComplete { get; set; }
            public float StartPlayerHealth { get; private set; }

            public Level(string name, string scenePath, float startPlayerHealth, bool isCompleted = false)
            {
                Name = name;
                ScenePath = scenePath;
                IsComplete = isCompleted;
                StartPlayerHealth = startPlayerHealth;
            }
        }

        public struct Build 
        {
            public string ScenePath { get ; private set; }
            public bool Aviable { get; private set; }
            public int Level { get; set; }
            public float Damage { get; set; }

            public Build(string scenePath, int level, float damage, bool aviable = false)
            {
                ScenePath = scenePath;
                Aviable = aviable;
                Level = level;
                Damage = damage;
            }
        }

        public static readonly string PlayerDataSavePath = "user://PlayerData.json";
        public static readonly string LevelsDataSavePath = "user://LevelsData.json";

        public static class GlobalInfo
        {
            public static int CurrentLevelIdx { get; set; }
        }

        public static readonly Mob[] MobsList = new Mob[]
        {
            new Mob("DevMob", 10, 10, "res://Scenes/Dev_Mob.tscn")
        };

        public static readonly Level[] LevelsList = new Level[]
        {
            new Level("Dev Level", "res://Scenes/Levels/Debug_Level.tscn", 100)
        };

        public static readonly Dictionary<string, Build> BuildsList = new Dictionary<string, Build>()
        {
            {"CrossBow", new Build("res://Scenes/Player_Builds/Cross_Bow.tscn", 1, 10, true)}
        };
    }
}


