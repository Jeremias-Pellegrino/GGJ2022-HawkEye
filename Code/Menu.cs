using Godot;
using System;

public class Menu : Control
{
    public static Menu _ { get; set; }
    public Button ButtonPlay1;
    PackedScene GameSI;

    public override void _Ready()
    {
        _ = this;
        ButtonPlay1 = GetNode<Button>("Play1");
        GameSI = ResourceLoader.Load("res://Tscn/SI2.tscn") as PackedScene;
    }
    public override void _Process(float delta)
    {
        if (ButtonPlay1.Pressed)
        {
            Visible = false;
            GetParent().AddChild(GameSI.Instance<SI2>());
        }
    }
}
