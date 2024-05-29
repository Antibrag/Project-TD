using Data;
using Godot;

namespace UI
{
    public partial class BSIButton : TextureButton
    {
        public string ButtonName { get; set; }

        private Data.BSIButton _buttonConfiguration;

        private void ChangeManaCostLabel()
        {
            if (_buttonConfiguration.BSIName == "none" || _buttonConfiguration.BSIName == "")
                return;

            GetNode<Label>("ManaCastIcon/ManaCost").Text = Storage.BuildsList[_buttonConfiguration.BSIName].ManaCost.ToString();
        }

        public void Initialize(int actionIndex, string buttonType)
        {
            Disabled = false;

            switch (buttonType)
            {
                case nameof(Build):
                    ButtonName = $"{actionIndex + 1}BButton";
                    _buttonConfiguration = Storage.BuildButtonsList[ButtonName];
                    ChangeManaCostLabel();
                    break;

                    //case nameof(Spell):
                    //	ButtonName = $"{actionIndex}SButton";
                    //break;

                    //case nameof(Item):
                    //	ButtonName = $"{actionIndex}IButton";
                    //break;
            }

            //Add shortcut to key
            Shortcut = new();
            Shortcut.Events = (Godot.Collections.Array)new Godot.Collections.Array();
            Shortcut.Events.Add(new InputEventKey() { Keycode = _buttonConfiguration.ShortcutKey });

            //Add keyBind label shortcut key number
            string shortcutKeyName = _buttonConfiguration.ShortcutKey.ToString();

            if (shortcutKeyName.Contains("Key"))
                shortcutKeyName = shortcutKeyName.Substring(shortcutKeyName.IndexOf('y') + 1);

            GetNode<Label>("KeyBind").Text = shortcutKeyName;

            //Add build-skill-item texture
            TextureNormal = GD.Load<CompressedTexture2D>(_buttonConfiguration.ButtonTexturePath);

            GD.Print($"Initialize button({ButtonName}, shortcut - {_buttonConfiguration.ShortcutKey})");
        }

        public void OnPressed()
        {
            GetNode<Player>("/root/Main/Level/Player").SelectBuild(_buttonConfiguration.BSIName);
        }
    }
}

