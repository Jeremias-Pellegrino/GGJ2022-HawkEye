using Godot;
using System;

public class _2D : Control
{
    public static _2D _ { get; set; }
    public Button Play1;
    PackedScene _3d;

    public override void _Ready()
    {
        _ = this;
        Play1 = GetNode<Button>("Play1");
        _3d = ResourceLoader.Load("res://Tscn/SI2.tscn") as PackedScene;
    }
    public override void _Process(float delta)
    {
        if (Play1.Pressed)
        {
            Visible = false;
            GetParent().AddChild(_3d.Instance<_3D>());
        }
    }
}
