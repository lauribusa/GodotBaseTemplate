using Godot;
using System;

public partial class Pause : Control
{
	[Export]
	public NodePath settingsMenuPath;
	private Window settingsPopup;
	[Export]
	public NodePath quitConfirmPath;
	private ConfirmationDialog popupMenu;

	private bool isInMenu = false;
	#region Lifecycle events
	public override void _Ready()
	{
		popupMenu = GetNode(quitConfirmPath) as ConfirmationDialog;
		settingsPopup = GetNode(settingsMenuPath) as Window;
	}

	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("Pause"))
		{
			if (isInMenu) return;
			if (this.Visible)
			{
				Hide();
				GetTree().Paused = false;
				return;
			}
			Show();
			GetTree().Paused = true;
		}
	}
	#endregion

	#region Signals
	private void OpenQuitDialog()
	{
		isInMenu = true;
		popupMenu.PopupCentered();
	}

	private void OpenSettings()
	{
		isInMenu = true;
		settingsPopup.PopupCenteredRatio(.7f);
	}

	private void OnSettingsVisibilityChanged()
	{
		if (settingsPopup.Visible)
		{
			isInMenu = true;
			return;
		}
		isInMenu = false;
	}

	private void QuitGame()
	{
		GetTree().Quit();
	}

	private void OnQuitDialogCancel()
	{
		isInMenu = false;
	}

	private void BackToMain(string action)
	{
		GetTree().Paused = false;
		GetTree().ChangeSceneToFile("res://Scenes/MainMenu.tscn");
	}

	private void ResumeGame()
	{
		Hide();
		GetTree().Paused = false;
	}
	#endregion
}
