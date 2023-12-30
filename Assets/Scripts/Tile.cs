using UnityEngine;
public class Tile : MonoBehaviour
{
	public GameManager gameManager;

	public GameObject xPrefab;
	public GameObject oPrefab;
	
	[SerializeField] bool isChildAlive;
	GameObject child;
	const float childScale = 0.2f;
	const float speed = 5;

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
			if (isChildAlive)
			{
				// Destroy the child
				Destroy(child);
				isChildAlive = false;
			}
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
		// Ease-out
		child.transform.localScale = Vector3.Lerp(child.transform.localScale,Vector3.one * childScale, speed * Time.deltaTime);
	}
}
