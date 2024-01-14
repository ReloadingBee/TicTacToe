using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;
	Settings settings;

	bool finishedInitializing;
	void Awake()
	{
		settings = Settings.instance;

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

	const float moveCooldown = 0.5f;
	public bool isCooldownEnabled;
	public float remainingCooldown;

	public TMP_Text turnText;
	public TMP_Text winnerText;
	public TMP_Text moveCountText;
	
	// Sounds
	public List<AudioClip> moveSounds;
	AudioSource source;

	public enum Players
	{
		x = 'x',
		o = 'o'
	} // To make everyone's life harder :)
	void Start()
	{
		board = new string[9];
		if(SceneManager.GetActiveScene().name == "SampleScene") InitializeGame();
		source = GetComponent<AudioSource>();
	}

	void Update()
	{
		// Default targetFrameRate value is -1
		Application.targetFrameRate = settings.limitFPS ? settings.targetFrameRate : -1;

		if (Input.GetKeyDown(KeyCode.R))
		{
			InitializeGame();
		}

		if (!isCooldownEnabled)
			return;
		remainingCooldown -= Time.deltaTime;
		if (remainingCooldown > 0f)
			return;
		remainingCooldown = 0f;
		isCooldownEnabled = false;
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
		winnerText.text = "Winner: -";
		turnText.text = "Current turn: X";
		moveCount = 0;
		moveCountText.text = "Move count: 0";
		
		winner = '-'; // Empty
		currentTurn = (char)Players.x;
		gameEnded = false;
		finishedInitializing = true;
		
		EnableCooldown(1f);
	} // Resets the game

	void ResetBoard()
	{
		for (int i = 0; i < 9; i++)
		{
			board[i] = string.Empty;
		}
	} // Resets the board to it's starting position

	public static char OppositePlayer(char player)
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
		return currentBoard[tile] == string.Empty;
	} // Returns true if the tile of the board is empty

	public bool Move(int tile)
	{
		if (gameEnded || isCooldownEnabled) return false;
		
		// Check if the move is valid, if not valid return false.
		if (tile is < 0 or > 9)
		{
			throw new ArgumentOutOfRangeException(nameof(tile), "Invalid tile index. It should be between 0 and 8.");
			//return false;
		}
		if (!IsEmpty(board, tile))
		{
			return false;
		}

		// Make the move on the board
		board[tile] = currentTurn.ToString();
		moveCount++;

		// Play random move sound
		int sound = Random.Range(0, moveSounds.Count);
		source.PlayOneShot(moveSounds[sound]);
		
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
