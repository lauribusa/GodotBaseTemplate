using Godot;
using System;
namespace MainMenu;
public partial class Play : Button
{
	[Export(hint: PropertyHint.File, hintString: "Game scene")]
	public PackedScene scene;

	private void _on_Play_pressed()
	{
		GetTree().ChangeSceneToPacked(scene);
	}
}
