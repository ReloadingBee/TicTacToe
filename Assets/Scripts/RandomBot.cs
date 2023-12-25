using UnityEngine;

public class RandomBot : MonoBehaviour
{
	GameManager gameManager;

	public GameManager.Players symbol = GameManager.Players.o;
	char player;
	void Start()
	{
		gameManager = GameManager.instance;
		player = (char)symbol;
	}
	void Update()
	{
		if (gameManager.gameEnded || gameManager.currentTurn != player) return;
		
		int move = Random.Range(0, 9); // [0, 9)
		gameManager.Move(move);
	}
}
