using Godot;
using System;

public partial class HUD : Control
{
	private TextureProgressBar ManaBar;
	private TextureProgressBar HealthBar;

    public override void _Ready()
    {
        ManaBar = GetNode<TextureProgressBar>("ManaBar");
		HealthBar = GetNode<TextureProgressBar>("HealthBar");

		HealthBar.Value = HealthBar.MaxValue;
		ManaBar.Value = HealthBar.MaxValue;	
    }

    public void ChangeManaBarValue(float newValue)
		=> ManaBar.Value = newValue;

	public void ChangeHealthBarValue(float newValue)
		=> HealthBar.Value = newValue;
}
