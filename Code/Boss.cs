using Godot;
using System;

public class Boss : Spatial
{
    public NaveBot2[] navesBot1 = new NaveBot2[14], navesBot2 = new NaveBot2[14], navesBot3 = new NaveBot2[14], navesBot4 = new NaveBot2[14], navesBot5 = new NaveBot2[14], navesBot6 = new NaveBot2[14];
    public Spatial node1,node2, node3, node4, node5, node6;
    Vector3 dir = Vector3.Up;
    public int vida =50;
    public void _on_Area_body_entered(Node n)
    {
        if (n is Bala b && Visible)
        {
            Gal2._.RemoveChild(b);
            vida--;
            Gal2._.eb.Playing = true;
        }
    }
    public override void _Ready()
    {
        node1 = GetNode<Spatial>("N1");
        node2 = GetNode<Spatial>("N2");
        node3 = GetNode<Spatial>("N3");
        node4 = GetNode<Spatial>("N4");
        node5 = GetNode<Spatial>("N5");
        node6 = GetNode<Spatial>("N6");
        for (int i = 0; i < navesBot1.Length; i++)
        {
            navesBot1[i] = node1.GetNode<NaveBot2>("Bot" + (1 + i));
            navesBot1[i].SetTime(5);
        }
        for (int i = 0; i < navesBot2.Length; i++)
        {
            navesBot2[i] = node2.GetNode<NaveBot2>("Bot" + (1 + i));
            navesBot2[i].SetTime(5);
        }
        for (int i = 0; i < navesBot3.Length; i++)
        {
            navesBot3[i] = node3.GetNode<NaveBot2>("Bot" + (1 + i));
            navesBot3[i].SetTime(11);
        }
        for (int i = 0; i < navesBot4.Length; i++)
        {
            navesBot4[i] = node4.GetNode<NaveBot2>("Bot" + (1 + i));
            navesBot4[i].SetTime(11);
        }
        for (int i = 0; i < navesBot5.Length; i++)
        {
            navesBot5[i] = node5.GetNode<NaveBot2>("Bot" + (1 + i));
            navesBot5[i].SetTime(17);
        }
        for (int i = 0; i < navesBot6.Length; i++)
        {
            navesBot6[i] = node6.GetNode<NaveBot2>("Bot" + (1 + i));
            navesBot6[i].SetTime(17);
        }
    }

    public override void _Process(float delta)
    {
        if (vida > 0)
        {
            node1.RotationDegrees += Vector3.Right * 15 * delta;
            for (int i = 0; i < navesBot1.Length; i++)
            {
                navesBot1[i].GlobalRotate(Vector3.Left, 15 * delta / 180f * Mathf.Pi);
            }
            node2.RotationDegrees += Vector3.Left * 15 * delta;
            for (int i = 0; i < navesBot2.Length; i++)
            {
                navesBot2[i].GlobalRotate(Vector3.Right, 15 * delta / 180f * Mathf.Pi);
            }
            node3.RotationDegrees += Vector3.Right * 15 * delta;
            for (int i = 0; i < navesBot3.Length; i++)
            {
                navesBot3[i].GlobalRotate(Vector3.Left, 15 * delta / 180f * Mathf.Pi);
            }
            node4.RotationDegrees += Vector3.Left * 15 * delta;
            for (int i = 0; i < navesBot4.Length; i++)
            {
                navesBot4[i].GlobalRotate(Vector3.Right, 15 * delta / 180f * Mathf.Pi);
            }
            node5.RotationDegrees += Vector3.Right * 15 * delta;
            for (int i = 0; i < navesBot5.Length; i++)
            {
                navesBot5[i].GlobalRotate(Vector3.Left, 15 * delta / 180f * Mathf.Pi);
            }
            node6.RotationDegrees += Vector3.Left * 15 * delta;
            for (int i = 0; i < navesBot6.Length; i++)
            {
                navesBot6[i].GlobalRotate(Vector3.Right, 15 * delta / 180f * Mathf.Pi);
            }
            if (Translation.z > 30)
                Translation += Vector3.Forward * 20 * delta;
            else
                Translation += dir * delta * 10;
            if (Mathf.Abs(Translation.y) > 15)
            {
                dir = -dir;
            }
        }
    }
}
