using UnityEngine;

public class sBoolean : MonoBehaviour
{
    sMenu menu;

    private void Start()
    {
        menu = sMenu.instance;
    }

    public int id;
    public bool isToggled = false;

    private void OnMouseDown()
    {
        isToggled = !isToggled;
        menu.booleanSettings[id] = isToggled;
    }
}
