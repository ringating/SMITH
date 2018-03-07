using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchCursor : MonoBehaviour
{

	public GameState gs;

	public Camera cam;
	public GameObject platformPrefab;
	public Platform activePlat;
	public bool active = false;
	public bool useMouse = false;

	private bool wasActive = false;

	// Use this for initialization
	void Start ()
	{
		active = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		// Instantiate(platformPrefab, Vector3.zero, Quaternion.identity); // lol no

		// if the screen is being touched, set this position to the position of the first touch
		if (Input.touchCount > 0)
		{
			active = true;
			transform.position = new Vector3(cam.ScreenToWorldPoint(Input.GetTouch(0).position).x, cam.ScreenToWorldPoint(Input.GetTouch(0).position).y, transform.position.z);
		}
		else
		{
			active = false;
		}

		// due to being later, this takes precedence over touch input if enabled
		if (useMouse)
		{
			if (Input.GetButton("Fire1"))
			{
				active = true;
				transform.position = new Vector3(cam.ScreenToWorldPoint(Input.mousePosition).x, cam.ScreenToWorldPoint(Input.mousePosition).y, transform.position.z);
			}
			else
			{
				active = false;
			}
		}

		// tap calls
		if (active && wasActive)		{ TapHold(); }
		else if (active && !wasActive)	{ TapDown(); }
		else if (!active && wasActive)	{ TapUp(); }

		wasActive = active;
	}

	private void TapDown()
	{
		//determine what the action will be based on what is now colliding with the 
		RaycastHit2D rh = Physics2D.Linecast(transform.position, transform.position);
		if (rh.collider != null)
		{
			Debug.Log("TapDown on " + rh.collider.name);  // raycast of distance 0, so pretty much a point check
			if (rh.collider.transform.parent != null)
			{ 
				if (rh.collider.transform.parent.GetComponent<Platform>() != null)
				{
					//when tapping on an existing platform, delete it if in edit mode
					if (gs.editMode)
					{
						gs.permPlats.Remove(rh.collider.transform.parent.GetComponent<Platform>());
						Destroy(rh.collider.transform.parent.gameObject);
					}
					else
					{
						activePlat = Instantiate(platformPrefab, transform.position, Quaternion.identity).GetComponent<Platform>();
						activePlat.SetTemp();
					}
				}
			}
		}
		else
		{
			Debug.Log("TapDown on nothing");
			activePlat = Instantiate(platformPrefab, transform.position, Quaternion.identity).GetComponent<Platform>();

			if (gs.editMode)
			{
				activePlat.temporary = false;
				gs.permPlats.Add(activePlat);
			}
			else
			{
				activePlat.SetTemp();
			}

			/*activePlat.colBase.enabled = false;
			activePlat.colCap1.enabled = false;
			activePlat.colCap2.enabled = false; this didnt work, didnt exist yet so couldnt be disabled.*/
		}
	}

	private void TapHold()
	{
		if (activePlat != null)
		{
			activePlat.UpdateEndPoint(transform.position);
		}
	}

	private void TapUp()
	{
		if (activePlat != null)
		{
			activePlat.Collision(true);
			activePlat = null;
		}
	}
}
