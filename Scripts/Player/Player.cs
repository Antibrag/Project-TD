using Data;
using Godot;

public partial class Player : CharacterBody3D
{
    public float Health { get; set; }
    public float Mana { get; set; }
    public int Level { get; set; }

    public override void _Ready()
    {
        Health = Storage.LevelsList[Storage.CurrentLevel].StartPlayerHealth;
        Mana = 100;
    }

    public void SpendMana(float manaCost)
    {
        GetNode<UI.HUD>("/root/Main/HUD").AddDecreasingMana(manaCost);
        Mana -= manaCost;
    }

    public void TakeDamage(float damageValue)
    {
        GetNode<UI.HUD>("/root/Main/HUD").AddDecreasingHealth(damageValue);
        Health -= damageValue;

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

    public void SelectBuild(string buildname)
    {
        Build build = (Build)GD.Load<PackedScene>("res://Scenes/Build/Build.tscn").Instantiate();
        GetNode<Node>("Objects").AddChild(build);

        build.Initialize(buildname);
    }
}
