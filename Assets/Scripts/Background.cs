using System;
using UnityEngine;

public class Background : MonoBehaviour
{
	Settings settings;
	
	public Vector3 movementDirection = new Vector3(-1f, -1f, 0);
	public float loopDistance = 1.2f;

	void Start()
	{
		DontDestroyOnLoad(gameObject);
		settings = Settings.instance;
	}

	void Update()
	{
		if (settings.disableAnimations) return;
		transform.position += movementDirection * Time.deltaTime;

		if (Mathf.Abs(transform.position.x) > loopDistance || Mathf.Abs(transform.position.y) > loopDistance)
		{
			transform.position = Vector3.zero;
		}
	}
}
