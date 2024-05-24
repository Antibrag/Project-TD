using Godot;
using Godot.Collections;

namespace Data
{
    public static class Loader
    {
        public static void LoadAllData()
        {
            LoadPlayerData();
            LoadLevelsData();
            LoadBSIButtonsConfiguration();
        }

        private static void LoadLevelsData()
        {
            if (!FileAccess.FileExists(Storage.LevelsDataSavePath))
            {
                GD.PushWarning("Levels data not found\nCreate starting levels information");
                Saver.SaveLevelsData();
            }

            using var file = FileAccess.Open(Storage.LevelsDataSavePath, FileAccess.ModeFlags.Read);

            Dictionary data = (Dictionary)Json.ParseString(file.GetAsText());

            foreach (System.Collections.Generic.KeyValuePair<string, Level> level in Storage.LevelsList)
            {
                try
                {
                    level.Value.IsComplete = (bool)data[level.Key];
                }
                catch (System.Exception)
                {
                    GD.PrintErr($"Cannot load level data from file - {level.Key}");
                    return;
                }
            }

            GD.Print("Load data levels succsesfully");
        }

        private static void LoadPlayerData()
        {
            if (!FileAccess.FileExists(Storage.PlayerDataSavePath))
            {
                GD.PushWarning("Player data not found\nCreate starting player information");
                Saver.SavePlayerData(true);
            }

            using var file = FileAccess.Open(Storage.PlayerDataSavePath, FileAccess.ModeFlags.Read);

            Dictionary data = (Dictionary)Json.ParseString(file.GetAsText());

            GD.Print("Load player data succsesfully");
        }

        private static void LoadBSIButtonsConfiguration()
        {
            if (!FileAccess.FileExists(Storage.BSIButtonsConfigurationPath))
            {
                GD.PushWarning("BSIButtons configuration not found!\nCreate default configuration");
                Saver.SaveBSIButtonsConfiguration();
            }

            using var file = FileAccess.Open(Storage.BSIButtonsConfigurationPath, FileAccess.ModeFlags.Read);

            Dictionary data = (Dictionary)Json.ParseString(file.GetAsText());

            foreach (System.Collections.Generic.KeyValuePair<string, BSIButton> button in Storage.BuildButtonsList)
            {
                string[] buttonParameters = null;

                try
                {
                    buttonParameters = ((string[])data[button.Key]);
                }
                catch (System.Exception)
                {
                    GD.PrintErr($"Cannot load {button.Key} configuration. Continue...");
                }

                Key buttonKey;
                System.Enum.TryParse(buttonParameters[0], out buttonKey);

                button.Value.ShortcutKey = buttonKey;
                button.Value.BSIName = buttonParameters[1];
                button.Value.ButtonTexturePath = buttonParameters[2];
            }
        }
    }
}
