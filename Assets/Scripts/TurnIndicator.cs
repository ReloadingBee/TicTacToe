using UnityEngine;

public class TurnIndicator : MonoBehaviour
{
	Game game;

	void Start()
	{
		game = Game.instance;
		rend = GetComponent<SpriteRenderer>();
	}

	public bool isToTheLeft;

	const float posX = 0.7f;
	const float posY = 4f;

	const float animationSpeed = 10f;

	SpriteRenderer rend;
	void Update()
	{
		if (!game.hasGameStarted) return;
		
		var positionX = Mathf.Lerp(transform.position.x, game.currentTurn == 'x' ? -posX : posX, animationSpeed * Time.deltaTime);
		if (positionX == transform.position.x) return;
		transform.position = new Vector3(positionX, posY);

		if (isToTheLeft) return;
		var color = Mathf.InverseLerp(-posX, posX, positionX);
		rend.color = new Color(1f, 1f, 1f, color);
	}
}
