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

    public bool limitFPS;
    [Range(5, 10000)] public int targetFrameRate = 60;
    
    [Range(0f, 1f)] public float musicVolume = 1f;
    [Range(0f, 1f)] public float effectVolume = 1f;

    public bool disableAnimations;
}
