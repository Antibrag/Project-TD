using Godot;

namespace Data
{
    public class BSIButton
    {
        public Key ShortcutKey { get; set; }
        public string BSIName { get; set; } //Build-Spell-Item name
        public string ButtonTexturePath { get; set; }

        public static readonly string DefaultButtonTexturePath = "res://Assets/Textures/Skill-Build background button.png";

        public BSIButton()
        {
            ShortcutKey = Key.None;
            BSIName = "";
            ButtonTexturePath = DefaultButtonTexturePath;
        }

        public BSIButton(Key shortcut, string BSIName, string buttonTexturePath)
        {
            ShortcutKey = shortcut;
            this.BSIName = BSIName;
            ButtonTexturePath = buttonTexturePath;
        }
    }
}
