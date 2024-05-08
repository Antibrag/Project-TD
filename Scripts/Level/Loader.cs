using Data;
using Godot;
using System.Collections.Generic;

namespace LevelObjects
{
    public partial class Loader : Node
    {
        public override void _Ready() =>        
            Data.Loader.LoadLevelsData();
        
        //Replace to own class
        public async void StartGame()
        {
            LoadFirstUnCompleteLevel();

            //Wait while level is created on main scene
            await ToSignal(GetTree().CreateTimer(1), "timeout");

            Data.Loader.LoadPlayerData();

            GetNode<UI.StartMenu>("/root/Main/StartMenu").Disable();

            GetNode<Spawner>("/root/Main/Level/Objects/Spawner").StartSpawn(2);
        }

        public void LoadFirstUnCompleteLevel()
        {            
            foreach (KeyValuePair<string, Data.Level> level in Storage.LevelsList)
            {
                GD.Print(level.Key, level.Value.IsComplete);
                if (!level.Value.IsComplete)  
                { 
                    LoadLevel(level);
                    return;
                }
            }
            GD.PushWarning("All levels complete or failed to load");
        }

        public void LoadLevel(KeyValuePair<string, Data.Level> level)
        {
            var level_scene = GD.Load<PackedScene>(level.Value.ScenePath).Instantiate();
            GetNode<Node>("/root/Main").CallDeferred("add_child", level_scene);

            Storage.CurrentLevel = level.Key;

            GetNode<UI.HUD>("/root/Main/HUD").InitializeButtons();

            GD.Print($"Load level - {level.Key}");
            return;
        }
    }
}