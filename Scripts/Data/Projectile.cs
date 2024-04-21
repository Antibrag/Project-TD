namespace Data
{
    public class Projectile : GameObject, IClonable
    {
        public float AttackPower { get; set; }
        public float PenetrationPower { get; set; } //Пробивная способность
        public float FlightSpeed { get; set;}

        public Projectile(in MeshObject mesh, int level, float attackPower, float penetrationPower, float flightSpeed)
        {
            Mesh = mesh;
            Level = level;
            AttackPower = attackPower;
            PenetrationPower = penetrationPower;
            FlightSpeed = flightSpeed;
        }

        public object Clone() 
            => new Projectile(Mesh, Level, AttackPower, PenetrationPower, FlightSpeed);
    }
}