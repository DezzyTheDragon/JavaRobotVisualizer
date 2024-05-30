using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    // Update is called once per frame
    void Update()
    {
        
    }

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

    public void reloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void updateSavedDir()
    {
        PlayerPrefs.SetString("dir", directory.text);
        PlayerPrefs.Save();
    }

    public void loadDir()
    {
        string dir = PlayerPrefs.GetString("dir");
        directory.text = dir;
    }
}
