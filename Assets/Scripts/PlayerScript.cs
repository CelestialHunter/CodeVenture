using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    GameObject player;
    GameObject playerCamera;

    /* PlayerMovement */
    float movementSpeed = 10.0f;
    bool isGrounded = true;


    /* Camera */
    float cameraVerticalRotation = 0.0f;
    float verticalRotationLimit = 60.0f;
    float cameraSensitivity = 5f;

    /* Interaction */
    public float minInteractDistance = 2.0f;

    /* UI */
    public UIScript ui;

    void Start()
    {
        player = gameObject;
        playerCamera = player.transform.Find("PlayerCamera").gameObject;
    }

    void Update()
    {
        playerMovement();
        playerRotation();

        look();

    }

    void playerMovement()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        player.transform.Translate(movement * movementSpeed * Time.deltaTime);
    }

    void playerRotation()
    {
        // vertical mouse movement rotates camera up and down, between -90 and 90 degrees
        float mouseVertical = Input.GetAxisRaw("Mouse Y");
        float newRotation = cameraVerticalRotation - mouseVertical * cameraSensitivity;
        cameraVerticalRotation = Mathf.Clamp(newRotation, -verticalRotationLimit, verticalRotationLimit);
        playerCamera.transform.localEulerAngles = new Vector3(cameraVerticalRotation, 0, 0);

        // horizontal mouse movement rotates the whole player
        float horizontalRotation = Input.GetAxis("Mouse X");
        player.transform.Rotate(0, horizontalRotation * cameraSensitivity, 0);
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
                        ui.showMessage();
                        break;
                    default:
                        ui.hideMessages();
                        break;
                }
            }
            else
            {
                ui.hideMessages();
            }
        }
    }
}
