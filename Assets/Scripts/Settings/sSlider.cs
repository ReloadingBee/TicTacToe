using TMPro;
using UnityEngine;

public class sSlider : MonoBehaviour
{
	sMenu menu;
	void Start()
	{
		menu = sMenu.instance;
		cam = Camera.main;
		
		Main();
	}

	public int id;
	[Range(0f, 1f)] public float value;

	public float parentOffsetX;
	const float lengthX = 3.4f;
	const float lowerX = -(lengthX / 2);
	const float higherX = lengthX / 2;
	const float cornerOffset = 0.4f;

	bool mouseDown;
	bool mouseOver;
	bool isDragging;

	Camera cam;

	public GameObject squareShadow;
	public TMP_Text text;

	void Update()
	{
		menu.floatSettings[id] = value;
		// Drag check
		if (!isDragging)
		{
			if (mouseOver && mouseDown)
			{
				isDragging = true;
			}
			return;
		}
		Main();
	}

	void Main()
	{
		var mousePos = cam.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, 0));
		mousePos.x = Mathf.Clamp(mousePos.x - parentOffsetX, lowerX, higherX);
		
		transform.localPosition = new Vector2(mousePos.x, 0);

		// Set the value from 0 to 1
		value = Mathf.InverseLerp(lowerX, higherX, transform.localPosition.x);

		// Deal with the shadows
		squareShadow.transform.localPosition = new Vector2(Mathf.Lerp(lowerX, mousePos.x, 0.5f), 0);
		squareShadow.transform.localScale = new Vector2(mousePos.x + parentOffsetX - cornerOffset * 2, 0.2f);

		text.text = $"{Mathf.Round(value * 100)}%";
	}

	void OnMouseDown()
	{
		mouseDown = true;
	}
	void OnMouseUp()
	{
		mouseDown = false;
		isDragging = false;
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
