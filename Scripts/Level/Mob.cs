using Data;
using Godot;

namespace Level
{
	public partial class Mob : PathFollow3D
	{
		[Export] public float Speed { get; private set; }

		public Storage.Mob Characteristics { get; set; }

		public void Initialize(string name) 
		{
			Characteristics = (Storage.Mob) Storage.MobsList[name].Clone();

			MeshInstance3D mob_mesh = GetNode<MeshInstance3D>("Area3D/Mesh");
			mob_mesh.Mesh = GD.Load<Mesh>(Characteristics.Mesh.MeshPath);
			mob_mesh.Scale = Characteristics.Mesh.Scale;
		}

		public void Death()
		{
			GD.Print($"Delete mob - {Characteristics.Name}");
			Characteristics.Health = 0;
			QueueFree();
		}

		public void OnBodyEntered(Node3D node)
		{
			if (node.Name == "Player")
			{	
				Player player = GetNode<Player>(node.GetPath());

				GD.Print($"* {Characteristics.Name} deals damage to player - {Characteristics.AttackPower}");

				GetNode<Player>(node.GetPath()).TakeDamage(Characteristics.AttackPower);

				Death();
			}
		}

        public override void _PhysicsProcess(double delta)
		{
			ProgressRatio += Speed / 100 * (float) delta;
		}
	}	
}

