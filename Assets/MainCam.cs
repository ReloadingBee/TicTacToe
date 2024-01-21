using UnityEngine;

public class MainCam : MonoBehaviour
{
	public static MainCam instance;

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
}
