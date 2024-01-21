using UnityEngine;
using System.Collections.Generic;

public class sMenu : MonoBehaviour
{
	public static sMenu instance;
	Settings settings;
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
			1f, // Sound volume
		};
	}

	void Update()
	{
		elements.SetActive(isMenuEnabled);
		
		if (!isMenuEnabled) return;
		
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (!ignoreEscaping)
			{
				DisableMenu();
				mMenu.instance.EnableMenu();
			}
			else
			{
				ignoreEscaping = false;
			}
		}
		settings.disableAnimations = booleanSettings[0];
		settings.limitFPS = booleanSettings[1];
		settings.targetFrameRate = intSettings[0];
		settings.soundVolume = floatSettings[0];
	}

	public void EnableMenu()
	{
		isMenuEnabled = true;
		//settings.soundVolume = 1f;
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
		}
		else
		{
			EnableMenu();
		}
	}
}
