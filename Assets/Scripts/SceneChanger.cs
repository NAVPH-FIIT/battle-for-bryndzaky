using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class SceneChanger : MonoBehaviour
{
  public string sceneName;
  public Animator transition;
  public float transitionTime = 1f;
  public void OnTriggerEnter2D(Collider2D other)
  {
    if (other.CompareTag("Player"))
    {
      ChangeScene();
    }
  }

  public void ChangeScene()
  {
    StartCoroutine(LoadLevel(sceneName));
  }

  private IEnumerator LoadLevel(string levelName)
  {
    transition.SetTrigger("Start");

    yield return new WaitForSeconds(transitionTime);

    SceneManager.LoadScene(sceneName);
  }
}
