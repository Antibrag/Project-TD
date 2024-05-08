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
        public Texture ButtonTexture { get; set; }

        public BSButton()
        {
            ShortcutKey = Key.None;
            BSIName = "";
            ButtonTexture = null;
        }

        public BSButton(Key shortcut, string BSIName, Texture buttonTexture)
        {
            ShortcutKey = shortcut;
            this.BSIName = BSIName;
            ButtonTexture = buttonTexture;
        }
    }
}
