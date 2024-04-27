using Data;
using Godot;
using Godot.Collections;

public partial class Build : Area3D
{
	public Data.Build Characteristics { get; set; }
	private Data.Projectile SelectedProjectile { get; set; }
	private bool _isPlaced { get; set; } = false;

	[Export] public MeshInstance3D Head;
	[Export] public Timer AttackCDTimer;
	[Export] public MeshInstance3D TargetIndicator;

	private System.Collections.Generic.List<LevelObjects.Mob> _targetsList = new();
	private LevelObjects.Mob _target = null;
	private bool _isPossibilityPlace = true;

	private void Initialize(string name)
	{
		Characteristics = (Data.Build) Storage.BuildsList[name].Clone();

		//GetNode<CollisionShape3D>("AttackRadius").Disabled = true;
		
		MeshInstance3D head_mesh = GetNode<MeshInstance3D>("HeadMesh");
		head_mesh.Mesh = GD.Load<Mesh>($"res://Assets/Meshes/Builds/{name}_Head.res");
		head_mesh.Position = Characteristics.Mesh.HeadPosition;

		MeshInstance3D body_mesh = GetNode<MeshInstance3D>("BodyMesh");
		body_mesh.Mesh = GD.Load<Mesh>($"res://Assets/Meshes/Builds/{name}_Body.res");
		body_mesh.Position = Characteristics.Mesh.BodyPosition;

		SelectedProjectile = Characteristics.Projectiles["Wood Arrow"];

		AttackCDTimer.Start();
	}

	private void NextTarget()
	{
		if (_targetsList.Count == 1)
		{
			_target = null;
			TargetIndicator.GlobalPosition = Vector3.Zero;
			return;
		}

		_targetsList.Remove(_target);
		_target = _targetsList[0];
	}

	private void Shoot()
	{
		Projectile projectile_instance = (Projectile) GD.Load<PackedScene>("res://Scenes/Projectile.tscn").Instantiate();
		AddChild(projectile_instance);

		projectile_instance.Initialize(SelectedProjectile, _target);
	}

	private Vector3 ScreenPointToRay()
	{
		Vector2 mousePosition = GetViewport().GetMousePosition();
		Camera3D camera = GetTree().Root.GetCamera3D();

		Vector3 rayOrigin = camera.ProjectRayOrigin(mousePosition);
		Vector3 rayEnd = rayOrigin + camera.ProjectRayNormal(mousePosition) * 1000;

		var query = PhysicsRayQueryParameters3D.Create(rayOrigin, rayEnd);
		query.CollideWithAreas = true;

		Dictionary rayArray = GetWorld3D().DirectSpaceState.IntersectRay(query);

		if (!rayArray.ContainsKey("position"))
			return Vector3.Zero;

		return (Vector3) rayArray["position"];
	}

	private void MoveToMouse()
	{
		Vector3 MousePosition = ScreenPointToRay();

		Position = new Vector3
		(
			MousePosition != Vector3.Zero ? MousePosition.X : Position.X,
			Position.Y,
			MousePosition != Vector3.Zero ? MousePosition.Z : Position.Z
		);
	}

	public void OnAreaEntered(Area3D enteredArea)
	{
		if (!_isPlaced)
		{
			_isPossibilityPlace = false;
			return;
		}

		if (enteredArea.IsInGroup("Mob"))
		{
			LevelObjects.Mob mob = (LevelObjects.Mob) enteredArea.GetParent();

			if (!_targetsList.Contains(mob))
				_targetsList.Add(mob);

			if (_targetsList.Count == 1)
				_target = mob;
		}		
	}

	public void OnAreaExited(Area3D exitedArea)
	{
		if (!_isPlaced)
		{
			_isPossibilityPlace = true;
			return;
		}

		NextTarget();
	}

	public void OnAttackCDTimerTimeout()
	{
		if (_target != null)
			Shoot();
	}

	public override void _Ready()
		=> Initialize("CrossBow"); //Заменить хард-кодное объявление постройки и снаряда, на выбор во время игры 

	public override void _Input(InputEvent @ev)
	{
		if (ev.IsActionPressed("PastBuild") && !_isPlaced && _isPossibilityPlace)
		{
			_isPlaced = true;
			GetNode<CollisionShape3D>("AttackRadius").Hide();
			GetNode<CollisionShape3D>("AttackRadius").Disabled = false;
		}
	}

	public override void _Process(double delta)
	{
		//NEED OPTIMIZATION!!!!
		if (!_isPlaced)
			MoveToMouse();

		if (_isPlaced && _target != null)
		{
			Head.LookAt(_target.GlobalPosition);
			Head.RotationDegrees = new Vector3(-90, Head.RotationDegrees.Y + 90, 0);

			TargetIndicator.GlobalPosition = new Vector3(_target.GlobalPosition.X, _target.GlobalPosition.Y + 1, _target.GlobalPosition.Z); 
		}
		
		if (_isPlaced && _target != null && _target.Characteristics.Health <= 0)
			NextTarget();
	}
}

