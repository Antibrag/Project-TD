using Godot;

public partial class Player : CharacterBody3D
{
    public float Health { get; set; }

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
        GetNode<Level.Objects.Spawner>(GetParent().GetPath() + "/Objects/Spawner").QueueFreeAllMobs();
        GetNode<DeathMenu>("/root/Main/Death_Menu").Enable();
    }
}
