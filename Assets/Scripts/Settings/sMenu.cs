using UnityEngine;
using System.Collections.Generic;

public class sMenu : MonoBehaviour
{
	public static sMenu instance;
	Settings settings;
	mMenu menu;
	void Awake()
	{
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
		ignoreEscaping = false;
	} // Singleton

	public bool isMenuEnabled;
	public GameObject elements;

	public List<bool> booleanSettings;
	public List<int> intSettings;
	public List<float> floatSettings;

	public bool ignoreEscaping;
	void Start()
	{
		settings = Settings.instance;
		menu = mMenu.instance;

		booleanSettings = new List<bool>
		{
			false, // 1. Disable Animations
			false // 2. Limit FPS
		};

		// 1. Max FPS
		intSettings = new List<int>
		{
			60 // Max FPS
		};

		floatSettings = new List<float>
		{
			1f, // 1. Music volume
			1f // 2. Effects volume
		};
	}

	void Update()
	{
		elements.SetActive(isMenuEnabled);
		
		if (!isMenuEnabled) return;
		
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (!ignoreEscaping) ToggleMenu();
			else
			{
				ignoreEscaping = false;
			}
		}
		settings.disableAnimations = booleanSettings[0];
		settings.limitFPS = booleanSettings[1];
		settings.targetFrameRate = intSettings[0];
		settings.musicVolume = floatSettings[0];
	}

	public void EnableMenu()
	{
		isMenuEnabled = true;
	}
	public void DisableMenu()
	{
		isMenuEnabled = false;
	}
	public void ToggleMenu()
	{
		if (isMenuEnabled)
		{
			DisableMenu();
			menu.EnableMenu();
		}
		else
		{
			EnableMenu();
		}
	}
}
