using UnityEngine;

public class sButton : MonoBehaviour
{
    sMenu menu;

    private void Start()
    {
        menu = sMenu.instance;
    }

    private void OnMouseDown()
    {
        menu.ToggleMenu();
    }
}
