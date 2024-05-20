using System.Collections.Generic;

namespace Data
{
    public class Build : IClonable
    {
        public BuildMesh Mesh { get; set; }
        public int Level { get; set; }
        public float AttackSpeed { get; set; }
        public int AttackAreaRadius { get; set; }
        public Dictionary<string, Projectile> Projectiles { get; set; }

        public Build(in BuildMesh mesh, int level, float attackSpeed, int attackAreaRadius, Dictionary<string, Projectile> buildProjectilesList)
        {
            Mesh = mesh;
            Level = level;
            AttackSpeed = attackSpeed;
            AttackAreaRadius = attackAreaRadius;
            Projectiles = buildProjectilesList;
        }

        public object Clone()
            => new Build(Mesh, Level, AttackSpeed, AttackAreaRadius, Projectiles);
    }
}
