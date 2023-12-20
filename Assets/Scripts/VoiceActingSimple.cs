using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceActingSimple : MonoBehaviour
{
  // Start is called before the first frame update
  public AudioClip voiceSound;
  public AudioSource source;
  private bool played = false;
  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.CompareTag("Player"))
    {
      if (played) return;
      this.source.PlayOneShot(this.voiceSound, 0.25f);
      played = true;
      StartCoroutine(DeactivateAfterSound(this.voiceSound.length));

    }
  }


  private IEnumerator DeactivateAfterSound(float delay)
  {
    // Wait for the length of the sound clip
    yield return new WaitForSeconds(delay);

    // Deactivate the parent object, or the current object if no parent is found
    Destroy(gameObject);
  }
}

