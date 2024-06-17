using Godot;

namespace Data
{
    public class BSIButton
    {
        public Key ShortcutKey { get; set; }
        public string BSIName { get; set; } //Build-Spell-Item name

        public BSIButton()
        {
            ShortcutKey = Key.None;
            BSIName = "none";
        }

        public BSIButton(Key shortcut, string BSIName)
        {
            ShortcutKey = shortcut;
            this.BSIName = BSIName;
        }
    }
}
