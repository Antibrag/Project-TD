namespace Data
{
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
}