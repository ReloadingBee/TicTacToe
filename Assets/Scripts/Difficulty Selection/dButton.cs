using UnityEngine;

public class dButton : MonoBehaviour
{
	dMenu menu;

	[Range(0, 3)] public int id;

	bool mouseOver;
	bool mouseDown;

	const float animationSpeed = 5f;
	const float maxAnimationScale = 1.2f;

	RectTransform rect;

	const float indicatorOffsetY = -0.1f;
	const float indicatorOffsetX = 0.5f;
	const float indicatorOffsetY2 = 0.9f;

	Vector3 lastIndicatorPosition;
	Vector3 lastLocalScale;

	void Start()
	{
		menu = dMenu.instance;

		rect = GetComponent<RectTransform>();
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (menu.menuID == 1)
			{
				menu.DisableMenu1();
				mMenu.instance.EnableMenu();
			}
			else
			{
				menu.menuID = 1;
				menu.DisableMenu2();
				menu.EnableMenu1();
			}
		}
		if ((mouseOver && mouseDown) || (menu.selectedButton == id && Input.GetKeyDown(KeyCode.Return)))
		{
			ClickHandler();
			OnMouseUp();
		}

		if (menu.selectedButton == id)
		{
			// Optimization: only set the menu.IndicatorPosition if it's changed
			var indicatorPosition = new Vector3(rect.rect.size.x / 2 * transform.localScale.x + indicatorOffsetX, id * -1.5f + (menu.menuID == 1 ? indicatorOffsetY : indicatorOffsetY2));
			if (lastIndicatorPosition != indicatorPosition)
			{
				if(menu.menuID == 1)
					menu.IndicatorPosition1 = lastIndicatorPosition = indicatorPosition;
				else
				{
					menu.IndicatorPosition2 = lastIndicatorPosition = indicatorPosition;
				}
			}
		}
		// Size animation
		// Optimization: only set the transform.localScale if it's changed
		var localScale = Vector3.Lerp(transform.localScale, Vector3.one * (menu.selectedButton == id ? maxAnimationScale : 1f), animationSpeed * Time.deltaTime);
		if (lastLocalScale != localScale)
		{
			transform.localScale = lastLocalScale = localScale;
		}
	}

	void ClickHandler()
	{
		GameManager.instance.PlayRandomSound(GameManager.instance.tickSounds);
		if (menu.menuID == 1)
		{
			switch (id)
			{
				case 0:
					// Single Player
					menu.menuID = 2;
					menu.DisableMenu1();
					menu.EnableMenu2();
					break;
				case 1:
					// Multi-Player
					menu.DisableMenu1();
					GameManager.instance.LoadScene("Multi-Player");
					Game.instance.InitializeGame();
					break;
			}
		}
		else
		{
			menu.DisableMenu2();
			switch (id)
			{
				case 0:
					// Easy
					GameManager.instance.LoadScene("Easy");
					Game.instance.InitializeGame();
					break;
				case 1:
					// Medium
					GameManager.instance.LoadScene("Medium");
					Game.instance.InitializeGame();
					break;
				case 2:
					// Hard
					GameManager.instance.LoadScene("Hard");
					Game.instance.InitializeGame();
					break;
				case 3:
					// Impossible
					GameManager.instance.LoadScene("Impossible");
					Game.instance.InitializeGame();
					break;
			}
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
		menu.selectedButton = id;
	}
	void OnMouseExit()
	{
		mouseOver = false;
	}
}
