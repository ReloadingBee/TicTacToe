using UnityEngine;

public class RandomBot : MonoBehaviour
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
		
		var move = Random.Range(0, 9); // [0, 9)
		game.Move(move);
	}
}
