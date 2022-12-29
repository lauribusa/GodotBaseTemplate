using Godot;
using System;

namespace MainMenu
{
	public partial class SettingsButton : Button
	{
		[Export(hint: PropertyHint.File, hintString: "Credit scene")]
		public PackedScene scene;

		private void _on_Settings_pressed()
		{
			GetTree().ChangeSceneToPacked(scene);
		}
	}
}

