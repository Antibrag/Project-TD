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
                    ButtonName = $"{actionIndex + 1}BButton";
                    break;

                    //case nameof(Spell):
                    //	ButtonName = $"{actionIndex}SButton";
                    //break;

                    //case nameof(Item):
                    //	ButtonName = $"{actionIndex}IButton";
                    //break;
            }

            Data.BSIButton buttonConfiguration = Storage.BuildButtonsList[ButtonName];

            Shortcut = new();
            Shortcut.Events = (Godot.Collections.Array)new Godot.Collections.Array();
            Shortcut.Events.Add(new InputEventKey() { Keycode = buttonConfiguration.ShortcutKey });

            BSIName = buttonConfiguration.BSIName;
            GetNode<Label>("KeyBind").Text = buttonConfiguration.ShortcutKey.ToString();

            TextureNormal = GD.Load<CompressedTexture2D>(buttonConfiguration.ButtonTexturePath);

            GD.Print($"Initialize button({ButtonName}, shortcut - {buttonConfiguration.ShortcutKey})");
        }

        public void OnPressed()
        {
            GD.Print($"Button({Name}) Pressed, ShortCut key = {Shortcut.Events[0].ToString()}, eviri");
            GetNode<Player>("/root/Main/Level/Player").PastBuild(BSIName);
        }
    }
}

