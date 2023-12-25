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
		GameManager.instance.Move(tileID); 
	}
	void Update()
	{
		// Empty tile
		if (GameManager.instance.board[tileID] == string.Empty)
		{
			rend.material.color = Color.white;
			return;
		}
		
		rend.material.color = GameManager.instance.board[tileID] == "x" ? xColor : oColor;
	}
}
