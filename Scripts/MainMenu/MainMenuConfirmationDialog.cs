using Godot;
using System;

public partial class MainMenuConfirmationDialog : ConfirmationDialog
{
    [Export]
    private bool showBackToMain;
    private Button cancelButton;
    public override void _Ready()
    {
        var okButton = GetOkButton();
        okButton.Text = "_common.quit";
        cancelButton = GetCancelButton();
        cancelButton.Text = "_common.cancel";
        if (showBackToMain)
        {
            this.AddButton("start.toMain", true, "toMain");
        }
        
    }

    private void OnVisibilityChanged()
    {
        if (Visible)
        {
			cancelButton.GrabFocus();
		}
    }
}
