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
		//В будущем таймер будет брать список мобов на уровне из метаданных уровня
		//Список копируется в локальный список спавнера
		//Спавнер будет брать рандомного моба и инстанциировать его
		//Если количество данного моба уже закончилось то он вычеркивается из локального списка
	}
}
