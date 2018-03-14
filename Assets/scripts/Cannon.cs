using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
	public GameObject ball;
	public float launchForce;
	public Vector2 launchUnitVector = Vector2.one.normalized;

	public Transform visuals;
	public Transform indicator;
	//public Vector3 defaultPos;

	/*void Start ()
	{
		//defaultPos = transform.position;
	}*/

	void Update()
	{
		indicator.localPosition = new Vector3(launchForce/4,0,0);
		visuals.rotation = Quaternion.Euler(0, 0, Vector2.Angle(Vector2.right, launchUnitVector));
	}

	public void LaunchBall()
	{
		Instantiate(ball, transform.position, Quaternion.identity).GetComponent<Rigidbody2D>().AddForce(launchUnitVector * launchForce, ForceMode2D.Impulse);
	}
}
