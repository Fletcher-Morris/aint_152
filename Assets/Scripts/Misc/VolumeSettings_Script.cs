using UnityEngine;

public class VolumeSettings_Script : MonoBehaviour {

    public float volume;
    public bool useGameSettings = true;
    public float multiplier = 1;

    private void Update()
    {
        if (useGameSettings)
        {
            volume = (GameObject.Find("GM").GetComponent<GamePrefs_Script>().gamePrefs.volumeLevel / 10) * multiplier;
        }
        else
        {
            gameObject.GetComponent<AudioSource>().volume = (volume / 10) * multiplier;
        }

        gameObject.GetComponent<AudioSource>().volume = volume;
    }
}
