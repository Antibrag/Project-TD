namespace Data
{
    public class Mob : IClonable
    {
        public float Health { get; set; }
        public float AttackPower { get; private set; }
        public float ProtectionPercentage { get; private set; }

        public Mob(float health, float attackPower, float protectionPercentage)
        {
            Health = health;
            AttackPower = attackPower;
            ProtectionPercentage = protectionPercentage;
        }

        public object Clone() 
            => new Mob(Health, AttackPower, ProtectionPercentage);
            
    }    
}