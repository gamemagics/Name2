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
            Clear();
            ShowTextWithPlaceholder();
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

    public override void _Ready() {
        label = GetNode<Label>("Label");
        if (maxLength != -1) {
            label.Text = showText;
        }
        else {
            label.Text = text;
        }
    }

    public void Push(char c) {
        if (pointer == maxLength) {
            return;
        }

        ++pointer;
        text += c;
        ShowTextWithPlaceholder();
    }

    public void Pop() {
        if (pointer > 0) {
            --pointer;
            text = text.Substring(0, text.Length - 1);
            ShowTextWithPlaceholder();
        }
    }

    private void ShowTextWithPlaceholder() {
        showText = text;
        for (int i = text.Length; i < maxLength; ++i) {
            showText += placeholder;
        }

        if (label != null) {
            label.Text = showText;
        }
    }

    public void Clear() {
        pointer = 0;
        text = "";
    }
}
