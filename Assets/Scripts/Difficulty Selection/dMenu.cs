using UnityEngine;

public class dMenu : MonoBehaviour
{
	public static dMenu instance;
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

	public bool isMenuEnabled1 = true;
	public bool isMenuEnabled2 = true;
	public int menuID = 1;
	
	public GameObject elements1;
	public GameObject elements2;
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
		elements1.SetActive(isMenuEnabled1);
		elements2.SetActive(isMenuEnabled2);
	}

	public void EnableMenu1()
	{
		isMenuEnabled1 = true;
		menuID = 1;
	}
	public void DisableMenu1()
	{
		isMenuEnabled1 = false;
	}
	
	public void EnableMenu2()
	{
		isMenuEnabled2 = true;
		menuID = 2;
	}
	public void DisableMenu2()
	{
		isMenuEnabled2 = false;
	}

	public int selectedButton;
	public GameObject indicator1;
	public GameObject indicator2;
	public Vector3 IndicatorPosition1
	{
		get
		{
			return indicator1.transform.position;
		}
		set
		{
			indicator1.transform.position = value;
		}
	}
	public Vector3 IndicatorPosition2
	{
		get
		{
			return indicator2.transform.position;
		}
		set
		{
			indicator2.transform.position = value;
		}
	}
}
