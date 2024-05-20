using Godot;

namespace LevelObjects
{
    public partial class Mob : PathFollow3D
    {
        [Export] public float Speed { get; private set; }
        [Export] public ProgressBar HealthBar { get; private set; }

        public Data.Mob Characteristics { get; set; }
        public Build AttackingBuild { get; set; } = null;

        public void Initialize(string name)
        {
            Characteristics = (Data.Mob)Data.Storage.MobsList[name].Clone();

            MeshInstance3D mob_mesh = GetNode<MeshInstance3D>("Area3D/Mesh");

            mob_mesh.Mesh = GD.Load<Mesh>($"res://Assets/Meshes/Mobs/{name}.res");

            HealthBar.MaxValue = Characteristics.Health;
            HealthBar.Value = Characteristics.Health;
        }

        public void OnBodyEntered(Node3D node)
        {
            if (node.Name == nameof(Player))
            {
                GetNode<Player>(node.GetPath()).TakeDamage(Characteristics.AttackPower);
                Death();
            }
            else if (node.Name == nameof(Data.Projectile))
            {
                Projectile projectile = GetNode<Projectile>(node.GetPath());
                TakeDamage(projectile.Characteristics.AttackPower, projectile.Characteristics.PenetrationPower);
                projectile.QueueFree();
            }
        }

        public void TakeDamage(float damageValue, float penetrationPower)
        {
            Characteristics.Health -= damageValue * (100 - Characteristics.ProtectionPercentage + penetrationPower) / 100;
            HealthBar.Value = Characteristics.Health;

            if (Characteristics.Health <= 0)
                Death();
        }

        public void Death()
        {
            Characteristics.Health = 0;

            if (AttackingBuild != null)
                AttackingBuild.NextTarget(this);

            QueueFree();
        }

        public override void _PhysicsProcess(double delta)
        {
            Progress += (float)delta * Speed;
        }
    }
}


