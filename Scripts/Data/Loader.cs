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

            Dictionary data = (Dictionary)Json.ParseString(file.GetAsText());

            for (int i = 0; i < Storage.LevelsList.Length; i++) 
            {
                try
                {
                    Storage.LevelsList[i].IsComplete = (bool) data[Storage.LevelsList[i].Name];
                }
                catch (System.Exception)
                {
                    GD.PrintErr($"Cannot load level data from file - {Storage.LevelsList[i].Name}");
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
                Saver.SaveStartingPlayerData();
            }

            using var file = FileAccess.Open(Storage.PlayerDataSavePath, FileAccess.ModeFlags.Read);

            Dictionary data = (Dictionary)Json.ParseString(file.GetAsText());

            Storage.GlobalInfo.Level = (int) data["Level"];

            GD.Print("Load player data succsesfully");
        }
    }
}