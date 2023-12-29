using UnityEngine;

public class MinimaxBot : MonoBehaviour
{
	GameManager gameManager;

	public GameManager.Players symbol = GameManager.Players.o;
	char player;

	public bool useDepthLimit;
	public int depthLimit;
	void Start()
	{
		gameManager = GameManager.instance;
		player = (char)symbol;
	}
	void Update()
	{
		Move();
	}

	void Move()
	{
		// Quit if it's not my turn!
		if (gameManager.gameEnded || gameManager.currentTurn != player) return;

		int bestMove = Minimax(gameManager.board, 0, true, useDepthLimit ? depthLimit : 10);
		gameManager.Move(bestMove);
	}

	int Minimax(string[] currentBoard, int depth, bool isMaximizingPlayer, int maxDepth = 10)
	{
		// isMaximizingPlayer: TRUE = BOT; FALSE = OPPONENT

		const int masterDepth = 0;
		const int winValue = 10;
		const int drawValue = 0;

		#region Final Values
		// Return good values for winning and bad values for losing
		// Limit the search depth
		if (gameManager.GameHasWon(currentBoard, isMaximizingPlayer ? player : gameManager.OppositePlayer(player)))
		{
			return isMaximizingPlayer ? winValue - depth : -winValue + depth;
		}
		// Draw
		if (!gameManager.GameHasWon(currentBoard, player) && !gameManager.GameHasWon(currentBoard, gameManager.OppositePlayer(player)) && gameManager.GameIsDraw(currentBoard))
		{
			return drawValue;
		}
		if (depth >= maxDepth)
		{
			return drawValue;
		}
		#endregion

		int bestVal;
		var bestMove = -1;
		if (isMaximizingPlayer) // BOT
		{
			bestVal = -winValue;
			// Loop through every empty position
			foreach (var move in gameManager.GetEmptyPositions(currentBoard))
			{
				if (bestMove == -1) bestMove = move;
				
				currentBoard[move] = player.ToString(); // Make the move on the board
				var value = Minimax(currentBoard, depth + 1, false, maxDepth);
				currentBoard[move] = string.Empty; // Undo move

				if (depth == masterDepth)
				{
					if (value == bestVal)
					{
						// Randomize moves that are equally as good
						// Leads to more variety
						if (!(Random.Range(0f, 1f) > 1f / gameManager.GetEmptyPositions(currentBoard).Count))
							continue;
						bestVal = value;
						bestMove = move;
					}
					else if (value > bestVal)
					{
						// Mathf.Max(bestVal, value)
						bestVal = value;
						bestMove = move;
					}
				}
				else
				{
					// Choose the best value
					bestVal = Mathf.Max(bestVal, value);
				}
			}
			// Return the best value or the best move if it's the master depth
			return depth == masterDepth ? bestMove : bestVal;
		}
		else // OPPONENT
		{
			bestVal = winValue;
			// Loop through every empty position
			foreach (int move in gameManager.GetEmptyPositions(currentBoard))
			{
				currentBoard[move] = gameManager.OppositePlayer(player).ToString(); // Make the move on the board
				int value = Minimax(currentBoard, depth + 1, true, maxDepth);
				currentBoard[move] = string.Empty; // Undo move

				bestVal = Mathf.Min(bestVal, value);
			}
			return bestVal;
		}
	}
}
