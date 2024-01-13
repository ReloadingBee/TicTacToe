using UnityEngine;

public class sNumericalArrow : MonoBehaviour
{
	public sNumericalInput parent;
	public int valueIncrement;
	void OnMouseDown()
	{
		parent.value += valueIncrement;
	}
}
