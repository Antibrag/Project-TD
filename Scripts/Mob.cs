using Godot;
using System;

public partial class Mob : Area3D
{
	public string MobName { get; set; }
	public float Health { get; set; }

	public Mob(string name, float health)
	{
		MobName = name;
		Health = health;
	}

	public override void _Process(double delta)
	{
	}
}
