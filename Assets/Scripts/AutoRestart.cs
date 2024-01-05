using UnityEngine;

public class AutoRestart : MonoBehaviour
{
	public float restartDelayInSeconds;
	bool haveInvoked;
	void Update()
	{
		if (haveInvoked || !GameManager.instance.gameEnded)
			return;
		haveInvoked = true;
		Invoke("Restart", restartDelayInSeconds);
	}

	void Restart()
	{
		GameManager.instance.InitializeGame();
		haveInvoked = false;
	}
}
