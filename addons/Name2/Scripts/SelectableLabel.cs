using Godot;
using System;

public class SelectableLabel : HBoxContainer {
    private bool selected = false;
    private bool uppercase = false;
    private string text = "";

    [Export]
    private string selectKey = "name_select";

    [Export]
    public bool Selected {
        get { return selected; }
        set { 
            selected = value;
            if (selectTexture != null) {
                float alpha = (selected) ? 1.0f : 0.0f;
                selectTexture.Modulate = new Color(1.0f, 1.0f, 1.0f, alpha);
            }
        }
    }

    [Export]
    public bool Uppercase {
        get { return uppercase; }
        set { 
            uppercase = value;
            if (label != null) {
                label.Uppercase = value;
            }
        }
    }

    [Export]
    public string Text {
        get { return text; }
        set {
            text = value;
            if (label != null) {
                label.Text = value;
            }
        }
    }

    [Signal]
    delegate void OnSelected(string val);

    private TextureRect selectTexture = null;
    private Label label = null;

    public override void _Ready() {
        selectTexture = GetNode<TextureRect>("Selected");
        label = GetNode<Label>("Label");

        float alpha = (selected) ? 1.0f : 0.0f;
        selectTexture.Modulate = new Color(1.0f, 1.0f, 1.0f, alpha);
        label.Uppercase = uppercase;
        label.Text = text;
    }

    public override void _Process(float delta) {
        if (selected && Input.IsActionJustPressed(selectKey)) {
            if (uppercase) {
                this.EmitSignal("OnSelected", label.Text.ToUpper());
            }
            else {
                this.EmitSignal("OnSelected", label.Text);    
            }
            
        }
    }
}
