using Godot;
using System;

public partial class FpsDisplay : Label
{
	public override void _Process(double delta)
	{
		if (!Visible) return;
		this.Text = $"FPS: {Engine.GetFramesPerSecond()}";
	}
}
