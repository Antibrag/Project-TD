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
			for (int i = 0; i < Storage.MobsList.Length; i++)
				if (Storage.MobsList[i].Name == name)
					Characteristics = Storage.MobsList[i];
		}

		public void Death()
		{
			GD.Print($"Delete mob - {Characteristics.Name}");
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

