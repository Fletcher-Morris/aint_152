using UnityEngine;

public class VolumeSettings_Script : MonoBehaviour {

    public float volume;
    public bool useGameSettings = true;

    private void Update()
    {
        if (useGameSettings)
        {
            gameObject.GetComponent<AudioSource>().volume = GameObject.Find("GM").GetComponent<GamePrefs_Script>().gamePrefs.volumeLevel / 10;
            volume = GameObject.Find("GM").GetComponent<GamePrefs_Script>().gamePrefs.volumeLevel / 10;
        }
        else
        {
            gameObject.GetComponent<AudioSource>().volume = volume / 10;
        }
    }
}
