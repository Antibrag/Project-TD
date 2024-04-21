namespace Data
{
    public class Mob : GameObject, IClonable
    {
        public float Health { get; set; }
        public float AttackPower { get; private set; }
        public float ProtectionPercentage { get; private set; }

        public Mob(in MeshObject mesh, float health, float attackPower, float protectionPercentage)
        {
            Mesh = mesh;
            Health = health;
            AttackPower = attackPower;
            ProtectionPercentage = protectionPercentage;
        }

        public object Clone() 
            => new Mob(Mesh, Health, AttackPower, ProtectionPercentage);
            
    }    
}