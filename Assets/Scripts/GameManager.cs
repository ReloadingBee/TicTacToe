using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;
	static readonly int targetFrameRate = 60;
	void Awake()
	{
		Application.targetFrameRate = targetFrameRate;

		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	} // Singleton magic

	public string[] board;
	public char currentTurn;
	public bool gameEnded;
	public char winner;
	public int moveCount;

	public TMP_Text turnText;
	public TMP_Text winnerText;
	public TMP_Text moveCountText;

	public enum Players
	{
		x = 'x',
		o = 'o'
	} // To make everyone's life harder :)

	void Start()
	{
		InitializeGame();
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.R))
		{
			InitializeGame();
		}
	}

	void InitializeGame()
	{
		ResetBoard();
		gameEnded = false;
		winner = '-'; // Empty
		winnerText.text = "Winner: -";
		currentTurn = (char)Players.x;
		turnText.text = "Current turn: X";
		moveCount = 0;
		moveCountText.text = "Move count: 0";
	} // Resets the game

	void ResetBoard()
	{
		board = new string[9];
		for (int i = 0; i < 9; i++)
		{
			board[i] = string.Empty;
		}
	} // Resets the board to it's starting position

	static char OppositePlayer(char player)
	{
		if (player == (char)Players.x) return (char)Players.o;
		return (char)Players.x;
	} // Returns the opposite player from the Players enum. For example: x -> o OR o -> x

	public List<int> GetEmptyPositions(string[] currentBoard)
	{
		List<int> emptyPositions = new List<int>();

		for (int i = 0; i < currentBoard.Length; i++)
		{
			if (IsEmpty(currentBoard, i))
			{
				emptyPositions.Add(i);
			}
		}
		return emptyPositions;
	} // Returns a list of empty positions of a given board

	public bool IsEmpty(string[] currentBoard, int tile)
	{
		return currentBoard[tile] == String.Empty;
	} // Returns true if the tile of the board is empty

	public bool Move(int tile)
	{
		if (gameEnded) return false;
		// Check if the move is valid, if not valid return false.
		if (tile is < 0 or > 9)
		{
			return false;
		}
		if (!IsEmpty(board, tile))
		{
			return false;
		}

		// Make the move on the board
		board[tile] = currentTurn.ToString();
		moveCount++;

		GameEndHandler(currentTurn);

		return true;
	} // Makes the move on the board

	public bool GameIsDraw(string[] currentBoard)
	{
		return GetEmptyPositions(currentBoard).Count == 0;

		// Might be (almost nothing) faster, because it exits the loop the moment it finds an empty tile
		/*
		 for (int i = 0; i < 9; i++)
		{
			if (IsEmpty(currentBoard, i))
				return false; // Empty tile found, game is not a draw
		}
		return true;
		*/
	} // Checks if the game is tied

	public bool GameHasWon(string[] currentBoard, char player)
	{
		string p = player.ToString();
		string[] cb = currentBoard;

		// Check rows
		for (int i = 0; i < 3; i++)
		{
			if (cb[i * 3] == p && cb[i * 3 + 1] == p && cb[i * 3 + 2] == p)
			{
				return true;
			}
		}

		// Check columns
		for (int i = 0; i < 3; i++)
		{
			if (cb[i] == p && cb[i + 3] == p && cb[i + 6] == p)
			{
				return true;
			}
		}

		// Check diagonals
		if (cb[0] == p && cb[4] == p && cb[8] == p)
		{
			return true;
		}

		// No win found
		return cb[2] == p && cb[4] == p && cb[6] == p;
	} // Returns true if the player won

	void GameEndHandler(char turn)
	{
		moveCountText.text = $"Move count: {moveCount}";
		if (GameHasWon(board, turn))
		{
			gameEnded = true;
			winner = turn;
			winnerText.text = $"Winner: {winner.ToString().ToUpper()}";
			currentTurn = '-';
			turnText.text = "Current turn: -";
			return;
		}

		currentTurn = OppositePlayer(currentTurn);
		turnText.text = $"Current turn: {currentTurn.ToString().ToUpper()}";

		if (!GameIsDraw(board)) return;

		gameEnded = true;
		winner = '-';
		winnerText.text = "Winner: DRAW";
		currentTurn = '-';
		turnText.text = "Current turn: -";
	} // Checks for game end and changes respective vars
}
