using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMusicPlayer : MonoBehaviour
{
  public AudioClip[] clips;
  private AudioSource audioSource;

  void Start()
  {
    audioSource = GetComponent<AudioSource>();
    PlayRandomMusic();
  }

  void Update()
  {
    if (!audioSource.isPlaying)
    {
      PlayRandomMusic();
    }
  }

  void PlayRandomMusic()
  {
    int randomIndex = Random.Range(0, clips.Length);
    audioSource.clip = clips[randomIndex];
    audioSource.Play();
  }
}
