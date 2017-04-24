﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommandConsole_Script : MonoBehaviour {

    public bool consoleEnabled = false;
    public string enteredCommand;

    public GameObject placeholderObject;
    public GameObject textObject;

    public int motherlodeValue = 50000;
    public int goldRushValue = 1000000;

    public List<string> previousCommands;
    public int previousCommandsIndex = 0;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Slash))
        {
            consoleEnabled = !consoleEnabled;

            previousCommandsIndex = previousCommands.Count;
            gameObject.GetComponent<InputField>().text = "";
        }

        if (consoleEnabled)
        {
            gameObject.GetComponent<Image>().enabled = true;
            gameObject.GetComponent<InputField>().enabled = true;
            placeholderObject.SetActive(true);
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            textObject.SetActive(true);
            textObject.GetComponent<Text>().text = "";
        }
        else
        {
            gameObject.GetComponent<Image>().enabled = false;
            gameObject.GetComponent<InputField>().enabled = false;
            textObject.GetComponent<Text>().text = "";
            placeholderObject.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if(previousCommandsIndex >= 1)
            {
                previousCommandsIndex--;
                gameObject.GetComponent<InputField>().text = previousCommands[previousCommandsIndex];
            }
            else
            {
                gameObject.GetComponent<InputField>().text = "";
            }
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (previousCommandsIndex < previousCommands.Count - 1)
            {
                previousCommandsIndex++;
                gameObject.GetComponent<InputField>().text = previousCommands[previousCommandsIndex];
            }
            else
            {
                gameObject.GetComponent<InputField>().text = "";
            }
        }
    }

    public void GetCommand()
    {
        if (consoleEnabled)
        {
            enteredCommand = this.GetComponent<InputField>().text;

            previousCommands.Add(enteredCommand);

            ProccessCommand(); 
        }
    }

    public void ProccessCommand()
    {
        if(enteredCommand == "motherlode" || enteredCommand == "Motherlode" || enteredCommand == "motherload" || enteredCommand == "Motherload")
        {
            MotherlodeCheat();
        }
        else if (enteredCommand == "goldrush" || enteredCommand == "Goldrush")
        {
            GoldRushCheat();
        }
    }

    public void MotherlodeCheat()
    {
        GameObject.Find("WM").GetComponent<WorldLoader_Script>().theWorld.money += motherlodeValue;
    }
    public void GoldRushCheat()
    {
        GameObject.Find("WM").GetComponent<WorldLoader_Script>().theWorld.gold += goldRushValue;
    }
}
