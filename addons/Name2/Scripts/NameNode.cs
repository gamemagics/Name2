using Godot;
using System;

public class NameNode : VBoxContainer {
    private Texture avatar = null;
    private int maxLength = 8;
    private string text = "";
    private string hint = "";
    private TextureRect avatarRect = null;
    private BackgroundLabel inputLabel = null;
    private BackgroundLabel hintLabel = null;

    [Export]
    public Texture Avatar {
        get { return avatar; }
        set { 
            avatar = value;
            if (avatarRect != null) {
                avatarRect.Texture = avatar;
            }
        }
    }

    [Export]
    public int MaxLength {
        get { return maxLength; }
        set {
            maxLength = value;
            if (inputLabel != null) {
                inputLabel.MaxLength = maxLength;
            }
        }
    }

    [Export]
    public string Hint {
        get { return hint; }
        set {
            hint = value;
            if (hintLabel != null) {
                hintLabel.Text = hint;
            }
        }
    }

    [Signal]
    delegate void OnSubmitted(string text);

    public override void _Ready() {
        avatarRect = GetNode<TextureRect>("DisplayContainer/Avatar");
        inputLabel = GetNode<BackgroundLabel>("DisplayContainer/Input");
        hintLabel = GetNode<BackgroundLabel>("DisplayContainer/Hint");

        avatarRect.Texture = avatar;
        inputLabel.MaxLength = maxLength;
        hintLabel.Text = hint;
    }
}
