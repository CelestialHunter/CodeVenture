using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    GameObject player;
    GameObject playerCamera;
    CharacterController controller;

    /* PlayerMovement */
    float movementSpeed = 10.0f;
    bool isGrounded = true;

    /* Camera */
    public float cameraVerticalRotation = 0.0f;
    public float verticalRotationLimit = 60.0f;
    public float cameraSensitivity = 100f;

    bool movementEnabled = true;

    void Start()
    {
        player = gameObject;
        playerCamera = player.transform.Find("PlayerCamera").gameObject;
        controller = player.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        playerMovement();
        playerRotation();
    }

    void playerMovement()
    {
        if (!movementEnabled) return;
        
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = player.transform.right * moveHorizontal + player.transform.forward * moveVertical;
        controller.Move(movement * movementSpeed * Time.deltaTime);
        //player.transform.Translate(movement * movementSpeed * Time.deltaTime);
    }

    void playerRotation()
    {
        if (!movementEnabled) return;
        
        // vertical mouse movement rotates camera up and down, between -90 and 90 degrees
        float mouseVertical = Input.GetAxisRaw("Mouse Y") * cameraSensitivity * Time.deltaTime;
        float newRotation = cameraVerticalRotation - mouseVertical;
        cameraVerticalRotation = Mathf.Clamp(newRotation, -verticalRotationLimit, verticalRotationLimit);
        playerCamera.transform.localEulerAngles = new Vector3(cameraVerticalRotation, 0, 0);

        // horizontal mouse movement rotates the whole player
        float horizontalRotation = Input.GetAxis("Mouse X") * cameraSensitivity * Time.deltaTime;
        //player.transform.Rotate(0, horizontalRotation, 0);
        player.transform.Rotate(Vector3.up * horizontalRotation);

    }

    public void switchMovement()
    {
        movementEnabled = !movementEnabled;
        if (movementEnabled)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
