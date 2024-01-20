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
    }

    public override void _Ready()
    {
        Health = 100;
    }
}
