using Godot;
using System;

public class SI2 : Spatial
{
    public static SI2 _ { get; set; }
    public Area PJ1, PJ2;
    public Bala bala1, bala2;
    public int vi1 = 2, vi2 = 2;
    public bool gameFin = false, reset = true;
    public Spatial naves1, naves2;
    float timeNave1 = 0, timeNave2 = 0, balaSpeed1 = 2, balaSpeed2 = 2, PJSpeed1 = 1, PJSpeed2 = 1;
    public AudioStreamPlayer AStream, AStreamMuerte, AStreamFx1, AStreamFx2, fxon,fxoff1,fxoff2;
    Vector3 DireccionNaves1, DireccionNaves2, _DireccionNaves1, _DireccionNaves2;
    Buff bf1, bf2;
    float TimeToBuff = 0;
    Sprite3D sp3D;
    bool rib1 = false, rib2 = false;
    RandomNumberGenerator r = new RandomNumberGenerator();
    VideoPlayer vTemp = new VideoPlayer();
    public override void _Ready()
    {
        _ = this;
        r.Randomize();
        DireccionNaves1 = Vector3.Up;
        DireccionNaves2 = Vector3.Up;
        _DireccionNaves1 = Vector3.Up;
        _DireccionNaves2 = Vector3.Up;
        PJ1 = GetNode<Area>("PJ1");
        PJ2 = GetNode<Area>("PJ2");
        sp3D = GetNode<Sprite3D>("Sprite3D");
        sp3D.AddChild(vTemp);
        vTemp.Stream = ResourceLoader.Load<VideoStream>("res://Assets/music-sfx/SAVE THE EARTH.ogv");
        vTemp.Play();
        AStream = GetNode<AudioStreamPlayer>("AudioSI2");
        AStreamMuerte = GetNode<AudioStreamPlayer>("AudioMuerto");
        AStreamFx1 = GetNode<AudioStreamPlayer>("AudioFXNave1");
        AStreamFx2 = GetNode<AudioStreamPlayer>("AudioFXNave2");
        fxon = GetNode<AudioStreamPlayer>("AudioBuffOn");
        fxoff1 = GetNode<AudioStreamPlayer>("AudioBuffOff1");
        fxoff2 = GetNode<AudioStreamPlayer>("AudioBuffOff2");
        bf1 = GetNode<Buff>("Buff1");
        bf2 = GetNode<Buff>("Buff2");
        bala1 = ((ResourceLoader.Load("res://Tscn/bala.tscn")) as PackedScene).Instance<Bala>();
        bala2 = ((ResourceLoader.Load("res://Tscn/bala.tscn")) as PackedScene).Instance<Bala>();
        naves1 = ((ResourceLoader.Load("res://Tscn/Naves.tscn")) as PackedScene).Instance<Spatial>();
        naves2 = ((ResourceLoader.Load("res://Tscn/Naves.tscn")) as PackedScene).Instance<Spatial>();
        naves2.Translation = new Vector3(0, 0, 50);
        naves1.Translation = new Vector3(0, 0, -50);
        naves2.Scale = new Vector3(1, 1, -1);
        bala1.SetBala(1);
        bala2.SetBala(-1);
        AddChild(naves1);
        AddChild(naves2);
    }

    internal void MoveNave(int e, Vector3 v)
    {
        if (e > 0)
        {
            DireccionNaves1 = v;

        }
        else {
            DireccionNaves2 = v;
        }
    }
    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventKey k)
        {
            reset = false;
            if (k.Scancode == 16777217)//escape
            {
                Menu._.Visible = true;
                Menu._.AStream.Playing = true;
                QueueFree();
            }
        }
    }
    public void _on_PJ1_body_entered(Node n)
    {
        if (n is Bala) {
            RemoveChild(bala2);
            vi1--;
            if (vi1 > 0)
                AStreamFx1.Playing = true;
        }
        if (n is Buff bf)
        {
            if (bf.Visible) {
                fxoff1.Playing = true;
                bf.Visible = false;
                Buff1(bf);
            }
        }
    }

    private void Buff1(Buff bf)
    {
        switch (bf.tipo)
        {
            case 1:
                balaSpeed1 = 4;
                break;
            case 2:
                PJSpeed1 = 2;
                break;
        }
    }

    public void _on_PJ2_body_entered(Node n)
    {
        if (n is Bala)
        {
            RemoveChild(bala1);
            vi2--;
            if (vi2 > 0)
                AStreamFx2.Playing = true;
        }
        if (n is Buff bf)
        {
            if (bf.Visible)
            {
                fxoff2.Playing = true;
                bf.Visible = false;
                Buff2(bf);
            }
        }


    }

    private void Buff2(Buff bf)
    {
        switch (bf.tipo)
        {
            case 1:
                balaSpeed2 = 4;
                break;
            case 2:
                PJSpeed2 = 2;
                break;
        }
    }

    public override void _Process(float delta)
    {
        if (!gameFin)
        {
            sp3D.Texture = vTemp.GetVideoTexture();
            if ((vi2 > 0 && vi1 <= 0) || (naves1.Translation.z != PJ2.Translation.z && naves2.Translation.z == PJ1.Translation.z))
            {
                Fin("PJ2 gana");
            }
            else if ((vi2 <= 0 && vi1 > 0) || (naves1.Translation.z == PJ2.Translation.z && naves2.Translation.z != PJ1.Translation.z))
            {
                Fin("PJ1 gana");
            }
            else if ((vi2 <= 0 && vi1 <= 0) || (naves1.Translation.z == PJ2.Translation.z && naves2.Translation.z == PJ1.Translation.z))
            {
                Fin("Empate");
            }
            //1
            if (Input.IsActionPressed("UP1") && PJ1.Translation.y < 45)
            {
                PJ1.Translation += Vector3.Up * PJSpeed1;
            }
            if (Input.IsActionPressed("DOWN1") && PJ1.Translation.y > -45)
            {
                PJ1.Translation += Vector3.Down * PJSpeed1;
            }
            if (rib1)
            {
                rib1 = false;
                AddChild(bala1);
            }
            if (bala1.GetParentOrNull<Spatial>() == null)
            {
                if (Input.IsActionPressed("FIRE1"))
                {
                    bala1.Translation = PJ1.Translation + Vector3.Back * 4;
                    rib1 = true;
                }
            }
            else
            {
                bala1.Translation += Vector3.Back * balaSpeed1;
                if (Mathf.Abs(bala1.Translation.z) > 70)
                {
                    RemoveChild(bala1);
                }
            }
            //2
            if (Input.IsActionPressed("UP2") && PJ2.Translation.y < 45)
            {
                PJ2.Translation += Vector3.Up * PJSpeed2;
            }
            if (Input.IsActionPressed("DOWN2") && PJ2.Translation.y > -45)
            {
                PJ2.Translation += Vector3.Down * PJSpeed2;
            }
            if (rib2)
            {
                rib2 = false;
                AddChild(bala2);
            }
            if (bala2.GetParentOrNull<Spatial>() == null)
            {
                if (Input.IsActionPressed("FIRE2"))
                {

                    bala2.Translation = PJ2.Translation + Vector3.Forward * 4;
                    rib2 = true;
                }
            }
            else
            {
                bala2.Translation += Vector3.Forward * balaSpeed2;
                if (Mathf.Abs(bala2.Translation.z) > 70)
                {
                    RemoveChild(bala2);
                }
            }

            TimeToBuff += delta;
            if (TimeToBuff > 10.1f) {
                TimeToBuff = 0;
                int i = r.RandiRange(0, 9);
                if (i % 2 == 0)
                {
                    bf1.tipo = r.RandiRange(1, 2);
                    bf1.Visible = true;
                }
                else
                {
                    bf2.tipo = r.RandiRange(1, 2);
                    bf2.Visible = true;
                }
                fxon.Playing = true;
            }

            if (balaSpeed1 > 2)
                balaSpeed1 -= 0.05f * delta;//40 seg en regresar a velocida normal (4-=0.05*delta)=2 despues de 40s
            if (balaSpeed2 > 2)
                balaSpeed2 -= 0.05f * delta;
            if (PJSpeed1 > 1)
                PJSpeed1 -= 0.05f * delta;//20 seg en regresar a velocida normal (2-=0.05*delta)=1 despues de 20s
            if (PJSpeed2 > 1)
                PJSpeed2 -= 0.05f * delta;

            timeNave1 += delta;
            if (timeNave1 > 0.65f * (Mathf.Abs(PJ2.Translation.z - naves1.Translation.z) / 110))
            {
                timeNave1 = 0;
                naves1.Translation += DireccionNaves1 * 2.5f;
                if (!DireccionNaves1.Equals(_DireccionNaves1))
                {
                    _DireccionNaves1 = DireccionNaves1;
                    naves1.Translation += Vector3.Back * 10;
                }
            }
            timeNave2 += delta;
            if (timeNave2 > 0.65f * (Mathf.Abs(PJ1.Translation.z - naves2.Translation.z) / 110))
            {
                timeNave2 = 0;
                naves2.Translation += DireccionNaves2 * 2.5f;
                if (!DireccionNaves2.Equals(_DireccionNaves2))
                {
                    _DireccionNaves2 = DireccionNaves2;
                    naves2.Translation += Vector3.Forward * 10;
                }
            }
        }
        else
        {
            if (Input.IsActionPressed("R"))
            {
                bf1.Visible = false;
                bf2.Visible = false;
                gameFin = false;
                reset = true;
                AStream.Playing = true;
                vi1 = 2;
                vi2 = 2;
                DireccionNaves1 = Vector3.Up;
                DireccionNaves2 = Vector3.Up;
                _DireccionNaves1 = Vector3.Up;
                _DireccionNaves2 = Vector3.Up;
                PJ1.Translation = new Vector3(0, 0, -60);
                PJ2.Translation = new Vector3(0, 0, 60);
                bala1.GetNode<AudioStreamPlayer>("AudioBala").Playing = true;
                bala2.GetNode<AudioStreamPlayer>("AudioBala").Playing = true;
                if (bala1.GetParentOrNull<Spatial>() != null)
                    RemoveChild(bala1);
                if (bala2.GetParentOrNull<Spatial>() != null)
                    RemoveChild(bala2);
                naves1.Translation = new Vector3(0, 0, -50);
                naves2.Translation = new Vector3(0, 0, 50);
            }
        }
    }
    public void _on_GameOver_finished()
    {
        Menu._.Visible = true;
        Menu._.AStream.Playing = true;
        QueueFree();
    }

    private void Fin(string s)
    {
        AStream.Playing = false;
        AStreamMuerte.Playing = true;
        bala1.GetNode<AudioStreamPlayer>("AudioBala").Playing = false;
        bala2.GetNode<AudioStreamPlayer>("AudioBala").Playing = false;
        gameFin = true;
        GD.Print(s);
    }
}