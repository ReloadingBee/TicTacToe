using UnityEngine;
using System.Collections.Generic;

public class sMenu : MonoBehaviour
{
    Settings settings;

    public static sMenu instance;
    void Awake()
    {
        settings = Settings.instance;
        instance = this;
    } // Singleton

    public bool isMenuEnabled = false;
    public GameObject mask;

    public List<bool> booleanSettings;
    private void Start()
    {
        booleanSettings = new List<bool>();

        // 0 - Disable animations
        booleanSettings.Add(false);
    }

    private void Update()
    {
        if (!isMenuEnabled) return;

        settings.useAnimations = booleanSettings[0];
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
