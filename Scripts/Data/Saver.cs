using Godot;
using System.Collections.Generic;

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

            Godot.Collections.Dictionary data = new();

            foreach (KeyValuePair<string, Storage.Level> level in Storage.LevelsList) 
                data.Add(level.Key, level.Value.IsComplete);

            file.StoreLine(Json.Stringify(data));
        }
    }
}