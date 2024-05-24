using Godot;
using System.Collections.Generic;

namespace Data
{
    public static class Saver
    {
        public static void SavePlayerData(bool isStartingData = false)
        {
            // using var file = FileAccess.Open(Storage.GlobalInfo.PlayerDataSavePath, FileAccess.ModeFlags.Write);

            // if (isStartingData)
            // {
            //     file.StoreLine(Json.Stringify(data));
            //     return;
            // }
        }

        public static void SaveLevelsData()
        {
            using var file = FileAccess.Open(Storage.LevelsDataSavePath, FileAccess.ModeFlags.Write);

            Godot.Collections.Dictionary data = new();

            foreach (KeyValuePair<string, Level> level in Storage.LevelsList)
                data.Add(level.Key, level.Value.IsComplete);

            file.StoreLine(Json.Stringify(data));
        }

        public static void SaveBSIButtonsConfiguration()
        {
            using var file = FileAccess.Open(Storage.BSIButtonsConfigurationPath, FileAccess.ModeFlags.Write);

            Godot.Collections.Dictionary data = new();

            foreach (KeyValuePair<string, BSIButton> button in Storage.BuildButtonsList)
            {
                string[] buttonParameters = { button.Value.ShortcutKey.ToString(), button.Value.BSIName, button.Value.ButtonTexturePath };
                data.Add(button.Key, buttonParameters);
            }

            file.StoreLine(Json.Stringify(data));
        }
    }
}
