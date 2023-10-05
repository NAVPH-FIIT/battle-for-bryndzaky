using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public void Start() {
        transform.Find("VolumeSlider").GetComponent<Slider>().value = ConfigManager.Instance.GetVolume(ConfigManager.Volume.VolumeMaster);
    }
    public void SetVolume(float value) {
        ConfigManager.Instance.SetVolume(ConfigManager.Volume.VolumeMaster, value);
        //Debug.Log(value);
    }
}
