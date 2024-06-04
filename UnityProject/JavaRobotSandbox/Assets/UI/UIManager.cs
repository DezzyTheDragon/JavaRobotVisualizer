using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//Manages the UI elements
public class UIManager : MonoBehaviour
{
    public TMP_InputField directory;
    public GameObject detailsPanel;
    public TMP_Text detailsButtonText;

    private RectTransform detailsRectTransform;
    private bool detailsHidden = true;

    // Start is called before the first frame update
    void Start()
    {
        loadDir();
        detailsRectTransform = detailsPanel.GetComponent<RectTransform>();
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

    //Reloads the current scene
    public void reloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
