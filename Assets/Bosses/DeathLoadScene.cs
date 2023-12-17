using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class DeatLoadScene : MonoBehaviour
{
    [SerializeField]
    private string sceneToLoad = "FinalCutScene";
    public bool destroy = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (destroy)
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
