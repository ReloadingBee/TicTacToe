using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;
	Settings settings;
	Game game;

	void Awake()
	{
		settings = Settings.instance;

		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	} // Singleton magic
	
	public string activeScene;

	public bool isCooldownEnabled;
	public float remainingCooldown;
	
	// Sounds
	public List<AudioClip> moveSounds;
	public List<AudioClip> tickSounds;
	AudioSource source;

	public enum Players
	{
		x = 'x',
		o = 'o'
	} // To make everyone's life harder :)

	void Start()
	{
		game = Game.instance;
		source = GetComponent<AudioSource>();

		LoadScene(null);
	}

	void Update()
	{
		// Default targetFrameRate value is -1
		Application.targetFrameRate = settings.limitFPS ? settings.targetFrameRate : -1;
		source.volume = settings.soundVolume;

		// Cooldown logic
		if (isCooldownEnabled)
		{
			remainingCooldown -= Time.deltaTime;
			if (remainingCooldown < 0f)
			{
				remainingCooldown = 0f;
				isCooldownEnabled = false;
			}
		}
	}

	void EnableCooldown(float cooldown)
	{
		remainingCooldown = cooldown;
		isCooldownEnabled = true;
	}

	public void PlayRandomSound(List<AudioClip> sounds)
	{
		var sound = Random.Range(0, sounds.Count);
		source.PlayOneShot(sounds[sound]);
	}

	public void LoadScene([CanBeNull] string scene)
	{
		if (scene != null)
		{
			SceneManager.LoadScene(scene);
		}
		activeScene = SceneManager.GetActiveScene().name;
	}
}
