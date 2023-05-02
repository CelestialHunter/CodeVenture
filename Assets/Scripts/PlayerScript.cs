using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    GameObject playerCamera;

    /* Interaction */
    public float minInteractDistance = 3.0f;
    GameObject currentGazeTarget;

    bool interactionEnabled = true;

    /* UI */
    public UIScript ui;

    void Start()
    {
        playerCamera = transform.Find("PlayerCamera").gameObject;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (!interactionEnabled) return;
        look();
        interact();
    }


    void look()
    {
        RaycastHit target;

        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out target, 100))
        {
            if (target.distance <= minInteractDistance)
            {
                switch (target.transform.tag)
                {
                    case "Computer":
                        ui.showMessage(target.transform.GetComponent<LaptopScript>().interactMessage);
                        currentGazeTarget = target.transform.gameObject;
                        break;
                    default:
                        ui.hideMessages();
                        currentGazeTarget = null;
                        break;
                }
            }
            else
            {
                ui.hideMessages();
                currentGazeTarget = null;
            }
        }
    }

    void interact()
    {
        // if the player hits the interact key while looking at an interactable object, launch the action

        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log(currentGazeTarget.name);
            if (currentGazeTarget != null)
            {
                switch (currentGazeTarget.tag)
                {
                    case "Computer":
                        LaptopScript targetLaptop = currentGazeTarget.GetComponent<LaptopScript>();
                        ui.showIDE(targetLaptop); // todo with InteractableObject interface
                        break;
                    default:
                        break;
                }
            }
        }
    }

    public void switchInteraction()
    {
        interactionEnabled = !interactionEnabled;
    }
}
