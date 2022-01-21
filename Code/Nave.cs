using Godot;
using System;

public class Nave : Area
{
    int e = 0;
    public override void _Ready()
    {
        e = ((int)GetParent<Spatial>().Scale.z);
    }

    public void _on_body_entered(Node n)
    {
        if (n is Bala b)
        {
            if (b.N != e && Visible)
            {
                b.GetParent().RemoveChild(b);
                Visible = false;
            }
        }
    }
    public override void _Process(float delta)
    {
        if (GetParent<Spatial>().Translation.y + Translation.y > 45 && Visible)
        {

            _3D._.MoveNave(e, Vector3.Down);
        }
        if (GetParent<Spatial>().Translation.y + Translation.y < -45 && Visible)
        {
            _3D._.MoveNave(e, Vector3.Up);
        }
        if (_3D._.reset && !Visible)
            Visible = true;
    }
}
