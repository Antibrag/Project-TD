using Godot;
using Godot.Collections;

namespace Data
{
    public class Loader
    {
        public static void LoadLevelsData()
        {
            if (!FileAccess.FileExists(Storage.LevelsDataSavePath)) 
            {
                GD.PushWarning("Levels data not found\nCreate starting levels information");
                Saver.SaveLevelsData();
            }

            using var file = FileAccess.Open(Storage.LevelsDataSavePath, FileAccess.ModeFlags.Read);

            Dictionary data = (Dictionary) Json.ParseString(file.GetAsText());

            foreach (System.Collections.Generic.KeyValuePair<string, Level> level in Storage.LevelsList) 
            {
                try
                {
                    level.Value.IsComplete = (bool) data[level.Key];
                }
                catch (System.Exception)
                {
                    GD.PrintErr($"Cannot load level data from file - {level.Key}");
                    return;
                }
            }

            GD.Print("Load data levels succsesfully");
        }

        public static void LoadPlayerData()
        {
            if (!FileAccess.FileExists(Storage.PlayerDataSavePath)) 
            {
                GD.PushWarning("Player data not found\nCreate starting player information");
                Saver.SavePlayerData(true);
            }

            using var file = FileAccess.Open(Storage.PlayerDataSavePath, FileAccess.ModeFlags.Read);

            Dictionary data = (Dictionary)Json.ParseString(file.GetAsText());

            //Storage.GlobalInfo.CurrentLevelIdx = (int) data["CurrentLevelIdx"];

            GD.Print("Load player data succsesfully");
        }
    }
}