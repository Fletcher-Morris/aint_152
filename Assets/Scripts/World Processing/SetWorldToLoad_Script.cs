using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class SetWorldToLoad_Script : MonoBehaviour {

    public GameObject worldNameUIObject;

	public void SetWorldToLoad()
    {
        GameObject.Find("WM").GetComponent<WorldLoader_Script>().nameOfWorldToLoad = worldNameUIObject.GetComponent<Text>().text;
        Debug.Log(gameObject.name + ": World to load set to " + worldNameUIObject.GetComponent<Text>().text + ".");
        GameObject.Find("WM").GetComponent<WorldLoader_Script>().LoadSelectedWorld();
    }

    public void DeleteWorld()
    {
        GameObject.Find("WM").GetComponent<WorldLoader_Script>().nameOfWorldToLoad = worldNameUIObject.GetComponent<Text>().text;

        Directory.Delete(Application.dataPath + "/Saves/" + worldNameUIObject.GetComponent<Text>().text, true);

        Debug.Log(gameObject.name + ": Deleted " + worldNameUIObject.GetComponent<Text>().text + " save file.");

        GameObject.Find("Main Menu Canvas").GetComponent<MenuControls_Script>().GetSavedWorlds();

        GameObject.Find("WM").GetComponent<WorldLoader_Script>().nameOfWorldToLoad = null;
    }
}
