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

	public override void _Ready()
	{
		popupMenu = GetNode(quitConfirmPath) as ConfirmationDialog;
		settingsPopup = GetNode(settingsMenuPath) as Window;
	}

	private void _on_Quit_pressed()
	{
		popupMenu.PopupCentered();
	}

	private void _on_Settings_pressed()
	{
		settingsPopup.PopupCenteredRatio(.7f);
	}

	private void _on_ConfirmationDialog_confirmed()
	{
		GetTree().Quit();
	}

	private void _on_Resume_pressed()
	{
		GD.Print("Resume");
	}
}
