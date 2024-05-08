using Data;
using Godot;

public partial class BuildSkill_Button : TextureButton
{
	public string ButtonName { get; set; }
	public string BSIName { get; set; } //Build-Spell_Item name 

	public void Initialize(int actionIndex, string buttonType)
	{
		Disabled = false;

		switch (buttonType)
		{
			case nameof(Build):
				ButtonName = $"{actionIndex}BButton";
			break;

			//case nameof(Spell):
			//	ButtonName = $"{actionIndex}SButton";
			//break;

			//case nameof(Item):
			//	ButtonName = $"{actionIndex}IButton";
			//break;
		}

		BSButton buttonConfiguration = Storage.BuildButtonsList[ButtonName];

        InputEventKey inputEventKey = new() { Keycode = buttonConfiguration.ShortcutKey };
        Shortcut.Events.Add(inputEventKey);

		BSIName = buttonConfiguration.BSIName;

		TextureNormal = GD.Load<CompressedTexture2D>(buttonConfiguration.ButtonTexturePath);

	}
}
