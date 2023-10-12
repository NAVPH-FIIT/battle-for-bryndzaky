using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ConfigManager : MonoBehaviour
{
    [HideInInspector]
    public enum Volume {
        VolumeMaster
    }

    [HideInInspector]
    public static ConfigManager Instance { get; private set; }
    [HideInInspector]
    public static Dictionary<string, object> config = new Dictionary<string, object>
        {
            { "combat.knockback.strength",  10f },
            { "combat.knockback.delay",     0.05f },
        };

    [SerializeField]
    private AudioMixer MixerMaster;

    // Start is called before the first frame update
    void Start()
    {
        this.SetVolume(Volume.VolumeMaster, PlayerPrefs.GetFloat(Volume.VolumeMaster.ToString(), 0f));
    }

    // Update is called once per frame
    void Update()
    {
        
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

    public float GetVolume(Volume volume) {
        return PlayerPrefs.GetFloat(volume.ToString(), 0f);
    }

    public void SetVolume(Volume volume, float value) {
        MixerMaster.SetFloat(volume.ToString(), value);
        
        PlayerPrefs.SetFloat(volume.ToString(), value);
        PlayerPrefs.Save();
    }
}
