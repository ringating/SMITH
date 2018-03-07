using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basket : MonoBehaviour
{
	//public Vector3 defaultPos;
	public GameState gs;

	public bool success = false;

	private bool enteredThroughTop;

	void Start()
	{
		//defaultPos = transform.position;
		success = false;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.transform.position.y > transform.position.y && other.GetComponent<Ball>() != null)
		{
			enteredThroughTop = true;
		}
		else
		{
			enteredThroughTop = false;
		}
	}

	void OnTriggerStay2D(Collider2D col)
	{
		if(success != true) CheckSuccess(col);
	}

	void OnTriggerExit2D(Collider2D col)
	{
		if (success != true) CheckSuccess(col);
	}

	void CheckSuccess(Collider2D ball)
	{
		if (ball.GetComponent<Ball>() != null)
		{ 
			if (enteredThroughTop)
			{
				if (ball.transform.position.y < transform.position.y)
				{
					success = true;
					Debug.Log("success, top to bottom");

					gs.success = true;
					Destroy(ball.gameObject);
					gs.ChangeState();
				}
			}
			else
			{
				if (ball.transform.position.y > transform.position.y)
				{
					success = true;
					Debug.Log("success, bottom to top");

					gs.success = true;
					Destroy(ball.gameObject);
					gs.ChangeState();
				}
			}
		}
	}
}
