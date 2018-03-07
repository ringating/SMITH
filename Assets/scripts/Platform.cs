using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
	public Material tempMat;

	public GameObject platformBase;
	public GameObject platformCap1;
	public GameObject platformCap2;
	public bool temporary;
	public float tempTimer = 1f;

	public BoxCollider2D colBase;
	public CircleCollider2D colCap1;
	public CircleCollider2D colCap2;

	private float timer = 0f;

	void Update ()
	{
		/*if (placing)
		{
			// adjust x scale of base to correct length, accounting for this.transform.localScale
			platformBase.transform.localScale = new Vector3(length/transform.localScale.x, platformBase.transform.localScale.y, platformBase.transform.localScale.z);
			// adjust local x pos of base so its end stays rooted to cap1
			platformBase.transform.localPosition = new Vector3(length/transform.localScale.x/2,0,0);
			// adjust position of platformCap2 to correct length
			platformCap2.transform.localPosition = new Vector3(length / transform.localScale.x,0,0);*/

		if (temporary)
		{
			if (timer <= tempTimer)
			{
				timer += Time.deltaTime;
			}
			else
			{
				Delete();
			}
		}
	}

	void Start()
	{
		colBase = platformBase.GetComponent<BoxCollider2D>();
		colCap1 = platformCap1.GetComponent<CircleCollider2D>();
		colCap2 = platformCap2.GetComponent<CircleCollider2D>();

		Collision(false);
	}

	public void UpdateLength(float length)
	{
		// adjust x scale of base to correct length, accounting for this.transform.localScale
		platformBase.transform.localScale = new Vector3(length / transform.localScale.x, platformBase.transform.localScale.y, platformBase.transform.localScale.z);
		// adjust local x pos of base so its end stays rooted to cap1
		platformBase.transform.localPosition = new Vector3(length / transform.localScale.x / 2, 0, 0);
		// adjust position of platformCap2 to correct length
		platformCap2.transform.localPosition = new Vector3(length / transform.localScale.x, 0, 0);
	}

	public void UpdateAngle(float ang)
	{
		//transform.Rotate(new Vector3(0,0,ang),Space.World); // no
		transform.rotation = Quaternion.Euler(0, 0, ang);
	}

	public void UpdateEndPoint(Vector2 pt)
	{
		if (pt.y > transform.position.y)
		{
			UpdateAngle(Vector2.Angle(Vector2.right, pt - new Vector2(transform.position.x, transform.position.y)));
		}
		else
		{
			UpdateAngle(-Vector2.Angle(Vector2.right, pt - new Vector2(transform.position.x, transform.position.y)));
		}
		
		UpdateLength(Vector2.Distance(transform.position, pt));
	}

	public void Collision(bool on)
	{
		if (on)
		{
			colBase.enabled = true;
			colCap1.enabled = true;
			colCap2.enabled = true;
		}
		else
		{
			colBase.enabled = false;
			colCap1.enabled = false;
			colCap2.enabled = false;
		}
	}

	public void Delete()
	{
		Destroy(this.gameObject);
		/*Destroy(platformBase);
		Destroy(platformCap1);
		Destroy(platformCap2);*/
	}

	public void SetTemp()
	{
		platformBase.GetComponent<MeshRenderer>().material = tempMat;
		platformCap1.GetComponent<MeshRenderer>().material = tempMat;
		platformCap2.GetComponent<MeshRenderer>().material = tempMat;
		temporary = true;
	}
}
