using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

	public AudioSource music;
	public static SoundManager instance = null;
	private AudioClip musicLoop;

    private List<AudioClip[]> songs;
    private List<AudioClip> sounds;

    private int currentTrack = 0;

	// Use this for initialization
	void Awake () {
		if (instance == null) {
			instance = this;
		} else if (instance != null) {
			Destroy(gameObject);
		}
        songs = PrefabRepository.instance.Songs;
        sounds = PrefabRepository.instance.Sounds;

		PlayMusic(0);

		//DontDestroyOnLoad(gameObject);
	}

	public void PlayAudio(int sound) {
		AudioSource effect = gameObject.AddComponent(typeof(AudioSource)) as AudioSource; 
		effect.clip = sounds[mod(sound, sounds.Count)];
		effect.Play();
	}

	public void PlayRandomize(float pitch, params int[] sound) {
		AudioSource effect = gameObject.AddComponent(typeof(AudioSource)) as AudioSource; 
		int random = Random.Range(0, sound.Length);
		effect.clip = sounds[mod(sound[mod(random, sound.Length)], sounds.Count)];
		effect.pitch = Random.Range(1 - pitch, 1 + pitch);
		effect.Play();
		Destroy(effect, effect.clip.length);
	}

	public void PlayMusic(int number) {
		currentTrack = number;
		music.loop = false;
		music.clip = songs[currentTrack][0];
		music.Play();
		Invoke("PlayMusicLoop", music.clip.length + 0.5f); 
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
