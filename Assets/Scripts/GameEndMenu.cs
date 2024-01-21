using TMPro;
using UnityEngine;

public class GameEndMenu : MonoBehaviour
{
	public static GameEndMenu instance;
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

	public TMP_Text text;
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			selectedButton--;
			selectedButton = Mathf.Clamp(selectedButton, 0, 1);
		}
		if (Input.GetKeyDown(KeyCode.DownArrow))
		{
			selectedButton++;
			selectedButton = Mathf.Clamp(selectedButton, 0, 1);
		}
		
		if (lastIsMenuEnabled == isMenuEnabled)
			return;
		// Optimization: only set the elements.SetActive() if it's changed
		elements.SetActive(isMenuEnabled);
		lastIsMenuEnabled = isMenuEnabled;
	}

	public void EnableMenu()
	{
		if (Game.instance.winner == '-')
		{
			// Draw
			text.text = $"It's a draw!";
		}
		else
		{
			// Someone won
			text.text = $"{Game.instance.winner.ToString().ToUpper()} has Won!";
		}
		Invoke("ActuallyEnableMenu", 3f);
	}
	public void DisableMenu()
	{
		isMenuEnabled = false;
	}

	void ActuallyEnableMenu()
	{
		// The deadline is in an hour OK? Writing bad code just so it works
		isMenuEnabled = true;
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
