using UnityEngine;

public class GameEndButton : MonoBehaviour
{
    GameEndMenu gameEndMenu;

	[Range(0, 1)] public int id;

	bool mouseOver;
	bool mouseDown;

	const float animationSpeed = 5f;
	const float maxAnimationScale = 1.2f;

	RectTransform rect;

	const float indicatorOffsetY = -0.1f;
	const float indicatorOffsetX = 0.5f;

	Vector3 lastIndicatorPosition;
	Vector3 lastLocalScale;

	void Start()
	{
		gameEndMenu = GameEndMenu.instance;

		rect = GetComponent<RectTransform>();
	}

	void Update()
	{
		if ((mouseOver && mouseDown) || (gameEndMenu.selectedButton == id && Input.GetKeyDown(KeyCode.Return)))
		{
			ClickHandler();
			OnMouseUp();
		}

		if (gameEndMenu.selectedButton == id)
		{
			// Optimization: only set the menu.IndicatorPosition if it's changed
			var indicatorPosition = new Vector3(rect.rect.size.x / 2 * transform.localScale.x + indicatorOffsetX, id * -1.5f + indicatorOffsetY);
			if (lastIndicatorPosition != indicatorPosition)
			{
				gameEndMenu.IndicatorPosition = lastIndicatorPosition = indicatorPosition;
			}
		}
		// Size animation
		// Optimization: only set the transform.localScale if it's changed
		var localScale = Vector3.Lerp(transform.localScale, Vector3.one * (gameEndMenu.selectedButton == id ? maxAnimationScale : 1f), animationSpeed * Time.deltaTime);
		if (lastLocalScale != localScale)
		{
			transform.localScale = lastLocalScale = localScale;
		}
	}

	void ClickHandler()
	{
		switch (id)
		{
			case 0:
				// Rematch
				gameEndMenu.DisableMenu();
				Game.instance.InitializeGame();
				break;
			case 1:
				// Main-menu
				gameEndMenu.DisableMenu();
				mMenu.instance.EnableMenu();
				GameManager.instance.LoadScene("MainMenu");
				break;
		}
	}

	void OnMouseDown()
	{
		mouseDown = true;
	}
	void OnMouseUp()
	{
		mouseDown = false;
	}
	void OnMouseOver()
	{
		mouseOver = true;
		gameEndMenu.selectedButton = id;
	}
	void OnMouseExit()
	{
		mouseOver = false;
	}
}
