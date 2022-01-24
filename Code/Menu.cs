using Godot;
using System;

public class Menu : Control
{
    public static Menu _ { get; set; }
    public Button ButtonPlay1;
    PackedScene GameSI;
    public AudioStreamPlayer AStream;
    public override void _Ready()
    {
        _ = this;
        ButtonPlay1 = GetNode<Button>("Play1");
        GameSI = ResourceLoader.Load("res://Tscn/SI2.tscn") as PackedScene;
        AStream = GetNode<AudioStreamPlayer>("AudioMenu");
    }
    public override void _Process(float delta)
    {
        if (ButtonPlay1.Pressed)
        {
            ButtonPlay1.Visible = false;
            AStream.Playing = false;
            GetParent().AddChild(GameSI.Instance<SI2>());
        }
    }
}
