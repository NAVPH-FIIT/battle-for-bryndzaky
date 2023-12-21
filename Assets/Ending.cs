using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ending : MonoBehaviour
{
    [SerializeField] private int endGameAfter = 80;
    void Start()
    {
        StartCoroutine(EndGame());
    }

    private IEnumerator EndGame()
    {
        yield return new WaitForSeconds(endGameAfter);
        SceneChanger.Instance.ChangeScene("menu", "Koniec?");
    }
}
