using UnityEngine;

public class sNumericalArrow : MonoBehaviour
{
	public sNumericalInput parent;
	public int valueIncrement;

	bool isMouseDown;

	const float rapidTriggerCooldown = 0.4f;
	float cooldown;
	int holdChain;

	void Update()
	{
		cooldown -= Time.deltaTime;

		if (cooldown > 0f)
			return;
		cooldown = 0f;

		if (!isMouseDown) return;
		parent.value += valueIncrement;

		holdChain++;
		holdChain = Mathf.Clamp(holdChain, 1, 10);
		cooldown = rapidTriggerCooldown / holdChain;
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
}
