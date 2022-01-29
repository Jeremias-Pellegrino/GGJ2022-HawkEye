using Godot;
using System;

public class NaveBot2 : Spatial
{
    float ftime = 0, shoot = 0, f = 2;
    Bala bala;
    public void _on_Area_body_entered(Node n)
    {
        if (n is Bala b && Visible)
        {
            if (b.N!=0) {
                Gal2._.RemoveChild(b);
                Visible = false;
                if(b.N == -1)
                    Gal2._.e2.Playing = true;
                if (b.N == 1)
                    Gal2._.e1.Playing = true;
            }
        }
    }
    public override void _Ready()
    {
        bala = ((ResourceLoader.Load("res://Tscn/bala.tscn")) as PackedScene).Instance<Bala>();
        bala.SetBala(0);
    }

    public override void _Process(float delta)
    {

        if (ftime > 30 && !Visible && Gal2._.boss.vida > 0)
        {
            ftime = 0;
            Visible = true;
        }
        else if (!Visible)
        {
            if (bala.GetParentOrNull<Spatial>() != null)
                Gal2._.RemoveChild(bala);
            ftime += delta;
        }
        else {
            if (bala.GetParentOrNull<Spatial>() == null && Gal2._.boss.vida > 0)
            {
                shoot += delta;
                if (shoot >= f)
                {
                    shoot = 0;
                    bala.Translation = Translation+GetParent<Spatial>().Translation;
                    Gal2._.AddChild(bala);
                }
            }
            else if (Gal2._.boss.vida > 0)
            {
                bala.Translation += Vector3.Forward * 1.5f;
                if (bala.Translation.z < -65)
                    Gal2._.RemoveChild(bala);
            }
            else {
                if (bala.GetParentOrNull<Spatial>() != null)
                    Gal2._.RemoveChild(bala);
            }

        }
    }

    internal void SetTime(int v)
    {
        f = v;
    }
}
