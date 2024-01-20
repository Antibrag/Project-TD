using Godot;
using Godot.Collections;

public class DataController
{
    private const string PlayerDataSavePath = "user://PlayerData.json";
    private const string LevelsDataSavePath = "user://LevelsData.json";

    private static void SaveStartingLevelsData()
    {
        using var file = FileAccess.Open(LevelsDataSavePath, FileAccess.ModeFlags.Write);

        Dictionary dict = new();

        for (int i = 0; i < Data.LevelsList.Length; i++) 
            dict.Add(Data.LevelsList[i].Name, false);
        
        file.StoreLine(Json.Stringify(dict));
    }

    private static void SaveStartingPlayerData()
    {
        using var file = FileAccess.Open(PlayerDataSavePath, FileAccess.ModeFlags.Write);

        Dictionary dict = new() 
        {
            {"Level", 1}
        };

        file.StoreLine(Json.Stringify(dict));
    }

    public static void LoadLevelsData()
    {
        if (!FileAccess.FileExists(LevelsDataSavePath)) 
        {
            GD.PushWarning("Levels data not found\nCreate starting levels information");
            SaveStartingLevelsData();
        }

        using var file = FileAccess.Open(LevelsDataSavePath, FileAccess.ModeFlags.Read);

        Dictionary dict = (Dictionary)Json.ParseString(file.GetAsText());

        for (int i = 0; i < Data.LevelsList.Length; i++) 
        {
            try
            {
                Data.LevelsList[i].IsCompleted = (bool) dict[Data.LevelsList[i].Name];
            }
            catch (System.Exception)
            {
                GD.PrintErr($"Cannot load level data from file - {Data.LevelsList[i].Name}");
                return;
            }
        }

        GD.Print("Load data levels succsesfully");
    }

    public static void LoadPlayerData()
    {
        if (!FileAccess.FileExists(PlayerDataSavePath)) 
        {
            GD.PushWarning("Player data not found\nCreate starting player information");
            SaveStartingPlayerData();
        }

        using var file = FileAccess.Open(PlayerDataSavePath, FileAccess.ModeFlags.Read);

        Dictionary dict = (Dictionary)Json.ParseString(file.GetAsText());

        Player.GlobalInfo.Level = (int) dict["Level"];

        GD.Print("Load player data succsesfully");
    }
}
