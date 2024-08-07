using Godot;

namespace UI
{
    public partial class StartMenu : Control
    {
        public void OnStartButtonPressed() 
            => GetNode<LevelObjects.Loader>("/root/Loader").StartGame();
        
        public void Disable()
        {
            GetNode<Button>("StartButton").Disabled = true;
            Hide();
    
        }
    }
}
