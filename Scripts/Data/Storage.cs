using System.Collections.Generic;
using Godot;

namespace Data
{
    public static class Storage
    {
        private interface IClonable
        {
            public object Clone();
        }

        public class GameObject
        {
            //public string Name { get; set; }
            public MeshObject Mesh { get; set; }
            public int Level { get; set; }
        }

        public struct MeshObject
        {
            public string MeshPath;
            public Vector3 Scale;

            public MeshObject(string meshPath, in Vector3 scale)
            {
                MeshPath = meshPath;
                Scale = scale;
            }
        }

        public class Mob : GameObject, IClonable
        {
            public float Health { get; set; }
            public float AttackPower { get; private set; }

            public Mob(in MeshObject mesh, float health, float attackPower)
            {
                Mesh = mesh;
                Health = health;
                AttackPower = attackPower;
            }

            public object Clone() 
                => new Mob(Mesh, Health, AttackPower);
            
        }

        public class Level
        {
            public string ScenePath { get; private set; }
            public bool IsComplete { get; set; }
            public float StartPlayerHealth { get; private set; }

            public Level(string scenePath, float startPlayerHealth, bool isCompleted = false)
            {
                ScenePath = scenePath;
                IsComplete = isCompleted;
                StartPlayerHealth = startPlayerHealth;
            }
        }

        public class Build : GameObject, IClonable
        {
            public float AttackSpeed { get; set; }
            public Dictionary<string, Projectile> Projectiles { get; set; } 

            public Build(in MeshObject mesh, int level, float attackSpeed, Dictionary <string, Projectile> buildProjectilesList)
            {
                Mesh = mesh;
                Level = level;
                AttackSpeed = attackSpeed;
                Projectiles = buildProjectilesList;
            }

            public object Clone() 
                => new Build(Mesh, Level, AttackSpeed, Projectiles);
        }

        public class Projectile : GameObject, IClonable
        {
            public float AttackPower { get; set; }
            public float PenetrationPower { get; set; } //Пробивная способность

            public Projectile(in MeshObject mesh, int level, float attackPower, float penetrationPower)
            {
                Mesh = mesh;
                Level = level;
                AttackPower = attackPower;
                PenetrationPower = penetrationPower;
            }

            public object Clone() 
                => new Projectile(Mesh, Level, AttackPower, PenetrationPower);
        }

        public static readonly Dictionary<string, Projectile> ProjectilesList = new Dictionary<string, Projectile>()
        {
            {"Wood Arrow", new Projectile(new MeshObject("", new Vector3()), 1, 2, 1)}
        };

        public static readonly Dictionary<string, Mob> MobsList = new Dictionary<string, Mob>()
        {
            //Mob name, Mob health, Mob attack power, mob scene path
            {"DevMob", new Mob(new MeshObject("res://Assets/Meshes/DevMob.res", new Vector3(40, 40, 40)), 10, 10)}
        };

        public static readonly Level[] LevelsList = new Level[]
        {
            //Level name, Level path, Player start health
            new Level("res://Scenes/Levels/Debug_Level.tscn", 100)
        };

        public static readonly Dictionary<string, Build> BuildsList = new Dictionary<string, Build>()
        {
            {"CrossBow", new Build(
                new MeshObject("res://Assets/Meshes/Builds/CrossBow.res", new Vector3(100, 100, 100)), 
                1, 
                1,
                new Dictionary<string, Projectile>() { {"Wood Arrow", ProjectilesList["Wood Arrow"]} } 
            )}
        };

        public static class GlobalInfo
        {
            public static readonly string PlayerDataSavePath = "user://PlayerData.json";
            public static readonly string LevelsDataSavePath = "user://LevelsData.json";
            public static int CurrentLevelIdx;
        }
    }
}


