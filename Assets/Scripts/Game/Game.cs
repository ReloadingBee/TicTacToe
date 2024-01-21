using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
	public static Game instance;
	GameManager gameManager;

	bool finishedInitializing;
	void Awake()
	{
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
	public bool hasGameStarted;
	public bool hasGameEnded;
	public char winner;
	public int moveCount;

	const float moveCooldown = 0.5f;
	public bool isCooldownEnabled;
	public float remainingCooldown;

	public enum Players
	{
		x = 'x',
		o = 'o'
	} // To make everyone's life harder :)
	void Start()
	{
		gameManager = GameManager.instance;

		board = new string[9];
		if (SceneManager.GetActiveScene().name == "SampleScene") InitializeGame();
	}

	void Update()
	{
		// Restart game
		if (Input.GetKeyDown(KeyCode.R))
		{
			InitializeGame();
		}

		// Cooldown logic
		if (isCooldownEnabled)
		{
			remainingCooldown -= Time.deltaTime;
			if (remainingCooldown < 0f)
			{
				remainingCooldown = 0f;
				isCooldownEnabled = false;
			}
		}
	}

	void EnableCooldown(float cooldown)
	{
		remainingCooldown = cooldown;
		isCooldownEnabled = true;
	}

	public void InitializeGame()
	{
		finishedInitializing = false;
		ResetBoard();
		moveCount = 0;
		winner = '-'; // Empty
		currentTurn = (char)Players.x;
		hasGameStarted = true;
		hasGameEnded = false;
		finishedInitializing = true;

		EnableCooldown(1f);
	} // Resets the game

	public void ResetBoard()
	{
		for (var i = 0; i < 9; i++)
		{
			board[i] = string.Empty;
		}
	} // Resets the board to it's starting position

	public char OppositePlayer(char player)
	{
		return player == (char)Players.x ? (char)Players.o : (char)Players.x;
	} // Returns the opposite player from the Players enum. For example: x -> o OR o -> x

	public List<int> GetEmptyPositions(string[] currentBoard)
	{
		var emptyPositions = new List<int>();

		for (var i = 0; i < currentBoard.Length; i++)
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
		return currentBoard[tile] == string.Empty;
	} // Returns true if the tile of the board is empty

	public bool Move(int tile)
	{
		#region MoveValidation

		if (hasGameEnded || isCooldownEnabled) return false;
		if (tile is < 0 or > 9)
		{
			throw new ArgumentOutOfRangeException(nameof(tile), "Invalid tile index. It should be between 0 and 8.");
			//return false;
		}
		if (!IsEmpty(board, tile)) return false;

		#endregion

		// Make the move on the board
		board[tile] = currentTurn.ToString();
		moveCount++;
		gameManager.PlayRandomSound(gameManager.moveSounds);

		EnableCooldown(moveCooldown);
		GameEndHandler(currentTurn);

		return true;
	} // Makes the move on the board

	public bool GameIsDraw(string[] currentBoard)
	{
		return GetEmptyPositions(currentBoard).Count == 0;
	} // Checks if the game is tied

	public bool GameHasWon(string[] currentBoard, char player)
	{
		string p = player.ToString();
		string[] cb = currentBoard;

		// Check rows
		for (var i = 0; i < 3; i++)
		{
			if (cb[i * 3] == p && cb[i * 3 + 1] == p && cb[i * 3 + 2] == p)
			{
				return true;
			}
		}

		// Check columns
		for (var i = 0; i < 3; i++)
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
		return cb[2] == p && cb[4] == p && cb[6] == p;
	} // Returns true if the player won

	void GameEndHandler(char turn)
	{
		// Game won
		if (GameHasWon(board, turn))
		{
			winner = turn;
			currentTurn = '-';
			hasGameEnded = true;
			GameEndMenu.instance.EnableMenu();
			return;
		}

		// Game continued
		currentTurn = OppositePlayer(currentTurn);

		if (!GameIsDraw(board)) return;
		// Game draw
		hasGameEnded = true;
		winner = '-';
		currentTurn = '-';
		GameEndMenu.instance.EnableMenu();
	} // Checks for game end and changes respective vars
}
