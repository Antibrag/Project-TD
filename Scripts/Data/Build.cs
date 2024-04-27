using System.Collections.Generic;

namespace Data
{
    public class Build : IClonable
    {
        public BuildMesh Mesh { get; set; }
        public int Level { get; set; }
        public float AttackSpeed { get; set; }
        public Dictionary<string, Projectile> Projectiles { get; set; } 

        public Build(in BuildMesh mesh, int level, float attackSpeed, Dictionary<string, Projectile> buildProjectilesList)
        {
            Mesh = mesh;
            Level = level;
            AttackSpeed = attackSpeed;
            Projectiles = buildProjectilesList;
        }

        public object Clone() 
            => new Build(Mesh, Level, AttackSpeed, Projectiles);
    }
}