using Godot;

namespace Level
{
	public partial class Mob : PathFollow3D
	{
		public string GameName { get; private set; }
		public float Health { get; private set; }
		public float AttackPower { get; private set; }

		[Export]
		public float Speed { get; private set; }

		public void Initialize(string name, float health, float attackPower)
		{
			GameName = name;
			Health = health;
			AttackPower = attackPower;
		}

		public void Death()
		{
			GD.Print($"Delete mob - {GameName}");
			QueueFree();
		}

		public void OnBodyEntered(Node3D node)
		{
			if (node.Name == "Player")
			{	
				Player player = GetNode<Player>(node.GetPath());

				GD.Print($"* {GameName} deals damage to player - {AttackPower}");

				GetNode<Player>(node.GetPath()).TakeDamage(AttackPower);

				Death();
			}
		}

		public override void _PhysicsProcess(double delta)
		{
			ProgressRatio += Speed / 100 * (float) delta;
		}
	}	
}

