using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    public GameObject interactMessage;
    public GameObject IDE;
    public RawImage minimap;
    public RawImage keysHint;
    
    private List<GameObject> messages;

    private bool minimapState = false;

    // Start is called before the first frame update
    void Start()
    {
        messages = new List<GameObject>();
        messages.Add(interactMessage);

        StartCoroutine(showKeys());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showMessage(string message)
    {
        interactMessage.GetComponent<TMP_Text>().text = message;
        interactMessage.SetActive(true);
    }

    public void hideMessages()
    {
        foreach (GameObject message in messages)
        {
            message.SetActive(false);
        }
    }

    public void showIDE(LaptopScript laptop)
    {
        hideMessages();
        IDE.SetActive(true);
        IDE.GetComponent<IDEScript>().setEnv(laptop);
        GameObject.Find("Player").GetComponent<PlayerMovement>().switchMovement();
        GameObject.Find("Player").GetComponent<PlayerScript>().switchInteraction();
    }

    public void hideIDE()
    {
        IDE.SetActive(false);
        GameObject.Find("Player").GetComponent<PlayerMovement>().switchMovement();
        GameObject.Find("Player").GetComponent<PlayerScript>().switchInteraction();
    }

    public void toggleMinimap()
    {
        minimapState = !minimapState;
        if (minimapState)
        {
            enableMinimap();
        }
        else
        {
            disableMinimap();
        }
    }

    public void enableMinimap()
    {
        minimap.gameObject.SetActive(true);
        
        // TODO: set camera culling mask depending on what level the player is at
    }

    public void disableMinimap()
    {
        minimap.gameObject.SetActive(false);
    }

    public IEnumerator showKeys()
    {
        keysHint.gameObject.SetActive(true);
        for (float i = 0; i <= 1; i += 0.01f)
        {
            keysHint.color = new Color(1f, 1f, 1f, i);
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(2f);
        for (float i = 1; i >= 0; i -= 0.01f)
        {
            keysHint.color = new Color(1f, 1f, 1f, i);
            yield return new WaitForSeconds(0.01f);
        }
        keysHint.gameObject.SetActive(false);
    }
}
