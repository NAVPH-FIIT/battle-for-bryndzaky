using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine.UI;
using System.Threading;

public class SceneChanger : MonoBehaviour
{
  [HideInInspector] public static SceneChanger Instance { get; private set; }
  [SerializeField] private Animator transition;
  [SerializeField] private float transitionTime = 1f;
  private Image transitionImage;
  private TextMeshProUGUI transitionText;
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
    this.transitionImage.raycastTarget = false;
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

  public void ChangeScene(string scene, string transitionText, bool red = false)
  {
    this.transitionText.text = transitionText;
    StartCoroutine(LoadLevel(this.sceneTranslation[scene.ToLower()], red));
    // LoadLevel(this.sceneTranslation[scene.ToLower()], red);
  }

  private IEnumerator LoadLevel(int sceneIndex, bool red)
  // private void LoadLevel(int sceneIndex, bool red)
  {
    this.transitionImage.raycastTarget = true;
    this.transitionImage.color = red ? Color.red : Color.black;
    GetComponentInChildren<CanvasGroup>().alpha = 1;

    AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex);

    while (!asyncLoad.isDone)
    {
        float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);
        Debug.Log("Loading progress: " + (progress * 100) + "%");

        yield return null;
    }

    GetComponentInChildren<CanvasGroup>().alpha = 0;
    this.transitionImage.raycastTarget = false;
  }
}
