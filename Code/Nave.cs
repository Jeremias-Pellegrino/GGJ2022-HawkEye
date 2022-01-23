using Godot;
using System;

public class Nave : Area
{
	int team;
	
	private Team assignTeam(){
		//Usamos la orientacion del parent para saber si la nave es aliada o enemiga
		switch(((int)GetParent<Spatial>().Scale.z)){
			case 1:
				return Team.teamA;
			case -1:
				return Team.teamB;
		}
	}

	public override void _Ready()
	{
		team = assignTeam();
	}

	public void _on_body_entered(Node n)
	{
		if (n is Bala incomingBullet && (incomingBullet.team != team) && Visible)
			{
				incomingBullet.GetParent().RemoveChild(b);
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
