using Godot;
using System;

public class Gal2 : Spatial
{
    public static Gal2 _ { get; set; }
    Spatial e;
    public Area PJ1, PJ2;
    public Bala bala1, bala2;
    bool rib1 = false, rib2 = false;
    float balaSpeed1 = 2, balaSpeed2 = 2, PJSpeed1 = 1, PJSpeed2 = 1;
    public Boss boss;
    public int vida1=10;
    public int vida2=10;
    bool fin = false;
    public AudioStreamPlayer fx1, fx2, e1,e2,a1,a2,eb;
    public void _on_GameOver_finished()
    {
        Menu._.Visible = true;
        Menu._.AStream.Playing = true;
        QueueFree();
    }

    public void _on_PJ1_body_entered(Node n)
    {
        if (n is Bala b && PJ1.Visible)
        {
            RemoveChild(b);
            vida1--;
            if(vida1>0)
                a1.Playing = true;
        }

    }
    public void _on_PJ2_body_entered(Node n)
    {
        if (n is Bala b && PJ2.Visible)
        {
            RemoveChild(b);
            vida2--;
            if (vida2 > 0)
                a2.Playing = true;
        }
    }
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
        _ = this;
        e = GetNode<Spatial>("Edificios");
        PJ1 = GetNode<Area>("PJ1");
        PJ2 = GetNode<Area>("PJ2");
        bala1 = ((ResourceLoader.Load("res://Tscn/bala.tscn")) as PackedScene).Instance<Bala>();
        bala2 = ((ResourceLoader.Load("res://Tscn/bala.tscn")) as PackedScene).Instance<Bala>();
        bala1.SetBala(1);
        bala2.SetBala(-1);
        boss = GetNode<Boss>("BOSS");
        fx1 = GetNode<AudioStreamPlayer>("FX1");
        fx2 = GetNode<AudioStreamPlayer>("FX2");
        e1 = GetNode<AudioStreamPlayer>("NGE1");
        e2 = GetNode<AudioStreamPlayer>("NGE2");
        a1 = GetNode<AudioStreamPlayer>("NGA1");
        a2 = GetNode<AudioStreamPlayer>("NGA2");
        eb = GetNode<AudioStreamPlayer>("NGEB");
    }
    public override void _Process(float delta)
    {
        if (boss.vida > 0)
        {
            if (vida1 <= 0 && vida2 <= 0)
            {
                if (!fin)
                {
                    GetNode<AudioStreamPlayer>("GameOver").Playing = true;
                    fin = true;
                }
            }
            else
            {
                e.Translation += Vector3.Forward * 30 * delta;
                if (e.Translation.z <= -500)
                {
                    e.Translation -= Vector3.Forward * 400;
                }
                if (vida1 > 0)
                {
                    if (Input.IsActionPressed("UP1") && PJ1.Translation.y < 45)
                    {
                        PJ1.Translation += Vector3.Up * PJSpeed1;
                    }
                    if (Input.IsActionPressed("DOWN1") && PJ1.Translation.y > -35)
                    {
                        PJ1.Translation += Vector3.Down * PJSpeed1;
                    }
                    if (Input.IsActionPressed("LE1") && PJ1.Translation.z > -60)
                    {
                        PJ1.Translation += Vector3.Forward * PJSpeed1;
                    }
                    if (Input.IsActionPressed("RI1") && PJ1.Translation.z < 0)
                    {
                        PJ1.Translation += Vector3.Back * PJSpeed1;
                    }
                }
                else
                {
                    if (PJ1.Visible)
                        fx1.Playing = true;
                    PJ1.Visible = false;
                    if (bala1.GetParentOrNull<Spatial>() != null)
                        RemoveChild(bala1);
                }
                if (vida2 > 0)
                {
                    if (Input.IsActionPressed("UP2") && PJ2.Translation.y < 45)
                    {
                        PJ2.Translation += Vector3.Up * PJSpeed2;
                    }
                    if (Input.IsActionPressed("DOWN2") && PJ2.Translation.y > -35)
                    {
                        PJ2.Translation += Vector3.Down * PJSpeed2;
                    }
                    if (Input.IsActionPressed("LE2") && PJ2.Translation.z > -60)
                    {
                        PJ2.Translation += Vector3.Forward * PJSpeed2;
                    }
                    if (Input.IsActionPressed("RI2") && PJ2.Translation.z < 0)
                    {
                        PJ2.Translation += Vector3.Back * PJSpeed2;
                    }
                }
                else
                {
                    if (PJ2.Visible)
                        fx2.Playing = true;
                    PJ2.Visible = false;
                    if(bala2.GetParentOrNull<Spatial>() != null)
                        RemoveChild(bala2);
                }
                if (rib1)
                {
                    rib1 = false;
                    AddChild(bala1);
                }
                if (bala1.GetParentOrNull<Spatial>() == null && PJ1.Visible)
                {
                    if (Input.IsActionPressed("FIRE1"))
                    {

                        bala1.Translation = PJ1.Translation + Vector3.Back * 4;
                        rib1 = true;
                    }
                }
                else if (PJ1.Visible)
                {
                    bala1.Translation += Vector3.Back * balaSpeed1;
                    if (Mathf.Abs(bala1.Translation.z) > 80)
                    {
                        RemoveChild(bala1);
                    }
                }
                if (rib2)
                {
                    rib2 = false;
                    AddChild(bala2);
                }
                if (bala2.GetParentOrNull<Spatial>() == null && PJ2.Visible)
                {
                    if (Input.IsActionPressed("FIRE2"))
                    {

                        bala2.Translation = PJ2.Translation + Vector3.Back * 4;
                        rib2 = true;
                    }
                }
                else if (PJ2.Visible)
                {
                    bala2.Translation += Vector3.Back * balaSpeed2;
                    if (Mathf.Abs(bala2.Translation.z) > 80)
                    {
                        RemoveChild(bala2);
                    }
                }
            }
        }
        else
        {
            if (!fin)
            {
                GetNode<AudioStreamPlayer>("Victory").Playing = true;
                fin = true;
            }
        }
    }
}
