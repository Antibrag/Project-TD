using Data;
using Godot;

namespace Level
{
    public partial class Loader : Node
    {
        public override void _Ready()   
        {
            Data.Loader.LoadLevelsData();
        }

        //Replace to own class
        public async void StartGame()
        {
            LoadFirstUnCompleteLevel();

            //Wait while level is created on main scene
            await ToSignal(GetTree().CreateTimer(1), "timeout");

            Data.Loader.LoadPlayerData();

            GetNode<StartMenu>("/root/Main/StartMenu").Disable();

            GetNode<Objects.Spawner>("/root/Main/Level/Objects/Spawner").StartSpawn(2);
        }

        public void LoadFirstUnCompleteLevel()
        {
            for (int i = 0; i < Storage.LevelsList.Length; i++)
                if (!Storage.LevelsList[i].IsComplete)  
                { 
                    LoadLevel(i);
                    return;
                }

            GD.PushWarning("All levels complete or failed to load");
        }

        public void LoadLevel(string levelName)
        {
            for (int i = 0; i < Storage.LevelsList.Length; i++)
            {
                if (levelName != Storage.LevelsList[i].Name)
                    continue;
                
                var level = GD.Load<PackedScene>(Storage.LevelsList[i].ScenePath).Instantiate();
                GetNode<Node>("/root/Main").CallDeferred("add_child", level);

                GD.Print($"Load level - {Storage.LevelsList[i].Name}");
                return;
            }
        }

        public void LoadLevel(int levelIndex)
        {
            var level = GD.Load<PackedScene>(Storage.LevelsList[levelIndex].ScenePath).Instantiate();
            GetNode<Node>("/root/Main").CallDeferred("add_child", level);

            GD.Print($"Load level - {Storage.LevelsList[levelIndex].Name}");
            return;
        }
    }
}