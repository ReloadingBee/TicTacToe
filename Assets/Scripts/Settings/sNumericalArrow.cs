using UnityEngine;

public class sNumericalArrow : MonoBehaviour
{
	public sNumericalInput parent;
	public int valueIncrement;

	bool isMouseDown;
	bool isMouseOver;

	const float rapidTriggerCooldown = 0.4f;
	float cooldown;
	int holdChain;

	const float animationSpeed = 3;
	const float maxScale = 1.2f;

	void Update()
	{
		cooldown -= Time.deltaTime;

		transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one * (isMouseOver ? maxScale : 1f), animationSpeed * Time.deltaTime);
		
		if (cooldown > 0f)
			return;
		cooldown = 0f;

		if (!isMouseDown || !isMouseOver) return;
		parent.value += valueIncrement;

		holdChain++;
		holdChain = Mathf.Clamp(holdChain, 1, 10);
		cooldown = rapidTriggerCooldown / holdChain;
		transform.localScale = Vector3.one * 1f; // Click effect
	}

	void OnMouseDown()
	{
		isMouseDown = true;
		holdChain = 0;
	}
    void OnMouseUp()
    {
		isMouseDown = false;
		cooldown = 0f;
    }
    void OnMouseOver()
    {
	    isMouseOver = true;
    }
    void OnMouseExit()
    {
	    isMouseOver = false;
    }
}
