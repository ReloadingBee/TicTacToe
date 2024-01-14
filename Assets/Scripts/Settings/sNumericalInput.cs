using TMPro;
using UnityEngine;

public class sNumericalInput : MonoBehaviour
{
	sMenu menu;
	void Start()
	{
		menu = sMenu.instance;
		typedText = value.ToString();
	}

	public int id;

	public int value;
	public int lowerLimit;
	public int higherLimit;

	public TMP_Text text;

	void Update()
	{
		value = Mathf.Clamp(value, lowerLimit, higherLimit);
		menu.intSettings[id] = value;

		text.text = value.ToString();

		if (isActive)
		{
			// Check if clicked outside of the input field
			if (Input.GetMouseButtonDown(0) && !isMouseOver)
			{
				UpdateValue();
			}
			// Check if pressed Enter or Escape keys
			if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Escape))
			{
				UpdateValue();
			}
		}

		if (!isActive) return;

		// WRITE
		for (int i = 0; i < 10; i++)
		{
			if (Input.GetKeyDown(KeyCode.Alpha0 + i))
			{
				if (typedText.Length < 5) typedText += i;
			}
		}
		// DELETE
		if (Input.GetKeyDown(KeyCode.Backspace))
		{
			if (typedText.Length > 0)
			{
				typedText = typedText[..^1]; // Deletes the last character
			}
		}
		text.text = typedText;
	}

	void UpdateValue()
	{
		isActive = false;

		// Turn typedText into an int
		// Also make it fit into the boundaries of lowerLimit & higherLimit
		value = typedText.Length > 0 ? int.Parse(typedText) : lowerLimit;
		if (value > higherLimit) value = higherLimit;

		// Update the text
		typedText = value.ToString();
		text.text = typedText;
		menu.ignoreEscaping = false;
	}

	public bool isActive;
	public string typedText;

	bool isMouseOver;

	void OnMouseDown()
	{
		typedText = value.ToString();
		menu.ignoreEscaping = true;
		isMouseOver = true;
		isActive = true;
	}
	void OnMouseExit()
	{
		isMouseOver = false;
	}
}
