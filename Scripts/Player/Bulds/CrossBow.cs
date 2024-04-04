using Data;
using Godot;
using Godot.Collections;

namespace Builds
{
	public partial class CrossBow : Area3D
	{
		[Export] public Timer AttackCDTimer;
		[Export] public MeshInstance3D TargetIndicator;

		public Storage.Build Characteristics { get; set; }

		private System.Collections.Generic.List<Level.Mob> TargetsList { get; set; } = new();
		private Level.Mob Target { get; set; } = null;

		private bool IsPossibilityPlace { get; set; }
		private bool IsPlaced { get; set; }

		private void Initialize()
		{
			Characteristics = (Storage.Build) Storage.BuildsList["CrossBow"].Clone();
			
			IsPlaced = false;
			GetNode<CollisionShape3D>("AttackRadius").Disabled = true;
		}

		private void NextTarget()
		{
			if (TargetsList.Count == 1)
			{
				Target = null;
				TargetIndicator.GlobalPosition = Vector3.Zero;
				return;
			}

			TargetsList.Remove(Target);
			Target = TargetsList[0];
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
			if (!IsPlaced)
				IsPossibilityPlace = false;
			else
			{
				if (enteredArea.IsInGroup("Mob"))
				{
					Level.Mob mob = (Level.Mob) enteredArea.GetParent(); 

					if (!TargetsList.Contains(mob))
						TargetsList.Add(mob);	

					if (TargetsList.Count == 1)
						Target = mob;	
				}
			}
		}
			
		public void OnAreaExited(Area3D exitedArea) 
		{
			if (!IsPlaced)
			{
				IsPossibilityPlace = true;
				return;
			}

			NextTarget();
		}


		public override void _Ready()
			=> Initialize();

		public override void _Input(InputEvent @ev)
		{
			if (ev.IsActionPressed("PastBuild") && !IsPlaced && IsPossibilityPlace)
			{
				IsPlaced = true;

				GetNode<CollisionShape3D>("AttackRadius").Hide();
				GetNode<CollisionShape3D>("AttackRadius").Disabled = false;

				GD.Print(Characteristics.Name + " - has been placed");
			}
		}

		public override void _Process(double delta)
		{
			if (!IsPlaced)
				MoveToMouse();
				
			if (IsPlaced && Target != null && Target.Characteristics.Health <= 0)
				NextTarget();
			else if (IsPlaced && Target != null)
				TargetIndicator.GlobalPosition = new Vector3(Target.GlobalPosition.X, Target.GlobalPosition.Y+1, Target.GlobalPosition.Z);
		}
	}
}
