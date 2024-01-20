using UnityEngine;
public class Tile : MonoBehaviour
{
	Game game;

	public GameObject xPrefab;
	public GameObject oPrefab;

	GameObject child;
	public bool isChildAlive;
	const float childScale = 0.2f;
	const float childAnimationSpeed = 5;

	public int id;

	void Start()
	{
		game = Game.instance;
	}

	void OnMouseDown()
	{
		// Check if it's player's move
		game.Move(id);
	}
	void Update()
	{
		if (!game.hasGameStarted) return;
		if (game.IsEmpty(game.board, id))
		{
			if (!isChildAlive)
				return;
			
			// Destroy the child
			Destroy(child);
			isChildAlive = false;
			
			return;
		}
		if (isChildAlive) HandleChild();
		else
		{
			// Instantiate a new child
			var symbol = game.board[id] == GameManager.Players.x.ToString() ? xPrefab : oPrefab;
			child = Instantiate(symbol, transform);
			
			// Child scale
			child.transform.localScale = Settings.instance.disableAnimations ? Vector3.one * childScale : Vector3.zero;
			
			isChildAlive = true;
		}
	}
	void HandleChild()
	{
		// If the size is already correct, exit
		if (child.transform.localScale == Vector3.one * childScale) return;

		if (!Settings.instance.disableAnimations)
		{
			// Ease-out animation
			child.transform.localScale = Vector3.Lerp(child.transform.localScale, Vector3.one * childScale, childAnimationSpeed * Time.deltaTime);
		}
		else
		{
			child.transform.localScale = Vector3.one * childScale;
		}
	}
}
