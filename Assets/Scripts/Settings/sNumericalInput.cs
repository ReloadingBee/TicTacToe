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
	
	public bool isTyping;
	public string typedText;
	
	bool isMouseOver;

	public TMP_Text text;

	int lastValue;
	

	void Update()
	{
		if (lastValue != value)
		{
			value = Mathf.Clamp(value, lowerLimit, higherLimit);
			menu.intSettings[id] = value;
			text.text = value.ToString();
			lastValue = value;
		}

		if (!isTyping)
			return;
		
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

		// WRITE
		for (var i = 0; i < 10; i++)
		{
			if (!Input.GetKeyDown(KeyCode.Alpha0 + i) && !Input.GetKeyDown(KeyCode.Keypad0 + i))
				continue;
			if (typedText.Length < 5) typedText += i;
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
		isTyping = false;

		// Turn typedText into an int
		// Also make it fit into the boundaries of lowerLimit & higherLimit
		value = typedText.Length > 0 ? int.Parse(typedText) : lowerLimit;
		if (value > higherLimit) value = higherLimit;

		// Update the text
		typedText = text.text = value.ToString();
		menu.ignoreEscaping = false;
	}
	void OnMouseDown()
	{
		typedText = value.ToString();
		menu.ignoreEscaping = true;
		isMouseOver = true;
		isTyping = true;
	}
	void OnMouseExit()
	{
		isMouseOver = false;
	}
}
