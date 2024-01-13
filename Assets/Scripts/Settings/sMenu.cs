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
    void Start()
    {
        settings = Settings.instance;
        
        // 1. Disable Animations
        // 2. Limit FPS
        booleanSettings = new List<bool> {false, false};
        
        // 1. Max FPS
        intSettings = new List<int> {60};
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) ToggleMenu();
        
        elements.SetActive(isMenuEnabled);
        
        if (!isMenuEnabled) return;

        settings.disableAnimations = booleanSettings[0];
        settings.limitFPS = booleanSettings[1];
        settings.targetFrameRate = intSettings[0];
    }

    public void EnableMenu()
    {
        isMenuEnabled = true;
        fade.SetActive(true);
    }

    public void DisableMenu()
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
