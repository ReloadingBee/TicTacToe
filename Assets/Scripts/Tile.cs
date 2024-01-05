using UnityEngine;
public class Tile : MonoBehaviour
{
	GameManager gameManager;

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
	}

	void OnMouseDown()
	{
		GameManager.instance.Move(tileID);
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
			child.transform.localScale = Vector3.zero;
			isChildAlive = true;
		}
	}
	void HandleChild()
	{
		// Ease-out animation
		child.transform.localScale = Vector3.Lerp(child.transform.localScale,Vector3.one * childScale, childAnimationSpeed * Time.deltaTime);
	}
}
