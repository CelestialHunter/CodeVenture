using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void newGame()
    {
        StartCoroutine(loadFirstScene());
    }

    public void loadGame()
    {
        // load game state from xml/json file
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
}
