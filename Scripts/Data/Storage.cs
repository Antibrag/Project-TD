using System.Collections.Generic;
using Godot;

namespace Data
{
    public static class Storage
    {
        public static readonly string PlayerDataSavePath = "user://PlayerData.json";
        public static readonly string LevelsDataSavePath = "user://LevelsData.json";
        public static readonly string BSIButtonsConfigurationPath = "user://BSIButtonsConfiguration.json";
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
                new BuildMesh(new Vector3(0,0,0), new Vector3(0.2f,0.4f,0)),
                1, //Level
                25, //ManaCost
                1, //AttackSpeed
                3,
                new Dictionary<string, Projectile>() { {"Wood Arrow", ProjectilesList["Wood Arrow"]} }
            )}
        };

        public static readonly Dictionary<string, BSIButton> BuildButtonsList = new Dictionary<string, BSIButton>()
        {
            {"1BButton", new BSIButton(Key.Key1, "CrossBow")},
            {"2BButton", new BSIButton(Key.Key2, "CrossBow")},
            {"3BButton", new BSIButton()},
            {"4BButton", new BSIButton()},
            {"5BButton", new BSIButton()},
            {"6BButton", new BSIButton()},
            {"7BButton", new BSIButton()},
        };
    }
}


