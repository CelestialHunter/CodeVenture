using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScript : MonoBehaviour
{

    public GameObject interactMessage;
    
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

    public void showMessage()
    {
        interactMessage.SetActive(true);
    }

    public void hideMessages()
    {
        foreach (GameObject message in messages)
        {
            message.SetActive(false);
        }
    }
}
