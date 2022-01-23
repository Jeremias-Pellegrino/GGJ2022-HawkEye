using Godot;
using System;

public class Nave : Area
{
	Team team;
	
	private Team assignTeam(){
		//Usamos la orientacion del parent para saber si la nave es aliada o enemiga
		switch(((int)GetParent<Spatial>().Scale.z)){
			case 1:
				return Team.teamA;
			case -1:
				return Team.teamB;
			default:
				return Team.neutral;
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
			incomingBullet.GetParent().RemoveChild(incomingBullet);
			Visible = false;
		}
	}
	public override void _Process(float delta)
	{
		if (GetParent<Spatial>().Translation.y + Translation.y > 45 && Visible)
		{
			SI2._.MoveNave((int) team, Vector3.Down);
		}
		if (GetParent<Spatial>().Translation.y + Translation.y < -45 && Visible)
		{
			SI2._.MoveNave((int) team, Vector3.Up);
		}
		if (SI2._.reset && !Visible)
			Visible = true;
	}
}
