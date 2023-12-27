using System.Collections.Generic;
using UnityEngine;

public class RandomFasterBot : MonoBehaviour
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

		List<int> moves = gameManager.GetEmptyPositions(gameManager.board);
		gameManager.Move(moves[Random.Range(0, moves.Count)]);
	}
}
