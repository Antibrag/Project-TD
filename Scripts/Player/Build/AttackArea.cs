using Godot;

public partial class AttackArea : Area3D
{
    private Build _build;

    public override void _Ready()
    {
        _build = (Build)GetParent();
    }

    public void OnAreaEntered(Area3D enteredArea)
    {
        if (enteredArea.IsInGroup("Mob"))
            ((Build)GetParent()).AddMobInTargetList(enteredArea);
    }

    public void OnAreaExited(Area3D exitedArea)
    {
        if (exitedArea.IsInGroup("Mob"))
        {
            LevelObjects.Mob mob = (LevelObjects.Mob)exitedArea.GetParent();

            if (mob.Characteristics.Health == 0)
                return;

            _build.NextTarget(mob);
            mob.DelBuildFromList(_build);
        }
    }

    public void ChangeColor(Color newColor)
    {
        StandardMaterial3D attackRadiusMaterial = GetNode<MeshInstance3D>("AreaMesh").Mesh.SurfaceGetMaterial(0) as StandardMaterial3D;
        attackRadiusMaterial.AlbedoColor = newColor;
    }
}

