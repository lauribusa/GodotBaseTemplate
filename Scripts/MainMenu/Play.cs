using Godot;
namespace MainMenu;
public partial class Play : Button
{
	[Export(hint: PropertyHint.File, hintString: "Game scene")]
	public PackedScene scene;

	private void ToGameScene()
	{
		GetTree().ChangeSceneToPacked(scene);
	}
}
