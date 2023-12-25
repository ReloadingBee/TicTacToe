using UnityEngine;
public class Tile : MonoBehaviour
{
	public int tileID;
	public Color xColor;
	public Color oColor;
	Renderer rend;

	void Start()
	{
		rend = GetComponent<Renderer>();
	}

	void OnMouseDown()
	{
		if (GameManager.instance.Move(tileID))
		{
			// If move count is NOT divisible by 2, then it's X turn
			// We use this because of the inconvenience when the game ends - the currentTurn variable becomes '-'
			rend.material.color = GameManager.instance.moveCount % 2 != 0 ? xColor : oColor;
		}
	}
	void Update()
	{
		if (GameManager.instance.moveCount == 0)
		{
			rend.material.color = Color.white;
		}
	}
}
