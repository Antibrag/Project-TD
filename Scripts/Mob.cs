using Godot;

public partial class Mob : Area3D
{
	public string GameName { get; set; }
	public float Health { get; set; }

    public void Initialize(string name, float health, Vector3 position)
	{
		GameName = name;
		Health = health;
		Position = position;
	}
}
