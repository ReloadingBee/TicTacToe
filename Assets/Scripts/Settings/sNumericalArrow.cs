using UnityEngine;

public class sNumericalArrow : MonoBehaviour
{
	public sNumericalInput parent;
	public int valueIncrement;

	bool mouseDown;
	bool mouseOver;

	const float rapidTriggerCooldown = 0.4f;
	float cooldown;
	int holdChain;

	const float animationSpeed = 3;
	const float maxAnimationScale = 1.2f;
	
	Vector3 lastLocalScale;

	void Update()
	{
		cooldown -= Time.deltaTime;

		var localScale = Vector3.Lerp(transform.localScale, Vector3.one * (mouseOver ? maxAnimationScale : 1f), animationSpeed * Time.deltaTime);
		if (lastLocalScale != localScale) transform.localScale = lastLocalScale = localScale;
		
		if (cooldown > 0f)
			return;
		cooldown = 0f;

		if (!mouseDown || !mouseOver) return;
		parent.value += valueIncrement;

		holdChain++;
		holdChain = Mathf.Clamp(holdChain, 1, 10);
		cooldown = rapidTriggerCooldown / holdChain;
		transform.localScale = Vector3.one * 1f; // Click animation
		GameManager.instance.PlayRandomSound(GameManager.instance.tickSounds);
	}

	void OnMouseDown()
	{
		mouseDown = true;
		holdChain = 0;
	}
    void OnMouseUp()
    {
		mouseDown = false;
		cooldown = 0f;
    }
    void OnMouseOver()
    {
	    mouseOver = true;
    }
    void OnMouseExit()
    {
	    mouseOver = false;
    }
}
