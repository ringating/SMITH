using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tester : MonoBehaviour
{
	public GameState gs;

	public Ball ball;
	public Vector2 launchVel;

	void Update ()
	{
		/*if (Input.GetButton("Fire1"))
		{
			ball.Launch(launchVel);
		}*/

		if (Input.GetKeyDown("a")) { gs.ChangeState(); }
		if (Input.GetKeyDown("space")) { gs.cannon.LaunchBall(); }
	}
}
