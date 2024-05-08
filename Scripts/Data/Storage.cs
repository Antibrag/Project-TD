using System.Collections.Generic;
using Godot;

namespace Data
{
    public static class Storage
    {
        public static readonly string PlayerDataSavePath = "user://PlayerData.json";
        public static readonly string LevelsDataSavePath = "user://LevelsData.json";
        public static string CurrentLevel;

        private static readonly Dictionary<string, Projectile> ProjectilesList = new Dictionary<string, Projectile>()
        {
            //MeshObject, level, attackPower, penetrationPower, flightSpeed
            {"Wood Arrow", new Projectile(1, 20, 0.5f, 20)}
        };

        public static readonly Dictionary<string, Mob> MobsList = new Dictionary<string, Mob>()
        {
            //Mob name, Mob health, Mob attack power, mob scene path
            {"DevMob", new Mob(100, 10, 1)}
        };

        public static readonly Dictionary<string, Level> LevelsList = new Dictionary<string, Level>()
        {
            //Level name, Level path, Player start health
            {"Dev Level", new Level("res://Scenes/Levels/Debug_Level.tscn", 100)}
        };

        public static readonly Dictionary<string, Build> BuildsList = new Dictionary<string, Build>()
        {
            {"CrossBow", new Build(
                new BuildMesh(new Vector3(0,0,0), new Vector3(1.9f,4.1f,0)), 
                1, //Level
                1, //AttackSpeed
                new Dictionary<string, Projectile>() { {"Wood Arrow", ProjectilesList["Wood Arrow"]} } 
            )}
        };

        public static readonly Dictionary<string, BSButton> BSButtonsList = new Dictionary<string, BSButton>()
        {
            {"1BButton", new BSButton(Key.Key1, "CrossBow", "res://Assets/Textures/Skill-Build background button.png")},
            {"2BButton", new BSButton()},
            {"3BButton", new BSButton()},
            {"4BButton", new BSButton()},
            {"5BButton", new BSButton()},
            {"6BButton", new BSButton()},
            {"6BButton", new BSButton()},
        };
    }
}


