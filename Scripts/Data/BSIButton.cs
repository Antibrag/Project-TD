using Godot;

namespace Data
{
    public class BSIButton
    {
        public Key ShortcutKey { get; set; }
        public string BSIName { get; set; } //Build-Spell-Item name
        public string ButtonTexturePath { get; set; }

        private const string _defaultButtonTexturePath = "res://Assets/Textures/Skill-Build background button.png";

        public BSIButton()
        {
            ShortcutKey = Key.None;
            BSIName = "none";
            ButtonTexturePath = _defaultButtonTexturePath;
        }

        public BSIButton(Key shortcut, string BSIName, string buttonTexturePath = _defaultButtonTexturePath)
        {
            ShortcutKey = shortcut;
            this.BSIName = BSIName;
            ButtonTexturePath = buttonTexturePath;
        }
    }
}
