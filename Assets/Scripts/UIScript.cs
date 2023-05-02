using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIScript : MonoBehaviour
{
    public GameObject interactMessage;
    public GameObject IDE;
    
    private List<GameObject> messages;

    // Start is called before the first frame update
    void Start()
    {
        messages = new List<GameObject>();
        messages.Add(interactMessage);
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
}
