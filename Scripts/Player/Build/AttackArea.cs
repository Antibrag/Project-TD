using Godot;

public partial class AttackArea : Area3D
{
    public void OnAttackAreaBodyEntered(Node3D enteredBody)
    {
        if (enteredBody.IsInGroup("Mob"))
            ((Build)GetParent()).AddMobInTargetList(enteredBody);
    }

    public void OnAttackAreaBodyExited(Node3D exitedBody)
    {
        if (exitedBody.IsInGroup("Mob"))
            ((Build)GetParent()).NextTarget((LevelObjects.Mob)exitedBody.GetParent());
    }
}

