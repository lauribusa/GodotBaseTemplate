using Godot;
using System;

public partial class FpsDisplay : Label
{
	public override void _Ready() => VisibilityChanged += OnVisibilityChanged;

	public override void _Process(double delta) => Text = $"FPS: {Engine.GetFramesPerSecond()}";

	private void OnVisibilityChanged() => SetProcess(Visible);
}
