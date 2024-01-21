using UnityEngine;

public class RandomFasterBot : MonoBehaviour
{
	Game game;

	public GameManager.Players symbol = GameManager.Players.o;
	char player;
	void Start()
	{
		game = Game.instance;
		player = (char)symbol;
	}
	void Update()
	{
		if (game.hasGameEnded || game.currentTurn != player) return;

		var moves = game.GetEmptyPositions(game.board);
		game.Move(moves[Random.Range(0, moves.Count)]);
	}
}
