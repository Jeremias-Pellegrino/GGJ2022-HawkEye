using Godot;
using System;

public class FIN : Spatial
{
    Spatial pir;
    float f = 0;
    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventKey k)
        {
            if (k.Scancode == 16777217)//escape
            {
                Menu._.Visible = true;
                Menu._.AStream.Playing = true;
                QueueFree();
            }
        }
    }
    public override void _Ready()
    {
        pir=GetNode<Spatial>("P");
    }

    public override void _Process(float delta)
    {
        f += delta/180*3.1415f*30;
        pir.Translation = new Vector3(23,15-Mathf.Sin(f)*5,-2.4f);
    }
}
