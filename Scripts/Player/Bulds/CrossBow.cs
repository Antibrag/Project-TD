using Data;
using Godot;
using Godot.Collections;

namespace Builds
{
	public partial class CrossBow : Area3D
	{
		[Export] public Timer AttackCDTimer;
		public Storage.Build Characteristics { get; set; }

		private Level.Mob Target { get; set; }

		private bool IsPossibilityPlace { get; set; }
		private bool IsPlaced { get; set; }

		private void Initialize()
		{
			for (int i = 0; i < Storage.BuildsList.Length; i++)
				if (Storage.BuildsList[i].Name == "CrossBow")
					Characteristics = Storage.BuildsList[i];
			
			IsPlaced = false;
			GetNode<CollisionShape3D>("AttackRadius").Disabled = true;
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

		public void OnAreaEntered(Area3D enteredArea)
		{
			if (!IsPlaced)
			{
				IsPossibilityPlace = false;
			}
			/*else
			{

			}*/
		}

		public override void _Ready()
		{
			Initialize();
		}

		public override void _Input(InputEvent @ev)
		{
			if (ev.IsActionPressed("PastBuild") && !IsPlaced && IsPossibilityPlace)
				IsPlaced = true;
		}

		public override void _Process(double delta)
		{
			if (!IsPlaced)
			{
				Vector3 MousePosition = ScreenPointToRay();

				Position = new Vector3(
					MousePosition != Vector3.Zero ? MousePosition.X : Position.X,
					Position.Y,
					MousePosition != Vector3.Zero ? MousePosition.Z : Position.Z
				);
			}
		}
	}
}
