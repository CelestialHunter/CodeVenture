using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public GameObject overwriteDialog;
    bool saveFileExists = false;
    
    void Start()
    {
        // check if objectives.xml exists
        // if so, Load Game button can be interacted with

        // if not, Load Game button cannot be interacted with

        System.IO.FileStream file = null;
        try
        {
            file = System.IO.File.Open(Application.persistentDataPath + "/objectives.xml", System.IO.FileMode.Open);
            GameObject.Find("loadgameBT").GetComponent<UnityEngine.UI.Button>().interactable = true;
            saveFileExists = true;
            file.Close();
        }
        catch (System.IO.FileNotFoundException)
        {
            Debug.Log("No save file found.");
            GameObject.Find("loadgameBT").GetComponent<UnityEngine.UI.Button>().interactable = false;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void newGame()
    {
        if(saveFileExists)
        {
            // dialog box to ask if player wants to overwrite save file
            // if yes, delete save file and start new game
            // if no, do nothing
            toggleDialog();
            

            // if player chooses not to overwrite save file, do nothing
            return;
        }

        StartCoroutine(loadFirstScene());
    }

    public void loadGame()
    {
        // load game state from objectives.xml
        GameObject _loadGame = new GameObject("_loadGame");

        _loadGame.transform.position = new Vector3(0, 0, 0);

        DontDestroyOnLoad(_loadGame);

        StartCoroutine(loadFirstScene());
    }

    public void quitGame()
    {
        Application.Quit();
    }

    IEnumerator loadFirstScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(1);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            Debug.Log(asyncLoad.progress);
            yield return null;
        }
    }

    public void overwriteSavegame()
    {
        System.IO.File.Delete(Application.persistentDataPath + "/objectives.xml");
        StartCoroutine(loadFirstScene());
    }
    public void toggleDialog()
    {
        overwriteDialog.SetActive(!overwriteDialog.activeSelf);
    }
}
