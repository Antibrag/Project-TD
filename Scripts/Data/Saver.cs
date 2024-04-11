using Godot;
using Godot.Collections;

namespace Data
{
    public class Saver
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
            using var file = FileAccess.Open(Storage.GlobalInfo.LevelsDataSavePath, FileAccess.ModeFlags.Write);

            Dictionary data = new();

            for (int i = 0; i < Storage.LevelsList.Length; i++) 
                data.Add(Storage.LevelsList[i].Name, Storage.LevelsList[i].IsComplete);

            file.StoreLine(Json.Stringify(data));
        }
    }
}