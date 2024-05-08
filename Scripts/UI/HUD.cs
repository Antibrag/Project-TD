using Godot;

namespace UI
{
	public partial class HUD : Control
	{
		private TextureProgressBar _manaBar;
		private TextureProgressBar _healthBar;
		private Timer _decreasedTimer;

		private float _decreasedMana = 0;
		private float _decreasedHealth = 0;

		private void InitializeButtons()
		{
			HBoxContainer buildbar = GetNode<HBoxContainer>("Buildbar");

			for (int i = 0; i < buildbar.GetChildCount(); i++)
			{
				BuildSkill_Button button = (BuildSkill_Button) buildbar.GetChild(i);
				button.Initialize(i, nameof(Build));
			}
		}

		public override void _Ready()
		{
			_manaBar = GetNode<TextureProgressBar>("ManaBar");
			_healthBar = GetNode<TextureProgressBar>("HealthBar");
			_decreasedTimer = GetNode<Timer>("DecreasedTimer");

			_healthBar.Value = _healthBar.MaxValue;
			_manaBar.Value = _healthBar.MaxValue;	
		}

		public void AddDecreasingHealth(float value)
		{
			_decreasedHealth += value;
			_decreasedTimer.Start();
		}

		public void AddDecreasingMana(float value)
		{
			_decreasedMana += value;
			_decreasedTimer.Start();
		}

		public void OnDecreasedTimerTimeout()
		{
			if (_decreasedHealth == 0 && _decreasedMana == 0)
			{
				_decreasedTimer.Stop();
				return;
			}

			float decreaseHealth = _decreasedHealth * 3 / 100;
			float decreaseMana = _decreasedMana * 3 / 100;

			_healthBar.Value -= decreaseHealth;
			_decreasedHealth -= decreaseHealth;

			_manaBar.Value -= decreaseMana;
			_decreasedMana -= decreaseMana;
			
		}
	}
}
