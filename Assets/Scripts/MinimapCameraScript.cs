using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCameraScript : MonoBehaviour
{
    public Transform player;

    private Transform minimapCamera;

    public float[] veritcalPadding = { 0f, 105f };
    public float horizontalPadding = 90f;

    void Start()
    {
        minimapCamera = this.transform;
    }

    private void LateUpdate()
    {
        Vector3 newPosition = player.position;
        newPosition.y = minimapCamera.position.y;
        newPosition.x = Mathf.Clamp(newPosition.x, -horizontalPadding, horizontalPadding);
        newPosition.z = Mathf.Clamp(newPosition.z, veritcalPadding[0], veritcalPadding[1]);
        minimapCamera.position = newPosition;
    }
}
