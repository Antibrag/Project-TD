using Godot;

public partial class Mob : PathFollow3D
{
	public string GameName { get; set; }
	public float Health { get; set; }

	[Export]
	public float Speed { get; set; }

    public void Initialize(string name, float health)
	{
		GameName = name;
		Health = health;
	}

    public override void _Process(double delta)
    {
        ProgressRatio += Speed * (float) delta;
    }
}
