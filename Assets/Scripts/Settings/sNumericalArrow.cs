using UnityEngine;

public class sNumericalArrow : MonoBehaviour
{
	public sNumericalInput parent;
	public int valueIncrement;

	public bool isMouseDown;

	[SerializeField] static float rapidTriggerCooldown = 0.4f;
	public float cooldown;
	[Range(1, 10)] public int holdChain;

	private void Update()
	{
		cooldown -= Time.deltaTime;

		if(cooldown <= 0f)
        {
			cooldown = 0f;

			if (!isMouseDown) return;
			parent.value += valueIncrement;

			holdChain++;
			holdChain = Mathf.Clamp(holdChain, 1, 10);
			cooldown = rapidTriggerCooldown / holdChain;
        }
	}

	void OnMouseDown()
	{
		isMouseDown = true;
		holdChain = 0;
	}
    private void OnMouseUp()
    {
		isMouseDown = false;
		cooldown = 0f;
    }
}
