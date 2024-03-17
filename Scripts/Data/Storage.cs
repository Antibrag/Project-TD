namespace Data
{
    public static class Storage
    {
        private interface IClonable
        {
            object Clone();
        }

        public class Mob : IClonable 
        {
            public string Name { get; private set; }
            public string ScenePath { get; private set; }
            public float Health { get; set; }
            public float AttackPower { get; private set; }

            public Mob(string name, float health, float attackPower, string scenePath)
            {
                Name = name;
                Health = health;
                ScenePath = scenePath;
                AttackPower = attackPower;
            }

            public object Clone() =>     
                new Mob(Name, Health, AttackPower, ScenePath);
            
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

        public class Build : IClonable
        {
            public string Name { get; private set; }
            public string ScenePath { get ; private set; }
            public bool Available { get; set; }
            public int Level { get; set; }
            public float Damage { get; set; }
            public float AttackSpeed { get; set; }

            public Build(string name, string scenePath, int level, float damage, float attackSpeed, bool available = false)
            {
                Name = name;
                ScenePath = scenePath;
                Available = available;
                Level = level;
                Damage = damage;
                AttackSpeed = attackSpeed;
            }

            public object Clone() =>
                new Build(Name, ScenePath, Level, Damage, AttackSpeed, Available);
        }

        public static readonly string PlayerDataSavePath = "user://PlayerData.json";
        public static readonly string LevelsDataSavePath = "user://LevelsData.json";

        public static class GlobalInfo
        {
            public static int CurrentLevelIdx { get; set; }
        }

        public static readonly Mob[] MobsList = new Mob[]
        {
            //Mob name, Mob health, Mob attack power, mob scene path
            new Mob("DevMob", 10, 10, "res://Scenes/Dev_Mob.tscn")
        };

        public static readonly Level[] LevelsList = new Level[]
        {
            //Level name, Level path, Player start health
            new Level("Dev Level", "res://Scenes/Levels/Debug_Level.tscn", 100)
        };

        public static readonly Build[] BuildsList = new Build[]
        {
            //Build name, build scene path, build level, build damage, build attack speed, build is available? 
            new Build("CrossBow", "res://Scenes/Player_Builds/Cross_Bow.tscn", 1, 10, 0.1f, true)
        };
    }
}


