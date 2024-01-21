using UnityEngine;

public class sBoolean : MonoBehaviour
{
    sMenu menu;

    void Start()
    {
        menu = sMenu.instance;
    }

    public int id;
    public bool isToggled;
    bool mouseOver;
    
    public GameObject filledCheckbox;

    const float animationSpeed = 5;
    const float maxScale = 1.1f;
    void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one * (mouseOver ? maxScale : 1f), animationSpeed * Time.deltaTime);
        if (!menu.isMenuEnabled)
            return;
        filledCheckbox.SetActive(isToggled);
        filledCheckbox.transform.localScale = transform.localScale;
    }
    void OnMouseDown()
    {
        isToggled = !isToggled;
        menu.booleanSettings[id] = isToggled;
        GameManager.instance.PlayRandomSound(GameManager.instance.tickSounds);
    }
    
    
    SpriteRenderer spriteRenderer;
    public Color filledCheckboxHoverColor = new Color(0.8f, 0.8f, 0.8f, 1f);
    
    void OnMouseOver()
    {
        mouseOver = true;
        spriteRenderer = filledCheckbox.GetComponent<SpriteRenderer>();
        spriteRenderer.color = filledCheckboxHoverColor;
    }
    void OnMouseExit()
    {
        mouseOver = false;
        spriteRenderer.color = Color.white;
    }
}
