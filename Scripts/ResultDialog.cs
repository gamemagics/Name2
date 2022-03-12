using Godot;
using System;

public class ResultDialog : PopupDialog {
    private Label label = null;
    public override void _Ready() {
        label = GetNode<Label>("Label");
    }

	public void OnSubmitted(string text) {
        label.Text = "Welcome to " + text + " engine!";
        this.PopupCentered();
    }
}
