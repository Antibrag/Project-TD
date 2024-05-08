using Data;
using Godot;

namespace UI
{
	public partial class BSIButton : TextureButton
	{
		public string ButtonName { get; set; }
		public string BSIName { get; set; } //Build-Spell_Item name 

		public void Initialize(int actionIndex, string buttonType)
		{
			Disabled = false;

			switch (buttonType)
			{
				case nameof(Build):
					ButtonName = $"{actionIndex+1}BButton";
				break;

				//case nameof(Spell):
				//	ButtonName = $"{actionIndex}SButton";
				//break;

				//case nameof(Item):
				//	ButtonName = $"{actionIndex}IButton";
				//break;
			}

			Data.BSIButton buttonConfiguration = Storage.BuildButtonsList[ButtonName];

			InputEventKey inputEventKey = new() { Keycode = buttonConfiguration.ShortcutKey };
			Shortcut.Events.Add(inputEventKey);

			BSIName = buttonConfiguration.BSIName;
			GetNode<Label>("KeyBind").Text = buttonConfiguration.ShortcutKey.ToString();

			TextureNormal = GD.Load<CompressedTexture2D>(buttonConfiguration.ButtonTexturePath);

		}

		public void OnPressed()
		{
			GD.Print($"Button({Name}) Pressed, BSIName = {BSIName}");
			GetNode<Player>("/root/Main/Level/Player").PastBuild(BSIName);
		}
	}	
}

