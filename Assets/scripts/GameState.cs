using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameState : MonoBehaviour
{
	public Text message;

	public bool editMode = true;
	public bool selfPlay = true;
	public bool p1Map = true;
	public bool success;

	public Basket basket;
	public Cannon cannon;
	public Ball ball;

	public Transform basketDraggable;
	public Transform cannonDraggable;

	public int p1Score = 0;
	public int p2Score = 0;

	public List<Platform> permPlats;

	private Vector3 defaultBasketPos;
	private Vector3 defaultCannonPos;

	private void Start()
	{
		editMode = true;
		p1Map = true;
		selfPlay = true;
		SetMessageText();

		defaultBasketPos = basketDraggable.transform.position;
		defaultCannonPos = cannonDraggable.transform.position;
	}

	public void ChangeState()
	{
		if (editMode)
		{
			// currently in edit mode
			// go to self play mode
			selfPlay = true;
			editMode = false;
		}
		else
		{
			// currently in play mode
			if (!success)
			{
				// player failed map

				//award a point to map creator if it wasn't a self play
				if (!selfPlay)
				{
					Debug.Log("player messed up, map maker scores");
					if (p1Map) { P1Goal(); } else { P2Goal(); }
				}
				else
				{
					Debug.Log("player failed their own map");
				}

				// back to edit mode
				// other player's turn to map
				editMode = true;
				selfPlay = false;
				p1Map = !p1Map;
				ClearMap();
			}
			else
			{
				// player completed map

				if (selfPlay)
				{
					// player completed their own map
					// other player must now complete it
					selfPlay = false;

					Debug.Log("player successfully completed their own map");
				}
				else
				{
					// player completed other player's map
					// back to edit mode
					// other player's turn to map
					selfPlay = false;
					editMode = true;
					p1Map = !p1Map;
					ClearMap();
					Debug.Log("player successfully completed other player's map");
				}
			}
		}

		success = false;
		SetMessageText();
		basket.success = false;
		if (ball != null)
		{
			Destroy(ball.gameObject);
			ball = null;
		}
		
	}

	public void SetMessageText()
	{
		if (editMode)
		{
			if (p1Map)
			{
				message.text = "Player 1, make your map.";
			}
			else
			{
				message.text = "Player 2, make your map.";
			}
		}
		else
		{
			if (selfPlay)
			{
				if (p1Map)
				{
					message.text = "Player 1, play your map.";
				}
				else
				{
					message.text = "Player 2, play your map.";
				}
			}
			else
			{
				if (p1Map)
				{
					message.text = "Player 2, play your opponent's map.";
				}
				else
				{
					message.text = "Player 1, play your opponent's map.";
				}
			}
		}

		AppendScore();
	}

	public void ClearMap()
	{
		foreach (Platform p in permPlats)
		{
			p.Delete();
		}

		permPlats.Clear();

		//basket.transform.position = basket.defaultPos;
		//cannon.transform.position = cannon.defaultPos;

		basketDraggable.transform.position = defaultBasketPos;
		cannonDraggable.transform.position = defaultCannonPos;
	}

	public void P1Goal()
	{
		p1Score += 1;

		if (p1Score >= 5)
		{
			Victory(true);
		}
	}

	public void P2Goal()
	{
		p2Score += 1;

		if (p2Score >= 5)
		{
			Victory(false);
		}
	}

	private void Victory(bool player1Wins)
	{
		if (player1Wins)
		{
			message.text = "Player 1 Wins";
		}
		else
		{
			message.text = "Player 2 Wins";
		}
	}

	private void AppendScore()
	{
		message.text =
			message.text + System.Environment.NewLine +
			"P1: " + ScoreString(p1Score) + System.Environment.NewLine +
			"P2: " + ScoreString(p2Score);
	}

	private string ScoreString(int n)
	{
		if (n == 0)
		{
			return "";
		}
		else if (n == 1)
		{
			return "S";
		}
		else if (n == 2)
		{
			return "S M";
		}
		else if (n == 3)
		{
			return "S M I";
		}
		else if (n == 4)
		{
			return "S M I T";
		}
		else if (n == 5)
		{
			return "S M I T H";
		}

		return "[invalid score]";
	}
}
