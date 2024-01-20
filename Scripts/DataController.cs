using Godot;
using Godot.Collections;

public class DataController
{
    private const string PlayerDataSavePath = "user://PlayerData.json";
    private const string LevelsDataSavePath = "user://LevelsData.json";

    private static void SaveStartingLevelsData()
    {
        using var file = FileAccess.Open(LevelsDataSavePath, FileAccess.ModeFlags.Write);

        Dictionary data = new();

        for (int i = 0; i < Data.LevelsList.Length; i++) 
            data.Add(Data.LevelsList[i].Name, false);
        
        file.StoreLine(Json.Stringify(data));
    }

    private static void SaveStartingPlayerData()
    {
        using var file = FileAccess.Open(PlayerDataSavePath, FileAccess.ModeFlags.Write);

        Dictionary data = new() 
        {
            {"Level", 1}
        };

        file.StoreLine(Json.Stringify(data));
    }

    public static void SaveLevelsData()
    {
        using var file = FileAccess.Open(LevelsDataSavePath, FileAccess.ModeFlags.Write);

        Dictionary data = new();

        for (int i = 0; i < Data.LevelsList.Length; i++) 
            data.Add(Data.LevelsList[i].Name, Data.LevelsList[i].IsComplete);

        file.StoreLine(Json.Stringify(data));
    }

    public static void LoadLevelsData()
    {
        if (!FileAccess.FileExists(LevelsDataSavePath)) 
        {
            GD.PushWarning("Levels data not found\nCreate starting levels information");
            SaveStartingLevelsData();
        }

        using var file = FileAccess.Open(LevelsDataSavePath, FileAccess.ModeFlags.Read);

        Dictionary data = (Dictionary)Json.ParseString(file.GetAsText());

        for (int i = 0; i < Data.LevelsList.Length; i++) 
        {
            try
            {
                Data.LevelsList[i].IsComplete = (bool) data[Data.LevelsList[i].Name];
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

        Dictionary data = (Dictionary)Json.ParseString(file.GetAsText());

        Player.GlobalInfo.Level = (int) data["Level"];

        GD.Print("Load player data succsesfully");
    }
}
