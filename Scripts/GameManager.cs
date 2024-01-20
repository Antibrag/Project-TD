using Godot;

public partial class GameManager : Node
{
    public override void _Ready()   
    {
        DataController.LoadLevelsData();
    }

    public async void StartGame()
    {
        LoadFirstUnCompleteLevel();

        //Wait while level is created on main scene
        await ToSignal(GetTree().CreateTimer(1), "timeout");

        DataController.LoadPlayerData();

        GetNode<StartMenu>("/root/Main/StartMenu").Disable();

        GetNode<Spawner>("/root/Main/Level/Objects/Spawner").StartSpawn(2);
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