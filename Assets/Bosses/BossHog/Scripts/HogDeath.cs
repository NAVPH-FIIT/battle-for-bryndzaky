using Bryndzaky.Units.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HogDeath : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject deathEnable;
    [SerializeField] private int reward_xp;
    [SerializeField] private int reward_gold;

    private void OnDestroy()
    {
        Player.Instance.GrantReward(this.reward_xp, this.reward_gold);
        deathEnable.SetActive(true);
    }
}
