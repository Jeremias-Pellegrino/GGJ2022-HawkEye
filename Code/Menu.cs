using Godot;
using System;

public class Menu : Control
{
    public static Menu _ { get; set; }
    public TextureButton ButtonPlay1, ButtonPlay2; public Button Skip1, Skip2, atras1, atras2;
    PackedScene GameSI, GameGal2;
    public AudioStreamPlayer AStream;
    VideoPlayer v1,v2;
    float timeVideo1 = -1, timeVideo2 = -1;
    Control CG1, CG2;
    bool presedG1 = false, presedG2 = false;
    public override void _Ready()
    {
        _ = this;
        ButtonPlay1 = GetNode<TextureButton>("Play1");
        ButtonPlay2 = GetNode<TextureButton>("Play2");
        GameSI = ResourceLoader.Load("res://Tscn/SI2.tscn") as PackedScene;
        GameGal2 = ResourceLoader.Load("res://Tscn/Gal2.tscn") as PackedScene;
        AStream = GetNode<AudioStreamPlayer>("AudioMenu");
        CG1 = GetParent().GetNode<Control>("CG1");
        CG2 = GetParent().GetNode<Control>("CG2");
        Skip1 = CG1.GetNode<Button>("SK");
        Skip2 = CG2.GetNode<Button>("SK");
        atras1 = CG1.GetNode<Button>("a");
        atras2 = CG2.GetNode<Button>("a");
        v1 = CG1.GetNode<VideoPlayer>("VideoPlayer");
        v2 = CG2.GetNode<VideoPlayer>("VideoPlayer");
    }
    public override void _Process(float delta)
    {
        if (ButtonPlay1.Pressed)
        {
            presedG1 = true;
        }
        if (!ButtonPlay1.Pressed && presedG1)
        {
            presedG1 = false;
            Visible = false;
            AStream.Playing = false;
            CG1.Visible = true;
            v1.Play();
            timeVideo1 = 0;
        }

        if (timeVideo1 > -1)
            timeVideo1 += delta;
        if (timeVideo1 > 50)
        {
            CG1.Visible = false;
            v1.Stop();
            GetParent().AddChild(GameSI.Instance<SI2>());
            timeVideo1 = -1;
        }
        if (Skip1.Pressed)
        {
            timeVideo1 = -1;
            CG1.Visible = false;
            v1.Stop();
            GetParent().AddChild(GameSI.Instance<SI2>());
        }
        if (atras1.Pressed)
        {
            timeVideo1 = -1;
            v1.Stop();
            CG1.Visible = false;
            Visible = true;
            AStream.Playing = true;
        }
        if (ButtonPlay2.Pressed)
        {
            presedG2 = true;
        }
        if (!ButtonPlay2.Pressed && presedG2)
        {
            presedG2 = false;
            v2.Play();
            Visible = false;
            AStream.Playing = false;
            CG2.Visible = true;
            timeVideo2 = 0;
        }
        if (timeVideo2 > -1)
            timeVideo2 += delta;
        if (timeVideo2 > 50)
        {
            CG2.Visible = false;
            v1.Stop();
            GetParent().AddChild(GameGal2.Instance<Gal2>());
            timeVideo2 = -1;
        }
        if (Skip2.Pressed)
        {
            v2.Stop();
            CG2.Visible = false;
            GetParent().AddChild(GameGal2.Instance<Gal2>());
            timeVideo2 = -1;
        }
        if (atras2.Pressed)
        {
            v2.Stop();
            CG2.Visible = false;
            Visible = true;
            AStream.Playing = true;
            timeVideo2 = -1;
        }
    }
}
