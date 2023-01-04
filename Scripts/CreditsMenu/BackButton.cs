using Godot;
using System;

namespace Credits;
public partial class BackButton : Button
{
	private void ToMainMenu()
	{
		GetTree().ChangeSceneToFile("res://Scenes/MainMenu.tscn");
	}
}
