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
        public string ActionTargetName { get; set; }
        public Texture ButtonTexture { get; set; }

        public BSButton()
        {
            ShortcutKey = Key.None;
            ActionTargetName = "";
            ButtonTexture = null;
        }

        public BSButton(Key shortcut, string actionTargetName, Texture buttonTexture)
        {
            ShortcutKey = shortcut;
            ActionTargetName = actionTargetName;
            ButtonTexture = buttonTexture;
        }
    }
}
