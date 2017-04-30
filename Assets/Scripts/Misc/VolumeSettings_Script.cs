using UnityEngine;

public class VolumeSettings_Script : MonoBehaviour {

    public float volume;
    public bool useMusicVolumeSettings = false;
    public bool useEffectVolumeSettings = true;
    public float multiplier = 1;

    private void Update()
    {
        if (useEffectVolumeSettings)
        {
            volume = (GameObject.Find("GM").GetComponent<GamePrefs_Script>().gamePrefs.effectVolumeLevel / 10) * multiplier;
        }
        else if (useMusicVolumeSettings)
        {
            volume = (GameObject.Find("GM").GetComponent<GamePrefs_Script>().gamePrefs.musicVolumeLevel / 10) * multiplier;
        }
        else
        {
            gameObject.GetComponent<AudioSource>().volume = (volume / 10) * multiplier;
        }

        gameObject.GetComponent<AudioSource>().volume = volume;
    }
}
