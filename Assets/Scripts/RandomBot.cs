using UnityEngine;

public class RandomBot : MonoBehaviour
{
    GameManager gameManager;
    void Start()
    {
        gameManager = GameManager.instance;
    }
    void Update()
    {
        if (gameManager.gameEnded) return;
        int move = Random.Range(0, 9); // [0, 9)
        gameManager.Move(move);
    }
}
