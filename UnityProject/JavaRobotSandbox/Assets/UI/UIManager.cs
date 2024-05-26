using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TMP_InputField directory;

    // Start is called before the first frame update
    void Start()
    {
        loadDir();
    }

    // Update is called once per frame
    void Update()
    {
        
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
