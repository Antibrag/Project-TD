using Data;
using Godot;

public partial class Player : CharacterBody3D
{
    public float Health { get; set; } 
    public int Level { get; set; }

    public override void _Ready()
    {
        Health = Storage.LevelsList[Storage.GlobalInfo.CurrentLevelIdx].StartPlayerHealth;
    }

    public void TakeDamage(float damageValue) 
    {        
        float healthBefore = Health;
        Health -= damageValue;

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
        GetNode<Level.Spawner>(GetParent().GetPath() + "/Objects/Spawner").QueueFreeAllMobs();
        GetNode<UI.DeathMenu>("/root/Main/Death_Menu").Enable();
    }
}
