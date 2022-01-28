using Godot;
using System;

public class Bala : KinematicBody
{
    public int N = 0;

    internal void SetBala(int v)
    {
        N = v;
        if (v < 0)
        {
            GetNode<OmniLight>("Light").LightColor = Color.Color8(0, 0, 255);
            GetNode<MeshInstance>("Mesh").MaterialOverride = new SpatialMaterial() { AlbedoColor = Color.Color8(0, 0, 255) };
        }
        else if (v > 0)
        {
            GetNode<OmniLight>("Light").LightColor = Color.Color8(0, 255, 0);
            GetNode<MeshInstance>("Mesh").MaterialOverride = new SpatialMaterial() { AlbedoColor = Color.Color8(0, 255, 0) };
        }
        else
        {
            GetNode<OmniLight>("Light").LightColor = Color.Color8( 255, 0, 0);
            GetNode<MeshInstance>("Mesh").MaterialOverride = new SpatialMaterial() { AlbedoColor = Color.Color8(255, 0, 0) };
            GetNode<AudioStreamPlayer>("AudioBala").Autoplay = false;
        }
    }
}
