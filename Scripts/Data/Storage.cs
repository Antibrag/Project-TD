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

        public struct MeshObject
        {
            public string MeshPath;
            public Vector3 Scale;

            public MeshObject(string meshPath, Vector3 scale)
            {
                MeshPath = meshPath;
                Scale = scale;
            }
        }

        public class Mob : IClonable 
        {
            public string Name { get; private set; }
            public MeshObject Mesh { get; private set; }
            public float Health { get; set; }
            public float AttackPower { get; private set; }

            public Mob(string name, string meshPath, Vector3 meshScale, float health, float attackPower)
            {
                Name = name;
                Mesh = new MeshObject(meshPath, meshScale);
                Health = health;
                AttackPower = attackPower;
            }

            public Mob(string name, MeshObject mesh, float health, float attackPower)
            {
                Name = name;
                Mesh = mesh;
                Health = health;
                AttackPower = attackPower;
            }

            public object Clone() =>     
                new Mob(Name, Mesh, Health, AttackPower);
            
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
            public MeshObject Mesh { get; private set;}
            public bool Available { get; set; }
            public int Level { get; set; }
            public float Damage { get; set; }
            public float AttackSpeed { get; set; }

            public Build(string name, string meshPath, Vector3 meshScale, int level, float damage, float attackSpeed, bool available = false)
            {
                Name = name;
                Mesh = new MeshObject(meshPath, meshScale);
                Available = available;
                Level = level;
                Damage = damage;
                AttackSpeed = attackSpeed;
            }

            public Build(string name, MeshObject mesh, int level, float damage, float attackSpeed, bool available = false)
            {
                Name = name;
                Mesh = mesh;
                Available = available;
                Level = level;
                Damage = damage;
                AttackSpeed = attackSpeed;
            }

            public object Clone() =>
                new Build(Name, Mesh, Level, Damage, AttackSpeed, Available);
        }

        public static readonly string PlayerDataSavePath = "user://PlayerData.json";
        public static readonly string LevelsDataSavePath = "user://LevelsData.json";

        public static class GlobalInfo
        {
            public static int CurrentLevelIdx { get; set; }
        }

        public static readonly Dictionary<string, Mob> MobsList = new Dictionary<string, Mob>()
        {
            //Mob name, Mob health, Mob attack power, mob scene path
            {"DevMob", new Mob("DevMob", "res://Assets/Meshes/DevMob.res", new Vector3(40, 40, 40), 10, 10)}
        };

        public static readonly Level[] LevelsList = new Level[]
        {
            //Level name, Level path, Player start health
            new Level("Dev Level", "res://Scenes/Levels/Debug_Level.tscn", 100)
        };

        public static readonly Dictionary<string, Build> BuildsList = new Dictionary<string, Build>()
        {
            {"CrossBow", new Build("CrossBow", "res://Assets/Meshes/Builds/CrossBow.res", new Vector3(100, 100, 100), 1, 10, 0.1f, true)}
        };
    }
}


