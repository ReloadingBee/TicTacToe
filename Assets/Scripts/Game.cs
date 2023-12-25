using UnityEngine;

public class Game : MonoBehaviour
{
	GameManager gameManager;
	char mySymbol = (char)GameManager.Players.o;
	void Start()
	{
		gameManager = GameManager.instance;
	}
	void Update()
	{
		if (gameManager.gameEnded) return;
		if (gameManager.currentTurn == mySymbol)
		{
			
		}
	}
}
