using Godot;
using Godot.Collections;
using System;

public class NameNode : VBoxContainer {
    private Texture avatar = null;
    private int maxLength = 8;
    private string text = "";
    private string hint = "";
    private TextureRect avatarRect = null;
    private BackgroundLabel inputLabel = null;
    private BackgroundLabel hintLabel = null;
    private GridContainer keyboardContainer = null;
    private HBoxContainer controlContainer = null;
    private Array<SelectableLabel> keys = new Array<SelectableLabel>();
    private Array<SelectableLabel> controls = new Array<SelectableLabel>();
    private int position = 0;

    [Export]
    private string upKey = "name_up";

    [Export]
    private string downKey = "name_down";

    [Export]
    private string leftKey = "name_left";

    [Export]
    private string rightKey = "name_right";

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

        keyboardContainer = GetNode<GridContainer>("KeyboardContainer/SeperatorContainer/InputContainer");
        controlContainer = GetNode<HBoxContainer>("KeyboardContainer/SeperatorContainer/ControlContainer");

        var keyNodes = keyboardContainer.GetChildren();
        foreach (var keyNode in keyNodes) {
            SelectableLabel label = keyNode as SelectableLabel;
            label.Connect("OnSelected", this, "OnSelected");
            keys.Add(label);
        }

        var controlNodes = controlContainer.GetChildren();
        foreach (var controlNode in controlNodes) {
            SelectableLabel label = controlNode as SelectableLabel;
            label.Connect("OnSelected", this, "OnSelected");
            controls.Add(label);
        }
    }

    public override void _Process(float delta) {
        int next = position;
        if (Input.IsActionJustPressed(upKey)) {
            if (position < 0) {
                int sub = keyboardContainer.Columns / controls.Count;
                next = keys.Count - (controls.Count + position + 1) * sub;
            }
            else if (position >= keyboardContainer.Columns) {
                next -= keyboardContainer.Columns;
            }
        }
        else if (Input.IsActionJustPressed(downKey)) {
            if (position > -1 && position < keys.Count - keyboardContainer.Columns) {
                next += keyboardContainer.Columns;
            }
            else if (position >= keys.Count - keyboardContainer.Columns) {
                int sub = keyboardContainer.Columns / controls.Count;
                next = -((position % keyboardContainer.Columns) / sub) - 1;
            }
        }
        else if (Input.IsActionJustPressed(leftKey)) {
            if (position > -1 && position % keyboardContainer.Columns > 0) {
                --next;
            }
            else if (position < -1) {
                ++next;
            }
        }
        else if (Input.IsActionJustPressed(rightKey)) {
            if (position > -1 && (position + 1) % keyboardContainer.Columns > 0) {
                ++next;
            }
            else if (position < 0 && position > -controls.Count) {
                --next;
            }
        }

        if (next != position) {
            SwitchSelectableLabel(position, false);
            SwitchSelectableLabel(next, true);
            position = next;
        }
    }

    public void OnSelected(string val) {
        GD.Print(val);
    }

    private void SwitchSelectableLabel(int index, bool selected) {
        if (index > -1) {
            keys[index].Selected = selected;
        }
        else {
            index = -index - 1;
            controls[index].Selected = selected;
        }
    }
}
