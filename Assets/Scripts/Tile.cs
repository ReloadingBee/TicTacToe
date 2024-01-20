using UnityEngine;
public class Tile : MonoBehaviour
{
	Game game;

	public GameObject xPrefab;
	public GameObject oPrefab;

	GameObject child;
	bool isChildAlive;
	const float childScale = 0.2f;
	const float childAnimationSpeed = 5;

	public bool mouseOver;

	public bool isGhostAlive;
	char ghostSymbol;
	GameObject ghost;

	public int id;

	void Start()
	{
		game = Game.instance;
	}

	void Update()
	{
		// Destroy ghost
		if ((isChildAlive || !mouseOver) && isGhostAlive)
		{
			Destroy(ghost);
			isGhostAlive = false;
		}
		// Check if the ghost is outdated (someone made a move)
		if (isGhostAlive && game.currentTurn != ghostSymbol)
		{
			Destroy(ghost);
			isChildAlive = false;
			InstantiateGhost();
		}

		if (!game.hasGameStarted) return;

		if (game.IsEmpty(game.board, id))
		{
			if (isChildAlive)
			{
				// Destroy the child
				Destroy(child);
				isChildAlive = false;
			}
			else if (mouseOver && !isGhostAlive)
			{
				InstantiateGhost();
			}
			return;
		}

		if (isChildAlive)
		{
			HandleChild();
		}
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

	void InstantiateGhost()
	{
		// Instantiate a new ghost
		var symbol = game.currentTurn.ToString() == GameManager.Players.x.ToString() ? xPrefab : oPrefab;
		ghostSymbol = game.currentTurn.ToString() == GameManager.Players.x.ToString() ? 'x' : 'o';
		ghost = Instantiate(symbol, transform);
		ghost.transform.localScale = Vector3.one * childScale;
		isGhostAlive = true;

		ghost.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f);
	}

	void OnMouseDown()
	{
		// Check if it's player's move
		game.Move(id);
	}
	void OnMouseOver()
	{
		mouseOver = true;
	}
	void OnMouseExit()
	{
		mouseOver = false;
	}
}
