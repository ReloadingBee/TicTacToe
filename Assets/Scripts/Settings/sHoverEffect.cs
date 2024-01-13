using UnityEngine;

public class sHoverEffect : MonoBehaviour
{
	SpriteRenderer spriteRenderer;

	public Color defaultColor = Color.white;
	public Color hoverColor = new Color(0.8f, 0.8f, 0.8f, 1f);

	void Start()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		defaultColor = spriteRenderer.color;
	}

	void OnMouseOver()
	{
		// Change the color when the mouse is over
		spriteRenderer.color = hoverColor;
	}

	void OnMouseExit()
	{
		// Reset the color when the mouse exits
		spriteRenderer.color = defaultColor;
	}
}
