using Bryndzaky.Combat.Spells;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    [SerializeField]
    private float maxHealth = 100;
    private float health;
    [SerializeField]
    private GameObject healthbar;
    [SerializeField]
    private Slider healthSlider;
    //Start is called before the first frame update

    private void Start()
    {
        healthbar.SetActive(true);
        health = maxHealth;
    }
    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Damage(int dealt)
    {
        health -= dealt;
        if (this.healthSlider != null)
            this.healthSlider.value = (float)health / maxHealth;
        Debug.Log("Auu");
    }

    private void OnDestroy()
    {
        healthbar.SetActive(false);
    }
}
