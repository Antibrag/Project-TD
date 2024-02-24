using Godot;
using Godot.Collections;

namespace Data
{
    public class Saver
    {
        public static void SaveStartingPlayerData()
        {
            using var file = FileAccess.Open(Storage.PlayerDataSavePath, FileAccess.ModeFlags.Write);

            Dictionary data = new() 
            {
                {"Level", 1}
            };

            file.StoreLine(Json.Stringify(data));
        }

        public static void SaveLevelsData()
        {
            using var file = FileAccess.Open(Storage.LevelsDataSavePath, FileAccess.ModeFlags.Write);

            Dictionary data = new();

            for (int i = 0; i < Storage.LevelsList.Length; i++) 
                data.Add(Storage.LevelsList[i].Name, Storage.LevelsList[i].IsComplete);

            file.StoreLine(Json.Stringify(data));
        }
    }
}