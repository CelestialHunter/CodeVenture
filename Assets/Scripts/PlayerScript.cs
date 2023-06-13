using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    GameObject playerCamera;

    /* Interaction */
    public float minInteractDistance = 3.0f;
    Interactable currentGazeTarget;

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
        minimap();
        pauseGame();
    }


    void look()
    {
        RaycastHit target;

        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out target, 100) && target.distance <= minInteractDistance)
        {
            Interactable targetObj = target.collider.gameObject.GetComponent<Interactable>();
            if (targetObj != null && targetObj != currentGazeTarget)
            {
                if (currentGazeTarget != null) currentGazeTarget.unhighlight();
                currentGazeTarget = targetObj;
                currentGazeTarget.highlight();
                ui.showMessage(targetObj.interactMessage);
            }
            else
            {
                //currentGazeTarget.unhighlight();
                //currentGazeTarget = null;
                //ui.hideMessages();
             }
        }
        else
        {
            if (currentGazeTarget != null) currentGazeTarget.unhighlight();
            currentGazeTarget = null;
            ui.hideMessages();
        }
    }

    void interact()
    {
        // if the player hits the interact key while looking at an interactable object, launch the action

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (currentGazeTarget != null)
            {
                currentGazeTarget.interact();
            }
        }
    }

    public void switchInteraction()
    {
        interactionEnabled = !interactionEnabled;
    }

    void minimap()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            ui.toggleMinimap();
        }
    }

    void pauseGame()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            ui.enablePause();
        }
    }
}
