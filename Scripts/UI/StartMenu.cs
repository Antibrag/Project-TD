using Godot;

public partial class StartMenu : Control
{
    public void OnStartButtonPressed() =>
        GetNode<GameManager>("/root/Main/GameManager").StartGame();
    
    public void Disable()
    {
        GetNode<Button>("StartButton").Disabled = true;
        Hide();
    }
}
