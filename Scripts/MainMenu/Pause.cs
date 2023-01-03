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

	private void _on_Quit_pressed()
	{
		isInMenu = true;
		popupMenu.PopupCentered();
	}

	private void _on_Settings_pressed()
	{
		isInMenu = true;
		settingsPopup.PopupCenteredRatio(.7f);
	}

	private void _on_Settings_visibility_changed()
	{
		if(settingsPopup.Visible)
		{
			isInMenu= true;
			return;
		}
		isInMenu = false;
	}

	private void _on_ConfirmationDialog_confirmed()
	{
		GetTree().Quit();
	}

	private void _on_ConfirmationDialog_cancelled()
	{
		isInMenu= false;
	}

	private void _on_ConfirmationDialog_custom(string action)
	{
		GetTree().Paused = false;
		GD.Print(action);
		GetTree().ChangeSceneToFile("res://Scenes/MainMenu.tscn");
	}

	private void _on_Resume_pressed()
	{
		Hide();
		GetTree().Paused = false;
		GD.Print("Resume");
	}
}
