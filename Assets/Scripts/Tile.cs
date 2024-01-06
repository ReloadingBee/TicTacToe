using UnityEngine;
public class Tile : MonoBehaviour
{
	GameManager gameManager;
	Settings settings;

	public GameObject xPrefab;
	public GameObject oPrefab;

	GameObject child;
	bool isChildAlive;
	const float childScale = 0.2f;
	const float childAnimationSpeed = 5;

	public int tileID;

	void Start()
	{
		gameManager = GameManager.instance;
		settings = Settings.instance;
	}

	void OnMouseDown()
	{
		gameManager.Move(tileID);
	}
	void Update()
	{
		if (gameManager.IsEmpty(gameManager.board, tileID))
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
			child = Instantiate(gameManager.board[tileID] == GameManager.Players.x.ToString() ? xPrefab : oPrefab, this.transform);
			
			// Child scale
			child.transform.localScale = settings.useAnimations ? Vector3.zero : Vector3.one * childScale;
			
			isChildAlive = true;
		}
	}
	void HandleChild()
	{
		// If the size is already correct, exit
		if (child.transform.localScale == Vector3.one * childScale) return;

		if (settings.useAnimations)
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
