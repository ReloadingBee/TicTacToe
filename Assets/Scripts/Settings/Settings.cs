using UnityEngine;

public class Settings : MonoBehaviour
{
	public static Settings instance;
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

    public bool disableAnimations;
    
    public bool limitFPS;
    [Range(5, 10000)] public int targetFrameRate = 60;
    
    [Range(0f, 1f)] public float soundVolume = 1f;
}
