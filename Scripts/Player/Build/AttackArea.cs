using Godot;

public partial class AttackArea : Area3D
{
    public void OnAreaEntered(Area3D enteredArea)
    {
        if (enteredArea.IsInGroup("Mob"))
            ((Build)GetParent()).AddMobInTargetList(enteredArea);
    }

    public void OnAreaExited(Area3D exitedArea)
    {
        if (exitedArea.IsInGroup("Mob"))
            ((Build)GetParent()).NextTarget((LevelObjects.Mob)exitedArea.GetParent());
    }

    public void ChangeColor(Color newColor)
    {
        StandardMaterial3D attackRadiusMaterial = GetNode<MeshInstance3D>("AreaMesh").Mesh.SurfaceGetMaterial(0) as StandardMaterial3D;
        attackRadiusMaterial.AlbedoColor = newColor;
    }
}

