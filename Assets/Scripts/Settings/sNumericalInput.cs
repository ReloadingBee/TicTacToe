using TMPro;
using UnityEngine;

public class sNumericalInput : MonoBehaviour
{
	sMenu menu;
	void Start()
	{
		menu = sMenu.instance;
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
	}
}
