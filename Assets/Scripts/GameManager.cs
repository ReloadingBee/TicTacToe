using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
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

	public TMP_Text turnText;
	public TMP_Text winnerText;
	public TMP_Text moveCountText;
	
	// Sounds
	public List<AudioClip> moveSounds;
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

		if (!game.hasGameStarted) return;
		// TODO: Assign texts
		//winnerText.text = $"Winner: {game.winner.ToString().ToUpper()}";
		//turnText.text = $"Current turn: {game.currentTurn.ToString().ToUpper()}";
		//moveCountText.text = $"Move count: {game.moveCount}";
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
