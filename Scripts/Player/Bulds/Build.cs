using Data;
using Godot;
using Godot.Collections;

public partial class Build : Area3D
{
	public Storage.Build Characteristics { get; set; }
	private bool _isPlaced { get; set; } = false;

	[Export] public Timer AttackCDTimer;
	[Export] public MeshInstance3D TargetIndicator;

	private System.Collections.Generic.List<Level.Mob> _targetsList = new();
	private Level.Mob _target = null;
	private bool _isPossibilityPlace;

	private void Initialize()
	{
		Characteristics = (Storage.Build) Storage.BuildsList["CrossBow"].Clone();

		GetNode<CollisionShape3D>("AttackRadius").Disabled = true;
		
		MeshInstance3D build_mesh = GetNode<MeshInstance3D>("Mesh");
		build_mesh.Mesh = GD.Load<Mesh>(Characteristics.Mesh.MeshPath);
		build_mesh.Scale = Characteristics.Mesh.Scale;
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

	private Vector3 ScreenPointToRay()
	{
		Vector2 mousePosition = GetViewport().GetMousePosition();
		Camera3D camera = GetTree().Root.GetCamera3D();

		Vector3 rayOrigin = camera.ProjectRayOrigin(mousePosition);
		Vector3 rayEnd = rayOrigin + camera.ProjectRayNormal(mousePosition) * 2000;

		var query = PhysicsRayQueryParameters3D.Create(rayOrigin, rayEnd);
		query.CollideWithAreas = true;
		query.CollideWithBodies = false;

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
			Level.Mob mob = (Level.Mob) enteredArea.GetParent();

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


	public override void _Ready()
		=> Initialize();

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
		if (!_isPlaced)
			MoveToMouse();

		if (_isPlaced && _target != null && _target.Characteristics.Health <= 0)
			NextTarget();
		else if (_isPlaced && _target != null)
			TargetIndicator.GlobalPosition = new Vector3(_target.GlobalPosition.X, _target.GlobalPosition.Y + 1, _target.GlobalPosition.Z);
	}
}

