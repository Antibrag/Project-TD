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
            {"Wood Arrow", new Projectile(new MeshObject("", new Vector3()), 1, 20, 0.5f, 20)}
        };

        public static readonly Dictionary<string, Mob> MobsList = new Dictionary<string, Mob>()
        {
            //Mob name, Mob health, Mob attack power, mob scene path
            {"DevMob", new Mob(new MeshObject("res://Assets/Meshes/DevMob.res", new Vector3(40, 40, 40)), 100, 10, 1)}
        };

        public static readonly Dictionary<string, Level> LevelsList = new Dictionary<string, Level>()
        {
            //Level name, Level path, Player start health
            {"Dev Level", new Level("res://Scenes/Levels/Debug_Level.tscn", 100) }
        };

        public static readonly Dictionary<string, Build> BuildsList = new Dictionary<string, Build>()
        {
            {"CrossBow", new Build(
                new MeshObject("res://Assets/Meshes/Builds/CrossBow.res", new Vector3(80, 80, 80)), 
                1, 
                1,
                new Dictionary<string, Projectile>() { {"Wood Arrow", ProjectilesList["Wood Arrow"]} } 
            )}
        };
    }
}


