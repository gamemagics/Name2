using Godot;
using System;

public class BackgroundLabel : PanelContainer {
    private int maxLength = -1; // -1 == inf, which means this is not an input node
    private int pointer = 0;
    private string text = "";
    private string showText = ""; // will be diplayed actually
    private Label label = null;

    [Export]
    private string placeholder = ".";

    [Export]
    public int MaxLength {
        get { return maxLength; }
        set {
            maxLength = value;
            text = "";
            pointer = 0;
            for (int i = 0; i < maxLength; ++i) {
                showText += placeholder;
            }

            if (label != null) {
                label.Text = showText;
            }
        }
    }

    [Export]
    public string Text {
        get { return text; }
        set { 
            text = value;
            if (label != null && maxLength == -1) {
                label.Text = text;
            }
        }
    }

    [Signal]
    delegate void OnTextChanged();

    public override void _Ready() {
        label = GetNode<Label>("Label");
        if (maxLength != -1) {
            label.Text = showText;
        }
        else {
            label.Text = text;
        }
    }
}
