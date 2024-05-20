using Godot;

public partial class AttackArea : Area3D
{
    public void OnBodyEntered(Node3D enteredBody)
    {
        if (enteredBody.IsInGroup("Mob"))
            ((Build)GetParent()).AddMobInTargetList(enteredBody);
    }

    public void OnBodyExited(Node3D exitedBody)
    {
        if (exitedBody.IsInGroup("Mob"))
            ((Build)GetParent()).NextTarget((LevelObjects.Mob)exitedBody.GetParent());
    }

    public void ChangeColor(Color newColor)
    {
        StandardMaterial3D attackRadiusMaterial = GetNode<MeshInstance3D>("AreaCollision/AreaMesh").Mesh.SurfaceGetMaterial(0) as StandardMaterial3D;
        attackRadiusMaterial.AlbedoColor = newColor;
    }
}

