using Godot;

public partial class Projectile : RigidBody3D
{
	public Data.Projectile Characteristics { get; set; }
	private LevelObjects.Mob _target;

	public void Initialize(Data.Projectile characteristics, LevelObjects.Mob target)
	{
		Characteristics = characteristics;
		_target = target;
	}

    public override void _Process(double delta)
    {
		if (IsInstanceValid(_target))
		{
			LookAt(_target.GlobalPosition);
			GlobalPosition = GlobalPosition.MoveToward(_target.GlobalPosition, (float) delta * Characteristics.FlightSpeed);
		}
		else
			QueueFree(); 
    }
}
