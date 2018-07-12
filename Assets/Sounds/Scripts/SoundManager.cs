using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

	public AudioSource effects;
	public AudioSource music;
	public static SoundManager instance = null;
	private AudioClip musicLoop;
	private string[] songsToLoad = {"overworld", "spooky", "fight"};
	private List<AudioClip[]> songs = new List<AudioClip[]>();
	private string[] soundsToLoad = {"crash1", "crash2", "crash3", "crash4",
									"plopp1", "plopp2", "mouth1-1", "mouth2-1", 
									"mouth3-1", "mouth4-1", "mouth5-1", "tssss1", 
									"tssss2", "pew1", "shield"};
	private List<AudioClip> sounds = new List<AudioClip>();
	private int currentTrack = 0;

	// Use this for initialization
	void Awake () {
		if (instance == null) {
			instance = this;
		} else if (instance != null) {
			Destroy(gameObject);
		}



		foreach (string song in songsToLoad) {
			songs.Add(
				new AudioClip[] {
					(Resources.Load(song + "-1") as AudioClip), 
					(Resources.Load(song + "-2") as AudioClip)
					});
		}

		foreach (string sound in soundsToLoad) {
			sounds.Add(Resources.Load(sound) as AudioClip);
		}

		PlayMusic(0);

		DontDestroyOnLoad(gameObject);
	}

	public void PlayAudio(int sound) {
		effects.clip = sounds[mod(sound, sounds.Count)];
		effects.Play();
	}

	public void PlayRandomize(float pitch, params int[] sound) {
		int random = Random.Range(0, sound.Length);
		effects.clip = sounds[mod(sound[mod(random, sound.Length)], sounds.Count)];
		effects.pitch = Random.Range(1 - pitch, 1 + pitch);
		effects.Play();
	}

	public void PlayMusic(int number) {
		currentTrack = number;
		music.loop = false;
		music.clip = songs[currentTrack][0];
		music.Play();
		Invoke("PlayMusicLoop", music.clip.length - 0.5f); 
	}

	private void PlayMusicLoop() {
		music.loop = true;
		music.clip = songs[currentTrack][1];
		music.Play();
	}

	private int mod(int x, int m) {
		return ((x % m) + m) % m;
	}
}
