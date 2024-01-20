using UnityEngine;

public class mMenu : MonoBehaviour
{
	public static mMenu instance;
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
		
		lastIsMenuEnabled = isMenuEnabled;
	} // Singleton

	public bool isMenuEnabled = true;
	bool lastIsMenuEnabled;
	
	public GameObject elements;
	
	public string sceneScript = "mMenu";
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			selectedButton--;
			selectedButton = Mathf.Clamp(selectedButton, 0, 3);
		}
		if (Input.GetKeyDown(KeyCode.DownArrow))
		{
			selectedButton++;
			selectedButton = Mathf.Clamp(selectedButton, 0, 3);
		}
		
		if (lastIsMenuEnabled == isMenuEnabled)
			return;
		// Optimization: only set the elements.SetActive() if it's changed
		elements.SetActive(isMenuEnabled);
		lastIsMenuEnabled = isMenuEnabled;
	}

	public void EnableMenu()
	{
		isMenuEnabled = true;
	}
	public void DisableMenu()
	{
		isMenuEnabled = false;
	}

	public int selectedButton;
	public GameObject indicator;
	public Vector3 IndicatorPosition
	{
		get
		{
			return indicator.transform.position;
		}
		set
		{
			indicator.transform.position = value;
		}
	}
}
