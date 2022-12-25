using Godot;
using System;


namespace Credits;
public partial class BackButton : Button
{
	private void _on_button_pressed()
	{
		GetTree().ChangeSceneToFile("res://MainMenu.tscn");
	}
}
