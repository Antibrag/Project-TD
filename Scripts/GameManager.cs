using Godot;

public partial class GameManager : Node
{
    public override void _Ready()   
    {
        DataController.LoadLevelsData();
        DataController.LoadPlayerData();

        LoadFirstUnCompleteLevel(); 
    }

    private void LoadFirstUnCompleteLevel()
    {
        for (int i = 0; i < Data.LevelsList.Length; i++)
            if (!Data.LevelsList[i].IsCompleted)  
            { 
                LoadLevel(i);
                return;
            }

        GD.PushWarning("All levels complete or failed to load");
    }

    private void LoadLevel(string levelName)
    {
        for (int i = 0; i < Data.LevelsList.Length; i++)
        {
            if (levelName != Data.LevelsList[i].Name)
                continue;
            
            var level = GD.Load<PackedScene>(Data.LevelsList[i].ScenePath).Instantiate();
            GetNode<Node>("/root/Main").CallDeferred("add_child", level);

            GD.Print($"Load level - {Data.LevelsList[i].Name}");
            return;
        }
    }

    private void LoadLevel(int levelIndex)
    {
        var level = GD.Load<PackedScene>(Data.LevelsList[levelIndex].ScenePath).Instantiate();
        GetNode<Node>("/root/Main").CallDeferred("add_child", level);

        GD.Print($"Load level - {Data.LevelsList[levelIndex].Name}");
        return;
    }
}