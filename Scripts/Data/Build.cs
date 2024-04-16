using System.Collections.Generic;

namespace Data
{
    public class Build : GameObject, IClonable
    {
        public float AttackSpeed { get; set; }
        public Dictionary<string, Projectile> Projectiles { get; set; } 

        public Build(in MeshObject mesh, int level, float attackSpeed, Dictionary<string, Projectile> buildProjectilesList)
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