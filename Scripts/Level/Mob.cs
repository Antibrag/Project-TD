using Data;
using Godot;

namespace LevelObjects
{
	public partial class Mob : PathFollow3D
	{
		[Export] public float Speed { get; private set; }

		public Data.Mob Characteristics { get; set; }

		public void Initialize(string name) 
		{
			Characteristics = (Data.Mob) Storage.MobsList[name].Clone();

			MeshInstance3D mob_mesh = GetNode<MeshInstance3D>("Area3D/Mesh");
			mob_mesh.Mesh = GD.Load<Mesh>(Characteristics.Mesh.MeshPath);
			mob_mesh.Scale = Characteristics.Mesh.Scale;
		}

		public void Death()
		{
			Characteristics.Health = 0;
			QueueFree();
		}

		public void OnBodyEntered(Node3D node)
		{
			if (node.Name == "Player")
			{	
				Player player = GetNode<Player>(node.GetPath());

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

