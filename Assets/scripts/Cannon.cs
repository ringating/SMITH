using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
	public GameObject ball;
	public float launchForce;
	public Vector2 launchUnitVector = Vector2.one.normalized;
	//public Vector3 defaultPos;

	/*void Start ()
	{
		//defaultPos = transform.position;
	}*/

	public void LaunchBall()
	{
		Instantiate(ball, transform.position, Quaternion.identity).GetComponent<Rigidbody2D>().AddForce(launchUnitVector * launchForce, ForceMode2D.Impulse);
	}
}
