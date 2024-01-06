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

    public int targetFrameRate = 60;
    
    public float musicVolume = 1f; // 0.0f - 1.0f
    public float effectVolume = 1f;

    public bool useAnimations = true;
}
