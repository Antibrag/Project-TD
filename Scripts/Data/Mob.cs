namespace Data
{
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
}