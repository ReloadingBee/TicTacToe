using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;
	void Awake()
	{
		Application.targetFrameRate = 60;

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

	public enum Players
	{
		x = 'x',
		o = 'o'
	} // To make everyone's life harder :)

	void Start()
	{
		InitializeGame();
	}

	void InitializeGame()
	{
		ResetBoard();
		gameEnded = false;
		winner = '-'; // Empty
		currentTurn = (char)Players.x;
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
	} // Returns the opposite player from the Players enum

	public bool Move(int move)
	{
		if (gameEnded) return false;
		// Check if the move is valid, if not valid return false.
		if (move is < 0 or > 9)
		{
			return false;
		}
		if (board[move] != string.Empty)
		{
			return false;
		}

		// Make the move on the board
		board[move] = currentTurn.ToString();

		GameEndHandler();

		moveCount++;
		return true;
	} // Makes the move on the board

	bool GameIsDraw()
	{
		for (int i = 0; i < 9; i++)
		{
			if (board[i] == string.Empty)
				return false; // Empty cell found, game is not a draw
		}
		return true;
	} // Checks if the game is tied

	bool GameHasWon(string player)
	{
		// Check rows
		for (int i = 0; i < 3; i++)
		{
			if (board[i * 3] == player && board[i * 3 + 1] == player && board[i * 3 + 2] == player)
			{
				return true;
			}
		}

		// Check columns
		for (int i = 0; i < 3; i++)
		{
			if (board[i] == player && board[i + 3] == player && board[i + 6] == player)
			{
				return true;
			}
		}

		// Check diagonals
		if (board[0] == player && board[4] == player && board[8] == player)
		{
			return true;
		}

		// No win found
		return board[2] == player && board[4] == player && board[6] == player;
	} // Returns true if the player won

	void GameEndHandler()
	{
		if (GameHasWon(currentTurn.ToString()))
		{
			gameEnded = true;
			winner = currentTurn;
			winnerText.text = $"Winner: {winner.ToString().ToUpper()}";
			currentTurn = '-';
			turnText.text = "Current turn: -";
			return;
		}
		
		currentTurn = OppositePlayer(currentTurn);
		turnText.text = $"Current turn: {currentTurn.ToString().ToUpper()}";

		if (!GameIsDraw())
			return;
		
		gameEnded = true;
		winner = '-';
		winnerText.text = "Winner: DRAW";
		currentTurn = '-';
		turnText.text = "Current turn: -";
	} // Checks for game end and changes respective vars
}
