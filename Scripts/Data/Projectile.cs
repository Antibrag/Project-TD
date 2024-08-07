namespace Data
{
    public class Projectile : IClonable
    {
        public float AttackPower { get; set; }
        public int Level { get; set; }
        public float PenetrationPower { get; set; } //Пробивная способность
        public float FlightSpeed { get; set; }

        private void SyncPropertiesWithLevel()
        {
            AttackPower += 2 * Level;
            PenetrationPower += 0.1f * Level;
        }

        public Projectile(int level, float attackPower, float penetrationPower, float flightSpeed)
        {
            Level = level;
            AttackPower = attackPower;
            PenetrationPower = penetrationPower;
            FlightSpeed = flightSpeed;
        }

        public object Clone()
            => new Projectile(Level, AttackPower, PenetrationPower, FlightSpeed);
    }
}
