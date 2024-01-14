using Godot.Collections;

public class Data
{
    public struct Mob
    {
        public string Name { get; private set; }
        public string ScenePath { get; private set; }
        public float Health { get; private set; }

        public Mob(string name, float health, string scenePath)
        {
            Name = name;
            Health = health;
            ScenePath = scenePath;
        }
    }

    public struct Level 
    {
        public string Name { get; private set; }
        public string ScenePath { get; private set; }
        public bool IsCompleted { get; set; }

        public Level(string name, string scenePath, bool isCompleted = false)
        {
            Name = name;
            ScenePath = scenePath;
            IsCompleted = isCompleted;
        }
    }

    //public class Player...

    public static Mob[] MobsList = new Mob[]
    {
        new Mob("DevMob", 10, "res://Scenes/Dev_Mob.tscn")
    };

    public static Level[] LevelsList = new Level[]
    {
        new Level("Dev Level", "res://Scenes/Debug_Level.tscn")
    };
}

