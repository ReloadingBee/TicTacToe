using System;
using UnityEngine;

public class mButton : MonoBehaviour
{
	mMenu menu;

	void Start()
	{
		menu = mMenu.instance;
	}

	[Range(0, 4)] public int id;

	bool mouseOver;

	void Update()
	{
		if (mouseOver)
		{
			menu.selectedButton = id;
		}
	}
	void OnMouseOver()
	{
		mouseOver = true;
	}
	void OnMouseExit()
	{
		mouseOver = false;
	}
	void OnMouseDown()
	{
		menu.Click();
	}
}
