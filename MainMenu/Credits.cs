using Godot;
using System;
namespace MainMenu
{
    public partial class Credits : Button
    {
        [Export(hint: PropertyHint.File, hintString: "Credit scene")]
        public PackedScene scene;

        private void _on_Credits_pressed()
        {
            GetTree().ChangeSceneToPacked(scene);
        }
    }

}
