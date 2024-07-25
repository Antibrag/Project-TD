using Data;
using Godot;
using Godot.Collections;

public partial class Build : Area3D
{
    public Data.Build Characteristics { get; set; }
    private Data.Projectile _selectedProjectile { get; set; }

    [Export] public MeshInstance3D Head;
    [Export] public Timer AttackCDTimer;
    [Export] public AttackArea AttackArea;

    private enum States { SELECTED, PASTED }
    private States _state = States.SELECTED;

    private System.Collections.Generic.List<LevelObjects.Mob> _targetsList = new();
    private int _enteredAreasCount = 0;

    private void StatesControll()
    {
        switch (_state)
        {
            case States.SELECTED:
                Vector3 MousePosition = ScreenPointToRay();
                Position = new Vector3
                (
                    MousePosition.X,
                    Position.Y,
                    MousePosition.Z
                );

                if (Input.IsActionJustPressed("PastBuild"))
                {
                    if (_enteredAreasCount > 0)
                        return;
                    
                    Player player = GetNode<Player>("/root/Main/Level/Player");

                    if (player.Mana < Characteristics.ManaCost)
                        return;
                    
                    player.SpendMana(Characteristics.ManaCost);
                    GetNode<Area3D>("AttackArea").Hide();
                    GetNode<CollisionShape3D>("AttackArea/AreaCollision").Disabled = false;
                    
                    _state = States.PASTED;
                    GD.Print("Change staate to PASTED");
                    AttackCDTimer.Start();
                }
                break;
            
            case States.PASTED:
                if (_targetsList.Count <= 0)
                    return;
                
                Head.LookAt(_targetsList[0].GlobalPosition);
                Head.RotationDegrees = new Vector3(0, Head.RotationDegrees.Y + 90, 0);
                break;
        }
    }

    private void Shoot()
    {
        Projectile projectile_instance = (Projectile)GD.Load<PackedScene>("res://Scenes/Build/Projectile.tscn").Instantiate();
        AddChild(projectile_instance);

        projectile_instance.Initialize(_selectedProjectile, _targetsList[0]);
    }

    private Vector3 ScreenPointToRay()
    {
        Vector2 mousePosition = GetViewport().GetMousePosition();
        Camera3D camera = GetTree().Root.GetCamera3D();

        Vector3 rayOrigin = camera.ProjectRayOrigin(mousePosition);
        Vector3 rayEnd = rayOrigin + camera.ProjectRayNormal(mousePosition) * 1000;

        var query = PhysicsRayQueryParameters3D.Create(rayOrigin, rayEnd);
        query.CollisionMask = 16;
        query.CollideWithAreas = true;
        query.CollideWithBodies = false;

        Dictionary rayArray = GetWorld3D().DirectSpaceState.IntersectRay(query);

        if (!rayArray.ContainsKey("position"))
            return Vector3.Zero;

        return (Vector3)rayArray["position"];
    }

    public void Initialize(string name)
    {
        Characteristics = (Data.Build)Storage.BuildsList[name].Clone();

        MeshInstance3D head_mesh = GetNode<MeshInstance3D>("HeadMesh");
        head_mesh.Mesh = GD.Load<Mesh>($"res://Assets/Meshes/Builds/{name}_Head.res");
        head_mesh.Position = Characteristics.Mesh.HeadPosition;

        MeshInstance3D body_mesh = GetNode<MeshInstance3D>("BodyMesh");
        body_mesh.Mesh = GD.Load<Mesh>($"res://Assets/Meshes/Builds/{name}_Body.res");
        body_mesh.Position = Characteristics.Mesh.BodyPosition;

        TorusMesh attackAreaMesh = (TorusMesh)GetNode<MeshInstance3D>("AttackArea/AreaMesh").Mesh;
        attackAreaMesh.InnerRadius = Characteristics.AttackAreaRadius + 0.05f;
        attackAreaMesh.OuterRadius = Characteristics.AttackAreaRadius;

        ((CylinderShape3D)GetNode<CollisionShape3D>("AttackArea/AreaCollision").Shape).Radius = Characteristics.AttackAreaRadius;

        GD.Print($"AttackSpeed = {Characteristics.AttackSpeed}");
        AttackCDTimer.WaitTime = 1 - Characteristics.AttackSpeed / 100;

        _selectedProjectile = Characteristics.Projectiles["Wood Arrow"];
    }

    public void NextTarget(LevelObjects.Mob mob)
    {
        _targetsList.Remove(mob);
    }

    public void AddMobInTargetList(Node3D mobBody)
    {
        LevelObjects.Mob mob = (LevelObjects.Mob)mobBody.GetParent();

        if (!_targetsList.Contains(mob))
            _targetsList.Add(mob);

        mob.AttackingBuild.Add(this);
    }

    public void OnAreaEntered(Area3D enteredArea)
    {
        if (_state == States.SELECTED)
        {
            _enteredAreasCount++;
            AttackArea.ChangeColor(new Color(255, 0, 0));
        }
    }

    public void OnAreaExited(Area3D exitedArea)
    {
        if (_state == States.SELECTED)
        {
            _enteredAreasCount--;

            if (_enteredAreasCount == 0)
                AttackArea.ChangeColor(new Color(255, 255, 255));
        }
    }

    public void OnBodyEntered(Node3D enteredBody)
    {
        if (_state == States.SELECTED)
        {
            _enteredAreasCount++;
            AttackArea.ChangeColor(new Color(255, 0, 0));
        }
    }

    public void OnBodyExited(Node3D exitedBody)
    {
        if (_state == States.SELECTED)
        {
            _enteredAreasCount--;

            if (_enteredAreasCount == 0)
                AttackArea.ChangeColor(new Color(255, 255, 255));
        }
    }

    public void OnAttackCDTimerTimeout()
    {
        if (_targetsList.Count > 0)
            Shoot();
    }

    public override void _Process(double delta)
    {
        StatesControll();
    }
}