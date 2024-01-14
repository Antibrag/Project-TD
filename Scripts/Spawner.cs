using Godot;
using Godot.Collections;

public partial class Spawner : Area3D
{
	private Dictionary<string, int> LevelMobs { get; set; }

    public override void _Ready()
    {
        LevelMobs = (Dictionary<string, int>) GetNode<Node3D>("/root/Main/Level").GetMeta("MobsCounts");
    }

    public void StartSpawn(float spawnTime)
	{
		GetNode<Timer>("SpawnTimer").WaitTime = spawnTime;
		GetNode<Timer>("SpawnTimer").Start();
	}

	public void OnSpawnTimerTimeout()
	{
		int random_mob_index = new System.Random().Next(0, Data.MobsList.Length);
		string mob_name = Data.MobsList[random_mob_index].Name;

		if (LevelMobs[mob_name] == 0)
			LevelMobs.Remove(mob_name);
		else
			LevelMobs[mob_name]--;

		Mob mob_instance = (Mob) GD.Load<PackedScene>(Data.MobsList[random_mob_index].ScenePath).Instantiate();
		GetParent().CallDeferred("add_child", mob_instance);

		mob_instance.Initialize(mob_name, Data.MobsList[random_mob_index].Health, Position);

		GD.Print($"Create Mob - {Data.MobsList[random_mob_index].Name}");
	}
}
