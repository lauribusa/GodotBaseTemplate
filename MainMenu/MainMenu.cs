using Godot;
using System;
namespace MainMenu
{
	public partial class MainMenu : Node, IMainMenu
	{
		// Declare member variables here. Examples:
		// private int a = 2;
		// private string b = "text";
		[Export]
		public NodePath settingsMenuPath;
		private Window settingsPopup;
		[Export]
		public NodePath nodePath;
		private ConfirmationDialog popupMenu;

		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			popupMenu = GetNode(nodePath) as ConfirmationDialog;
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
	}
}

