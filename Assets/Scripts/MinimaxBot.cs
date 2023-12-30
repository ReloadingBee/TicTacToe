using System.Collections.Generic;
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
		List<int> bestMoves = new List<int>();
		if (isMaximizingPlayer) // BOT
		{
			bestVal = -winValue;
			// Loop through every empty position
			foreach (var move in gameManager.GetEmptyPositions(currentBoard))
			{
				currentBoard[move] = player.ToString(); // Make the move on the board
				var value = Minimax(currentBoard, depth + 1, false, maxDepth);
				currentBoard[move] = string.Empty; // Undo move

				if (depth == masterDepth)
				{
					if (value == bestVal)
					{
						// If the move is as good as other moves, add it to the list
						bestMoves.Add(move);
					}
					else if (value > bestVal)
					{
						// If the move is BETTER than the rest, delete the rest and add it to the list
						bestVal = value;
						bestMoves.Clear();
						bestMoves.Add(move);
					}
				}
				else
				{
					// Choose the best value
					bestVal = Mathf.Max(bestVal, value);
				}
			}
			// Randomly chooses a move from bestMoves list
			return depth == masterDepth ? bestMoves[Random.Range(0, bestMoves.Count)] : bestVal;
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
