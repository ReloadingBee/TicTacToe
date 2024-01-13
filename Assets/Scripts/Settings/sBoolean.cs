using UnityEngine;

public class sBoolean : MonoBehaviour
{
    sMenu menu;

    void Start()
    {
        menu = sMenu.instance;
    }

    public int id;
    public bool isToggled = false;
    
    public GameObject filledCheckbox;

    void Update()
    {
        if (menu.isMenuEnabled)
        {
            filledCheckbox.SetActive(isToggled);
        }
    }
    void OnMouseDown()
    {
        isToggled = !isToggled;
        menu.booleanSettings[id] = isToggled;
    }
    
    
    SpriteRenderer spriteRenderer;
    public Color filledCheckboxHoverColor = new Color(0.8f, 0.8f, 0.8f, 1f);
    
    void OnMouseOver()
    {
        spriteRenderer = filledCheckbox.GetComponent<SpriteRenderer>();
        spriteRenderer.color = filledCheckboxHoverColor;
    }
    void OnMouseExit()
    {
        spriteRenderer.color = Color.white;
    }
}
