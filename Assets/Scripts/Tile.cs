using System;
using UnityEngine;
public class Tile : MonoBehaviour
{
	Game game;

	public GameObject xPrefab;
	public GameObject oPrefab;

	GameObject child;
	public bool isChildAlive;
	Vector3 targetScale = Vector3.one * 0.2f;
	const float childAnimationSpeed = 5;

	public bool mouseOver;

	public bool isGhostAlive;
	char ghostSymbol;
	GameObject ghost;

	public int id;

	public float cooldown;
	public bool isCooldownEnabled;

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

		#region Tile removal

		// Tile removing animation
		if (isCooldownEnabled) cooldown -= Time.deltaTime;
		if (game.hasGameEnded && isChildAlive && !isCooldownEnabled)
		{
			cooldown = 1 + 0.1f * id;
			isCooldownEnabled = true;
		}
		if (isCooldownEnabled && cooldown <= 0f)
		{
			cooldown = 0f;
			isCooldownEnabled = false;
			targetScale = Vector3.zero;

			if (isChildAlive && child.transform.localScale == targetScale)
			{
				game.board[id] = string.Empty;
				Destroy(child);
				isChildAlive = false;
				targetScale = Vector3.one * 0.2f;
			}
		}

		#endregion

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
			child.transform.localScale = Settings.instance.disableAnimations ? targetScale : Vector3.zero;

			isChildAlive = true;
		}
	}
	void HandleChild()
	{
		// If the size is already correct, exit
		if (child.transform.localScale == targetScale) return;

		if (!Settings.instance.disableAnimations)
		{
			// Ease-out animation
			var scale = Vector3.Lerp(child.transform.localScale, targetScale, childAnimationSpeed * Time.deltaTime);

			// Faster rounding
			if (MathF.Round(scale.x, 2) == targetScale.x)
			{
				child.transform.localScale = targetScale;
			}
			else child.transform.localScale = scale;
		}
		else
		{
			child.transform.localScale = targetScale;
		}
	}

	void InstantiateGhost()
	{
		if (game.hasGameEnded) return;
		// Instantiate a new ghost
		var symbol = game.currentTurn.ToString() == GameManager.Players.x.ToString() ? xPrefab : oPrefab;
		ghostSymbol = game.currentTurn.ToString() == GameManager.Players.x.ToString() ? 'x' : 'o';
		ghost = Instantiate(symbol, transform);
		ghost.transform.localScale = targetScale;
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
