using Godot;
using Godot.Collections;

public partial class Spawner : Area3D
{
	private Dictionary<string, int> _levelMobs;
	private Timer _spawnTimer;

    public override void _Ready()
    {
        _levelMobs = (Dictionary<string, int>) GetNode<Node3D>("/root/Main/Level").GetMeta("MobsCounts");
		_spawnTimer = GetNode<Timer>("SpawnTimer");
    }

    public void StartSpawn(float spawnTime)
	{
		_spawnTimer.WaitTime = spawnTime;
		_spawnTimer.Start();
	}

	public void OnSpawnTimerTimeout()
	{
		if (_levelMobs.Count == 0)
		{
			_spawnTimer.Stop();
			return;
		}

		int random_mob_index = new System.Random().Next(0, _levelMobs.Count);
		string mob_name = Data.MobsList[random_mob_index].Name;

		if (_levelMobs[mob_name] == 0)
		{
			_levelMobs.Remove(mob_name);
			return;
		}
		else
			_levelMobs[mob_name]--;

		Mob mob_instance = (Mob) GD.Load<PackedScene>(Data.MobsList[random_mob_index].ScenePath).Instantiate();
		GetNode<Path3D>(GetParent().GetPath() + "/Path3D").CallDeferred("add_child", mob_instance);

		mob_instance.Initialize(mob_name, Data.MobsList[random_mob_index].Health);

		GD.Print($"Create Mob - {Data.MobsList[random_mob_index].Name}");
	}
}
