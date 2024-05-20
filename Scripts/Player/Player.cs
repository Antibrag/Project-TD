using Data;
using Godot;

public partial class Player : CharacterBody3D
{
    public float Health { get; set; }
    public int Level { get; set; }

    public override void _Ready()
    {
        Health = Storage.LevelsList[Storage.CurrentLevel].StartPlayerHealth;
    }

    public void TakeDamage(float damageValue)
    {
        float healthBefore = Health;
        Health -= damageValue;

        GetNode<UI.HUD>("/root/Main/HUD").AddDecreasingHealth(damageValue);

        GD.Print($"Player health - {healthBefore} -> {Health}");

        if (Health <= 0)
        {
            Death();
            return;
        }
    }

    public void Death()
    {
        GD.Print("Player death!");
        GetNode<LevelObjects.Spawner>(GetParent().GetPath() + "/Objects/Spawner").QueueFreeAllMobs();
        GetNode<UI.DeathMenu>("/root/Main/Death_Menu").Enable();
    }

    public void PastBuild(string buildname)
    {
        Build build = (Build)GD.Load<PackedScene>("res://Scenes/Player/Build/Build.tscn").Instantiate();
        GetNode<Node>("Objects").AddChild(build);

        build.Initialize(buildname);
    }
}
