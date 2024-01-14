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
	} // Singleton

	public bool isMenuEnabled;
	public GameObject fade;
	public GameObject elements;

	public List<bool> booleanSettings;
	public List<int> intSettings;

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
			60
		};
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (!ignoreEscaping) ToggleMenu();
			else
			{
				ignoreEscaping = false;
			}
		}

		elements.SetActive(isMenuEnabled);

		if (!isMenuEnabled) return;

		settings.disableAnimations = booleanSettings[0];
		settings.limitFPS = booleanSettings[1];
		settings.targetFrameRate = intSettings[0];
	}

	void EnableMenu()
	{
		isMenuEnabled = true;
		fade.SetActive(true);
	}

	void DisableMenu()
	{
		isMenuEnabled = false;
		fade.SetActive(false);
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
