using Godot;

namespace Game;
public partial class SphereSpin : CSGBox3D
{
	public override void _Process(double delta)
	{
		this.RotateZ((float)(2f * delta));
		this.RotateY((float)(1.5f * delta));
		this.RotateX((float)(1f * delta));
	}
}
