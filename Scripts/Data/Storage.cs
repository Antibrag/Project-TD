namespace Data
{
    public class Storage
    {
        public static readonly string PlayerDataSavePath = "user://PlayerData.json";
        public static readonly string LevelsDataSavePath = "user://LevelsData.json";

        public static class GlobalInfo
        {
            public static int Level { get; set; }
        }

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

            public Level(string name, string scenePath, bool isCompleted = false)
            {
                Name = name;
                ScenePath = scenePath;
                IsComplete = isCompleted;
            }
        }

        //public class Player...

        public static Mob[] MobsList = new Mob[]
        {
            new Mob("DevMob", 10, 10, "res://Scenes/Dev_Mob.tscn")
        };

        public static Level[] LevelsList = new Level[]
        {
            new Level("Dev Level", "res://Scenes/Levels/Debug_Level.tscn")
        };
    }
}


