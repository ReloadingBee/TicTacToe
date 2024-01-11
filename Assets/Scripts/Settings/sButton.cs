using UnityEngine;

public class sButton : MonoBehaviour
{
    sMenu menu;

    void Start()
    {
        menu = sMenu.instance;
    }

    void OnMouseDown()
    {
        menu.ToggleMenu();
    }
}
