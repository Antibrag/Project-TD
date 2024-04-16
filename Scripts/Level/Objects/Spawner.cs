using System.Linq;
using Godot;
using Godot.Collections;

using Data;

namespace LevelObjects
{
	public partial class Spawner : Area3D
	{
		private Dictionary<string, int> _levelMobs;
		private Timer _spawnTimer;

		public override void _Ready()
		{
			_spawnTimer = GetNode<Timer>("SpawnTimer");
		}

		public void StartSpawn(float spawnTime)
		{
			//GD.Print("Spawner - Start spawn");
			_levelMobs = new ((Dictionary<string, int>) GetNode<Node3D>("/root/Main/Level").GetMeta("MobsCounts"));
			_spawnTimer.WaitTime = spawnTime;
			_spawnTimer.Start();
		}

		public void QueueFreeAllMobs()
		{
			_spawnTimer.Stop();

			Node mobsParent = GetNode<Node>(GetParent().GetPath() + "/MobsPath");

			for (int i = 0; i < mobsParent.GetChildCount(); i++)
				mobsParent.GetChild<Mob>(i).Death();
		}

		public void OnSpawnTimerTimeout()
		{
			if (_levelMobs.Count == 0)
			{
				_spawnTimer.Stop();
				return;
			}

			int random_mob_index = new System.Random().Next(0, _levelMobs.Count-1);
			string mob_name = Storage.MobsList.ElementAt(random_mob_index).Key;

			if (_levelMobs[mob_name] == 0)
			{
				_levelMobs.Remove(mob_name);
				return;
			}
			else
				_levelMobs[mob_name]--;

			Mob mob_instance = (Mob) GD.Load<PackedScene>("res://Scenes/Mob.tscn").Instantiate();
			GetNode<Path3D>(GetParent().GetPath() + "/MobsPath").CallDeferred("add_child", mob_instance);

			mob_instance.Initialize(mob_name);
		}
	}
}