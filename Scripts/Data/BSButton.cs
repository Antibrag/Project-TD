using Godot;

namespace Data
{
    // public enum ButtonTypes
    // {
    //     None,
    //     Skill,
    //     Build,
    //     Item
    // }

    public class BSButton
    {
        public Key ShortcutKey { get; set; }
        public string BSIName { get; set; } //Build-Skill-Item name
        public string ButtonTexturePath { get; set; }

        public BSButton()
        {
            ShortcutKey = Key.None;
            BSIName = "";
            ButtonTexturePath = null;
        }

        public BSButton(Key shortcut, string BSIName, string buttonTexturePath)
        {
            ShortcutKey = shortcut;
            this.BSIName = BSIName;
            ButtonTexturePath = buttonTexturePath;
        }
    }
}
