using Godot;
using System;

namespace MainMenu
{
	public partial class SettingsButton : Button
	{
		// Called when the node enters the scene tree for the first time.
		[Export(hint: PropertyHint.File, hintString: "Credit scene")]
		public PackedScene scene;

		private void _on_Settings_pressed()
		{
			GetTree().ChangeSceneToPacked(scene);
		}
	}
}

