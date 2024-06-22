using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

//Manages the UI elements
public class UIManager : MonoBehaviour
{
    public TMP_InputField directory;
    public TMP_InputField portInput;
    public TMP_Text detailsButtonText;
    public TMP_Text robotButtonText;
    public GameObject detailsPanel;
    public GameObject robotPanel;

    public IRobotUI robotBehavior;

    private RectTransform detailsRectTransform;
    private RectTransform robotRectTransform;
    private bool detailsHidden = true;
    private bool robotHidden = false;

    // Start is called before the first frame update
    void Start()
    {
        loadDir();
        robotBehavior = GameObject.FindGameObjectsWithTag("Robot")[0].GetComponent<IRobotUI>();
        if( robotBehavior == null)
        {
            Debug.LogWarning("Failed to find any game object taged with \"Robot\" OR does not implement IRobotUI");
        }
        detailsRectTransform = detailsPanel.GetComponent<RectTransform>();
        robotRectTransform = robotPanel.GetComponent<RectTransform>();
    }

    //Shows or Hides the the robot details panel
    public void toggleDetailsPanel()
    {
        detailsHidden = !detailsHidden;
        if (detailsHidden)
        {
            detailsRectTransform.anchoredPosition = new Vector3(200, 0, 0);
            detailsButtonText.text = "<";
        }
        else
        {
            detailsRectTransform.anchoredPosition = Vector3.zero;
            detailsButtonText.text = ">";
        }
    }

    public void toggleRobotPanel()
    {
        robotHidden = !robotHidden;
        if(robotHidden)
        {
            robotRectTransform.anchoredPosition = new Vector3(-170, 0, 0);
            robotButtonText.text = ">";
        }
        else
        {
            robotRectTransform.anchoredPosition = new Vector3(0, 0, 0);
            robotButtonText.text = "<";
        }
    }

    //Reloads the current scene
    public void reloadScene()
    {
        robotBehavior.closeRobotTerminal();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void startRobot()
    {
        robotBehavior.setRobotServerPort(Int32.Parse(portInput.text));
        portInput.interactable = false;
        robotBehavior.startRobotServer();
    }

    //Saves the given directory of the robot server jar
    public void updateSavedDir()
    {
        PlayerPrefs.SetString("dir", directory.text);
        PlayerPrefs.Save();
    }

    //Load the given directory of the robot server jar
    public void loadDir()
    {
        string dir = PlayerPrefs.GetString("dir");
        directory.text = dir;
    }
}
