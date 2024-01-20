using UnityEngine;

public class mButton : MonoBehaviour
{
	mMenu mainMenu;

	[Range(0, 3)] public int id;

	bool mouseOver;
	bool mouseDown;

	const float animationSpeed = 5f;
	const float maxAnimationScale = 1.2f;

	RectTransform rect;

	const float indicatorOffsetY = 0.35f;
	const float indicatorOffsetX = 0.5f;

	Vector3 lastIndicatorPosition;
	Vector3 lastLocalScale;

	void Start()
	{
		mainMenu = mMenu.instance;

		rect = GetComponent<RectTransform>();
	}

	void Update()
	{
		if ((mouseOver && mouseDown) || (mainMenu.selectedButton == id && Input.GetKeyDown(KeyCode.Return)))
		{
			ClickHandler();
			OnMouseUp();
		}

		if (mainMenu.selectedButton == id)
		{
			// Optimization: only set the menu.IndicatorPosition if it's changed
			var indicatorPosition = new Vector3(rect.rect.size.x / 2 * transform.localScale.x + indicatorOffsetX, id * -1.5f + indicatorOffsetY);
			if (lastIndicatorPosition != indicatorPosition)
			{
				mainMenu.IndicatorPosition = lastIndicatorPosition = indicatorPosition;
			}
		}
		// Size animation
		// Optimization: only set the transform.localScale if it's changed
		var localScale = Vector3.Lerp(transform.localScale, Vector3.one * (mainMenu.selectedButton == id ? maxAnimationScale : 1f), animationSpeed * Time.deltaTime);
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
				// Play
				GameManager.instance.LoadScene("SampleScene");
				mainMenu.DisableMenu();
				break;
			case 1:
				// Settings
				mainMenu.DisableMenu();
				sMenu.instance.EnableMenu();
				break;
			case 2:
				// Credits
				mainMenu.DisableMenu();
				Credits.instance.EnableMenu();
				break;
			case 3:
				// Quit
				QuitGame();
				break;
		}
	}
	static void QuitGame()
	{
		// If running in the Unity Editor, exit play mode
#if UNITY_EDITOR
		UnityEditor.EditorApplication.ExitPlaymode();
#else
        // If not running in the Unity Editor, quit the application
        Application.Quit();
#endif
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
		mainMenu.selectedButton = id;
	}
	void OnMouseExit()
	{
		mouseOver = false;
	}
}
