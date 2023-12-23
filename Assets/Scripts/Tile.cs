using UnityEngine;
public class Tile : MonoBehaviour
{
	public int tileID;
	public Material xMaterial;
	public Material oMaterial;
	void OnMouseDown()
	{
		if (GameManager.instance.Move(tileID))
		{
			Material myMaterial = GameManager.instance.currentTurn == GameManager.OppositePlayer((char)GameManager.Players.x) ? xMaterial : oMaterial;
			Renderer rend = GetComponent<Renderer>();
			rend.material = myMaterial;
		}
	}
}
