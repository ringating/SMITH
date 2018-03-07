using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
	public GameState gs;

	public float speed;	// this is just for visualizing in the editor
	public float minSpeed;
	public float minSpeedTimerToStop = 2f;

	private float speedTimer;
	private Rigidbody2D rigid;

	// Use this for initialization
	void Start ()
	{
		rigid = GetComponent<Rigidbody2D>();
		speedTimer = minSpeedTimerToStop;

		gs = (GameState)FindObjectOfType(typeof(GameState));

		if (gs.ball == null) { gs.ball = this; } else { Destroy(this.gameObject); }
	}

	void Update()
	{
		speed = rigid.velocity.magnitude;

		if (rigid.velocity.magnitude < minSpeed)
		{
			if (speedTimer > 0)
			{
				speedTimer -= Time.deltaTime;
			}
			else
			{
				rigid.velocity = Vector3.zero;
				rigid.Sleep();
			}
		}
		else
		{
			speedTimer = minSpeedTimerToStop;
		}

		// out of bounds checking
		if (transform.position.y > 10 || transform.position.y < -10 || transform.position.x > 17 || transform.position.x < -17)
		{
			gs.success = false;
			Destroy(this.gameObject);
			gs.ChangeState();
			
		}
	}

	public void Launch(Vector2 vel)
	{
		rigid.velocity = vel;
	}
}
