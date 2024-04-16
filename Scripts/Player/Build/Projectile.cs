using Godot;

public partial class Projectile : Area3D
{
	private Data.Projectile Characteristics { get; set; }
	private Vector3 _target_position;

	public void Initialize(Data.Projectile characteristics, in Vector3 target_position)
	{
		Characteristics = characteristics;
		_target_position = target_position;
	}

    public override void _Process(double delta)
    {
        GlobalPosition.MoveToward(_target_position, (float) delta);
    }
}
