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
        public Shortcut Shortcut { get; set; }
        public string ActionTargetName { get; set; }
        public Texture ButtonTexture { get; set; }

        public BSButton()
        {
            Shortcut = null;
            ActionTargetName = "";
            ButtonTexture = null;
        }

        public BSButton(Shortcut shortcut, string actionTargetName, Texture buttonTexture)
        {
            Shortcut = shortcut;
            ActionTargetName = actionTargetName;
            ButtonTexture = buttonTexture;
        }
    }
}
