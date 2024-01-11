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
    public GameObject mask;
    public GameObject elements;

    public List<bool> booleanSettings;
    void Start()
    {
        settings = Settings.instance;
        
        // 1. Disable Animations
        booleanSettings = new List<bool> {false};
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) ToggleMenu();
        
        elements.SetActive(isMenuEnabled);
        
        if (!isMenuEnabled) return;

        settings.disableAnimations = booleanSettings[0];
    }

    public void EnableMenu()
    {
        isMenuEnabled = true;
        mask.SetActive(true);
    }

    public void DisableMenu()
    {
        isMenuEnabled = false;
        mask.SetActive(false);
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
