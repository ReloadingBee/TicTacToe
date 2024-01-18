using UnityEngine;

public class mMenu : MonoBehaviour
{
	public static mMenu instance;
	GameManager gameManager;
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
	void Start()
	{
		gameManager = GameManager.instance;
		settings = Settings.instance;
	}

	public bool isMenuEnabled = true;

	public int selectedButton;
	public GameObject indicator;
	const float offsetY = 0.4f; // Originally 0.5f, but -0.1f because of a weird Text alignment
	const float offsetX = 5;

	public GameObject elements;

	bool isTransitioning;
	const float transitionTime = 1f;
	float remainingTransitionTime;

	void Update()
	{
		elements.gameObject.SetActive(!isTransitioning && isMenuEnabled);
		if (isTransitioning)
		{
			remainingTransitionTime -= Time.deltaTime;
			gameManager.Transition(-180f);
		}
		else indicator.transform.position = Vector3.right * offsetX + Vector3.up * (selectedButton * -1.5f + offsetY);
		if (!(remainingTransitionTime <= 0) || !isTransitioning)
			return;
		remainingTransitionTime = 0;
		isTransitioning = false;
		isMenuEnabled = false;
		gameManager.EndTransition();
		gameManager.LoadScene("SampleScene", true);
	}

	public void Click()
	{
		isTransitioning = true;
		remainingTransitionTime = transitionTime;
	}
}
