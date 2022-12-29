using Godot;
using System;

public partial class MainMenuConfirmationDialog : ConfirmationDialog
{
    public override void _Ready()
    {
        var okButton = GetOkButton();
        okButton.Text = "_common.quit";
        var cancelButton = GetCancelButton();
        cancelButton.Text = "_common.cancel";
    }
}
