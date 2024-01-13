using Godot;
using Godot.Collections;

public class DataController
{
    public static Data GameData { get; set; }

    private const string PlayerDataSavePath = "user://PlayerData.json";
    private const string LevelsDataSavePath = "user://LevelsData.json";

    private static void SaveStartupLevelsData()
    {
        using var file = FileAccess.Open(LevelsDataSavePath, FileAccess.ModeFlags.Write);

        Dictionary dict = new();

        for (int i = 0; i < Data.LevelsList.Length; i++) 
            dict.Add(Data.LevelsList[i].Name, false);
        

        file.StoreLine(Json.Stringify(dict));
    }

    private static void SaveStartupPlayerData()
    {
        using var file = FileAccess.Open(PlayerDataSavePath, FileAccess.ModeFlags.Write);

        Dictionary dict = new();

        //Add to dictionary player info

        file.StoreLine(Json.Stringify(dict));
    }

    public static void LoadLevelsData()
    {
        if (!FileAccess.FileExists(LevelsDataSavePath)) 
            SaveStartupLevelsData();

        using var file = FileAccess.Open(LevelsDataSavePath, FileAccess.ModeFlags.Read);

        Dictionary dict = (Dictionary)Json.ParseString(file.GetAsText());

        for (int i = 0; i < Data.LevelsList.Length; i++) 
        {
            try
            {
                Data.LevelsList[i].IsCompleted = (bool) dict[Data.LevelsList[i].Name];
            }
            catch (System.Exception ex)
            {
                GD.PrintErr($" - Cannot load level information from file - \n{ex}");
            }
        }
    }

    public static void LoadPlayerData()
    {
        if (!FileAccess.FileExists(PlayerDataSavePath)) 
            SaveStartupPlayerData();

        using var file = FileAccess.Open(PlayerDataSavePath, FileAccess.ModeFlags.Read);

        Dictionary dict = (Dictionary)Json.ParseString(file.GetAsText());

        //Code...
    }
}
