using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialTrigger : MonoBehaviour
{
    public GameObject tutorialPanel;
    public TutorialData tutorialData;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            tutorialPanel.GetComponentInChildren<TextMeshProUGUI>().text = tutorialData.tutorialMessage;
            tutorialPanel.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            tutorialPanel.SetActive(false);
        }
    }
}
