using UnityEngine;
public class Tile : MonoBehaviour
{
	public GameManager gameManager;
	
	public int tileID;
	public Color xColor;
	public Color oColor;
	Renderer rend;

	void Start()
	{
		gameManager = GameManager.instance;
		rend = GetComponent<Renderer>();
	}

	void OnMouseDown()
	{
		GameManager.instance.Move(tileID); 
	}
	void Update()
	{
		// Empty tile is White
		if(gameManager.IsEmpty(gameManager.board, tileID))
		{
			rend.material.color = Color.white;
			return;
		}
		
		rend.material.color = gameManager.board[tileID] == "x" ? xColor : oColor;
	}
}
