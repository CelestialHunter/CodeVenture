using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    GameObject player;
    GameObject playerCamera;
    CharacterController controller;

    /* PlayerMovement */
    public float movementSpeed = 10.0f;
    public float runningSpeed = 15.0f;
    public float currentSpeed;
    public float jumpHeigth = 2.0f;

    public float gravity = -9.81f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded = true;

    Vector3 velocity;

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

        currentSpeed = movementSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0) velocity.y = -2.0f;

        playerMovement();
        playerRotation();        
    }
    void playerMovement()
    {
        if (!movementEnabled) return;

        // sprint/run
        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = Mathf.Clamp(currentSpeed + .1f, movementSpeed, runningSpeed);
        }
        else
        {
            currentSpeed = Mathf.Clamp(currentSpeed - .1f, movementSpeed, runningSpeed);
        }        

        // movement
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = player.transform.right * moveHorizontal + player.transform.forward * moveVertical;
        controller.Move(movement * currentSpeed * Time.deltaTime);
        

        // jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            isGrounded = false;
            velocity.y = Mathf.Sqrt(jumpHeigth * -2.0f * gravity);
        }

        // gravity
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
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
        float mouseHorizontal = Input.GetAxis("Mouse X") * cameraSensitivity * Time.deltaTime;
        player.transform.Rotate(Vector3.up * mouseHorizontal, Space.World);
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
