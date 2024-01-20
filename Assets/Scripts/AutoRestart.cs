using UnityEngine;

public class AutoRestart : MonoBehaviour
{
	public float restartDelayInSeconds;
	bool haveInvoked;
	void Update()
	{
		if (!haveInvoked && Game.instance.hasGameEnded)
		{
			haveInvoked = true;
			Invoke("Restart", restartDelayInSeconds);
		}
	}

	void Restart()
	{
		Game.instance.InitializeGame();
		haveInvoked = false;
	}
}
