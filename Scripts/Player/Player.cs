using Godot;

public partial class Player : CharacterBody3D
{
    public static class GlobalInfo
    {
        public static int Level { get; set; }
    }

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
        GetNode<Spawner>(GetParent().GetPath() + "/Objects/Spawner").QueueFreeAllMobs();
        GetNode<DeathMenu>("/root/Main/Death_Menu").Enable();
    }

    public override void _Ready()
    {
        Health = 20;
    }
}
