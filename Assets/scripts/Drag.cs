using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour
{
	public GameState gs;

	public List<Transform> dragging = new List<Transform>();

	private TouchCursor cursor;
	private Vector3 prevPos;
	private CircleCollider2D col;

	// Use this for initialization
	void Start ()
	{
		cursor = GetComponent<TouchCursor>();
		prevPos = transform.position;
		col = GetComponent<CircleCollider2D>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		//
		if (cursor.active)
		{
			col.enabled = true;
		}
		else
		{
			col.enabled = false;
		}

		// move each thing in the list
		foreach (Transform t in dragging)
		{
			t.position += transform.position - prevPos;
		}
		prevPos = transform.position;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		Debug.Log("OnTriggerEnter2D");

		if (other.tag == "Draggable" && cursor.activePlat == null  && dragging.Count < 1)
		{
			dragging.Add(other.transform);
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "Draggable" && cursor.activePlat == null)
		{
			dragging.Remove(other.transform);
		}
	}
}
