using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DoorScript : MonoBehaviour, Interactable
{

    public string roomName = "";
    public bool isOpen = false;
    public bool isLocked = false;
    public bool _isInteractable = true;

    public Transform door;
    public TMP_Text roomNumberText;

    public string interactMessage => (isInteractable) ? (isLocked ? "Locked" : ("Press E to " + (isOpen ? "Close" : "Open"))) : "";

    public bool isInteractable => _isInteractable;

    public void highlight()
    {
        return;
    }

    public void interact()
    {
        if (!isInteractable || isLocked) return;
        if (isOpen)
        {
            close();
        }
        else
        {
            open();
        }
    }

    public void unhighlight()
    {
        return;
    }

    public void open()
    {
        isOpen = true;

        // animate door - y rotation 0->90
        StartCoroutine(openDoor());
    }

    public void close()
    {
        isOpen = false;

        // animate door - y rotation 90->0
        StartCoroutine(closeDoor());
    }

    private IEnumerator openDoor()
    {
        Quaternion startRotation = door.rotation;
        Quaternion endRotation = door.rotation * Quaternion.Euler(0, 90, 0);

        _isInteractable = false;

        float time = 0;
        while (time < 1)
        {
            time += Time.deltaTime;
            door.rotation = Quaternion.Lerp(startRotation, endRotation, time);
            yield return null;
        }

        _isInteractable = true;
    }

    private IEnumerator closeDoor()
    {
        Quaternion startRotation = door.rotation;
        Quaternion endRotation = door.rotation * Quaternion.Euler(0, -90, 0);

        _isInteractable = false;

        float time = 0;
        while (time < 1)
        {
            time += Time.deltaTime;
            door.rotation = Quaternion.Lerp(startRotation, endRotation, time);
            yield return null;
        }

        _isInteractable = true;
    }



    // Start is called before the first frame update
    void Start()
    {
        roomNumberText.text = roomName;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
