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

	public enum Players
	{
		x = 'x',
		o = 'o'
	}

	void Start()
	{
		InitializeGame();
	}

	public void InitializeGame()
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

	public static char OppositePlayer(char player)
	{
		if (player == (char)Players.x) return (char)Players.o;
		return (char)Players.x;
	} // Returns the opposite player from the Players enum

	public bool Move(int move)
	{
		// Check if the move is valid, if not valid return false.
		if (move < 0 || 9 < move)
		{
			return false;
		}
		if (board[move] != string.Empty)
		{
			return false;
		}

		// Make the move on the board
		board[move] = currentTurn.ToString();
		currentTurn = OppositePlayer(currentTurn);
		turnText.text = $"Current turn: {currentTurn.ToString().ToUpper()}";
		return true;
	} // Makes the move on the board


	public TMP_Text turnText;
	
}
