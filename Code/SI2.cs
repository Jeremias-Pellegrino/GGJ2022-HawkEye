using Godot;
using System;

public class SI2 : Spatial
{
	public static SI2 _ { get; set; }
	public Area PJ1,PJ2;
	public Bala bala1,bala2;
	public int vi1 = 2, vi2= 2;
	public bool gameFin = false , reset = true;
	public Spatial naves1, naves2;
	float timeNaves = 0;
	Vector3 DireccionNaves1, DireccionNaves2, _DireccionNaves1, _DireccionNaves2;

	public override void _Ready()
	{
		_ = this;
		DireccionNaves1 = Vector3.Up;
		DireccionNaves2 = Vector3.Up;
		_DireccionNaves1 = Vector3.Up;
		_DireccionNaves2 = Vector3.Up;
		PJ1 = GetNode<Area>("PJ1");
		PJ2 = GetNode<Area>("PJ2");
		bala1 = ((ResourceLoader.Load("res://Tscn/bala.tscn")) as PackedScene).Instance<Bala>();
		bala2 = ((ResourceLoader.Load("res://Tscn/bala.tscn")) as PackedScene).Instance<Bala>();
		naves1 = ((ResourceLoader.Load("res://Tscn/Naves.tscn")) as PackedScene).Instance<Spatial>();
		naves2 = ((ResourceLoader.Load("res://Tscn/Naves.tscn")) as PackedScene).Instance<Spatial>();
		naves2.Translation = new Vector3(0, 0, 50);
		naves1.Translation = new Vector3(0, 0, -50);
		naves2.Scale = new Vector3(1, 1, -1);
		bala1.N = 1;
		bala2.N = -1;
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
				QueueFree();
			}
		}
	}
	public void _on_PJ1_body_entered(Node n)
	{
		if (n is Bala b) {
			RemoveChild(b);
			vi1--;
		}
		if (n is Nave na)
		{
			vi1--;
			na.Visible = false;
		}
	}
	public void _on_PJ2_body_entered(Node n)
	{
		if (n is Bala b)
		{
			RemoveChild(b);
			vi2--;
		}
		if (n is Nave na) 
		{
			vi2--;
			na.Visible = false;
		}


	}
	public override void _Process(float delta)
	{
		if (!gameFin)
		{

			if (vi2 <= 0 || vi1<=0)
				gameFin=true;
			//1
			if (Input.IsActionPressed("UP1") && PJ1.Translation.y < 45)
			{
				PJ1.Translation += Vector3.Up;
			}
			if (Input.IsActionPressed("DOWN1") && PJ1.Translation.y > -45)
			{
				PJ1.Translation += Vector3.Down;
			}

			if (bala1.GetParentOrNull<Spatial>() == null)
			{
				if (Input.IsActionPressed("FIRE1"))
				{
					bala1.Translation = PJ1.Translation + Vector3.Back * 4;
					AddChild(bala1);
				}
			}
			else
			{
				bala1.Translation += Vector3.Back * 2;
				if (Mathf.Abs(bala1.Translation.z) > 65)
				{
					RemoveChild(bala1);
				}
			}
			//2
			if (Input.IsActionPressed("UP2") && PJ2.Translation.y < 45)
			{
				PJ2.Translation += Vector3.Up;
			}
			if (Input.IsActionPressed("DOWN2") && PJ2.Translation.y > -45)
			{
				PJ2.Translation += Vector3.Down;
			}
			if (bala2.GetParentOrNull<Spatial>() == null)
			{
				if (Input.IsActionPressed("FIRE2"))
				{

					bala2.Translation = PJ2.Translation + Vector3.Forward * 4;
					AddChild(bala2);
				}
			}
			else
			{
				bala2.Translation += Vector3.Forward*2;
				if (Mathf.Abs(bala2.Translation.z) > 65)
				{
					RemoveChild(bala2);
				}
			}
			timeNaves += delta;
			if (timeNaves > 0.5f) {
				timeNaves = 0;
				naves1.Translation += DireccionNaves1 * 1;
				if (!DireccionNaves1.Equals(_DireccionNaves1))
				{
					_DireccionNaves1 = DireccionNaves1;
					naves1.Translation += Vector3.Back * 5;
				}
			
				naves2.Translation += DireccionNaves2 * 1;
				if (!DireccionNaves2.Equals(_DireccionNaves2))
				{
					_DireccionNaves2 = DireccionNaves2;
					naves2.Translation += Vector3.Forward * 5;
				}
			}
			
		}
		else 
		{
			if (Input.IsActionPressed("R"))
			{
				gameFin = false;
				reset = true;
				vi1=2;
				vi2=2;
				DireccionNaves1 = Vector3.Up;
				DireccionNaves2 = Vector3.Up;
				_DireccionNaves1 = Vector3.Up;
				_DireccionNaves2 = Vector3.Up;
				PJ1.Translation = new Vector3(0, 0, -60);
				PJ2.Translation = new Vector3(0, 0, 60);
				if (bala1.GetParentOrNull<Spatial>() != null)
					RemoveChild(bala1);
				if (bala2.GetParentOrNull<Spatial>() != null)
					RemoveChild(bala2);
				naves1.Translation = new Vector3(0, 0, -50);
				naves2.Translation = new Vector3(0, 0, 50);
			}
		}
	}
}
