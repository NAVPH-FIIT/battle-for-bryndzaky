using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour
{
  [HideInInspector] public static SceneChanger Instance { get; private set; }
  [SerializeField] private Animator transition;
  [SerializeField] private float transitionTime = 1f;
  Image transitionImage;
  TextMeshProUGUI transitionText;
  private readonly Dictionary<string, int> sceneTranslation = new Dictionary<string, int> 
  {
    { "hub", 2 },
    { "level_1", 1 },
    { "level_2", 3 }
  };

  private void Start()
  {
    this.transitionText = GetComponentInChildren<TextMeshProUGUI>();
    this.transitionImage = GetComponentInChildren<Image>();
  }
  private void Awake()
  {
      if (Instance == null)
      {
          Instance = this;
          DontDestroyOnLoad(gameObject);
      }
      else
      {
          Destroy(gameObject);
      }
  }

  public void ChangeScene(string scene, string transitionText)
  {
    this.transitionText.text = transitionText;
    StartCoroutine(LoadLevel(this.sceneTranslation[scene]));
    // LoadLevel(sceneIndex);
  }

  private IEnumerator LoadLevel(int sceneIndex)
  // private void LoadLevel(int sceneIndex)
  {
    this.transitionImage.enabled = true;
    transition.SetTrigger("Start");

    yield return new WaitForSeconds(this.transitionTime);
    // transition.
    SceneManager.LoadScene(sceneIndex);
    // transition.SetTrigger("End");
  }
  // public string sceneName;
  // public Animator transition;
  // public float transitionTime = 1f;
  // public void OnTriggerEnter2D(Collider2D other)
  // {
  //   if (other.CompareTag("Player"))
  //   {
  //     ChangeScene();
  //   }
  // }

  // public void ChangeScene()
  // {
  //   StartCoroutine(LoadLevel(sceneName));
  // }

  // private IEnumerator LoadLevel(string levelName)
  // {
  //   transition.SetTrigger("Start");

  //   yield return new WaitForSeconds(transitionTime);

  //   SceneManager.LoadScene(sceneName);
  // }
}
