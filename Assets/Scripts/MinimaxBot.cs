using System.Collections.Generic;
using UnityEngine;

public class MinimaxBot : MonoBehaviour
{
	Game game;

	public GameManager.Players symbol = GameManager.Players.o;
	char player;

	public float evaluation;

	public bool useDepthLimit;
	public int depthLimit;
	const int maxDepth = 10;
	const int evalOffset = 2; // Master depth + First move
	void Start()
	{
		game = Game.instance;
		player = (char)symbol;
	}
	void Update()
	{
		Move();
	}

	void Move()
	{
		// Quit if it's not my turn!
		if (game.isCooldownEnabled || !game.hasGameStarted || game.hasGameEnded || game.currentTurn != player) return;
		var bestMove = Minimax(game.board, 0, true, useDepthLimit ? depthLimit : maxDepth);
		game.Move(bestMove);
	}

	int Minimax(string[] currentBoard, int depth, bool isMaximizingPlayer, int mMaxDepth = maxDepth)
	{
		// Optimization: on the first move, move randomly instead of calculating the whole game tree
		if (game.GetEmptyPositions(currentBoard).Count == 9)
		{
			return Random.Range(0, 9);
		}
		
		// isMaximizingPlayer: TRUE = BOT; FALSE = OPPONENT

		const int masterDepth = 0;
		const int winValue = 10;
		const int drawValue = 0;

		#region Evaluation
		// Return good values for winning and bad values for losing
		if (game.GameHasWon(currentBoard, isMaximizingPlayer ? player : game.OppositePlayer(player)))
		{
			return isMaximizingPlayer ? winValue - depth + evalOffset : -winValue + depth - evalOffset;
		}
		// Draw
		if (!game.GameHasWon(currentBoard, player) && !game.GameHasWon(currentBoard, game.OppositePlayer(player)) && game.GameIsDraw(currentBoard))
		{
			return drawValue;
		}
		// Limit the search depth
		if (depth >= mMaxDepth)
		{
			return drawValue;
		}
		#endregion

		int bestVal;
		var bestMoves = new List<int>();
		if (isMaximizingPlayer) // BOT
		{
			bestVal = -winValue;
			// Loop through every empty position
			foreach (var move in game.GetEmptyPositions(currentBoard))
			{
				currentBoard[move] = player.ToString(); // Make the move on the board
				var value = Minimax(currentBoard, depth + 1, false, mMaxDepth);
				currentBoard[move] = string.Empty; // Undo move

				// Best move(s)
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
						evaluation = bestVal;
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
			foreach (var move in game.GetEmptyPositions(currentBoard))
			{
				currentBoard[move] = game.OppositePlayer(player).ToString(); // Make the move on the board
				var value = Minimax(currentBoard, depth + 1, true, mMaxDepth);
				currentBoard[move] = string.Empty; // Undo move

				bestVal = Mathf.Min(bestVal, value);
			}
			return bestVal;
		}
	}
}
