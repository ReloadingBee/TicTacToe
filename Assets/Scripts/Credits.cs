using UnityEngine;

public class Credits : MonoBehaviour
{
	public static Credits instance;
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
	}

	public bool isCreditsEnabled;
	bool lastIsCreditsEnabled;

	public GameObject elements;
	void Update()
	{
		elements.SetActive(isCreditsEnabled);
		
		if (!isCreditsEnabled) return;

		if (Input.GetKeyDown(KeyCode.Escape))
		{
			DisableMenu();
			mMenu.instance.EnableMenu();
		}

		if (lastIsCreditsEnabled == isCreditsEnabled)
			return;
		// Optimization: only set the elements.SetActive() if it's changed
		elements.SetActive(isCreditsEnabled);
		lastIsCreditsEnabled = isCreditsEnabled;
	}

	public void EnableMenu()
	{
		isCreditsEnabled = true;
	}
	public void DisableMenu()
	{
		isCreditsEnabled = false;
	}
}
