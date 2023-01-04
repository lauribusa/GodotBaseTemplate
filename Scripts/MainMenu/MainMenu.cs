using Godot;
using System;
namespace MainMenu
{
	public partial class MainMenu : Node
	{
		[Export]
		public NodePath settingsMenuPath;
		private Window settingsPopup;
		[Export]
		public NodePath nodePath;
		private ConfirmationDialog popupMenu;
		#region Lifecycle events
		public override void _Ready()
		{
			popupMenu = GetNode(nodePath) as ConfirmationDialog;
			settingsPopup = GetNode(settingsMenuPath) as Window;
		}
		#endregion

		#region Signals
		private void OpenQuitDialog()
		{
			popupMenu.PopupCentered();
		}

		private void OpenSettings()
		{
			settingsPopup.PopupCenteredRatio(.7f);
		}

		private void QuitGame()
		{
			GetTree().Quit();
		}
		#endregion

	}
}

