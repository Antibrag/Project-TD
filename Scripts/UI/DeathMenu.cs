using Data;
using Godot;

namespace UI
{
	public partial class DeathMenu : Control
	{
		public override void _Ready()
		{
			GetNode<Button>("RestartButton").Disabled = true;
			GetNode<Button>("MenuButton").Disabled = true;
		}

		public async void Enable(float side_speed = 0.01f)
		{
			Show();

			for (float i = Modulate.A; i <= 1; i += 0.01f)
			{
				Modulate = new Color(Modulate, i);
				await ToSignal(GetTree().CreateTimer(side_speed), "timeout");
			}

			GetNode<Button>("RestartButton").Disabled = false;
			GetNode<Button>("MenuButton").Disabled = false;
		}

		public async void Disable(float side_speed = 0.01f)
		{
			for (float i = Modulate.A; i >= 1; i -= 0.01f)
			{
				Modulate = new Color(Modulate, i);
				await ToSignal(GetTree().CreateTimer(side_speed), "timeout");
			}

			GetNode<Button>("RestartButton").Disabled = true;
			GetNode<Button>("MenuButton").Disabled = true;


			Hide();
		}

		public void OnRestartButtonPressed()
		{
			GetNode<LevelObjects.Spawner>(GetParent().GetPath() + "/Level/Objects/Spawner").StartSpawn(2);
			GetNode<Player>(GetParent().GetPath() + "/Level/Player").Health = Storage.LevelsList[Storage.CurrentLevel].StartPlayerHealth;
			Disable();
		}

		/*public void OnMenuButtonPressed()
		{

		}*/
	}
}

