using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCameraScript : MonoBehaviour
{
    public Transform player;

    private Transform minimapCamera;

    private Camera cameraComponent;
    private float cameraDistance = 10f;


    public float[] veritcalPadding = { 0f, 105f };
    public float horizontalPadding = 90f;

    void Start()
    {
        minimapCamera = this.transform;
        cameraComponent = this.gameObject.GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        Vector3 newPosition = player.position;
        newPosition.y += cameraDistance;
        newPosition.x = Mathf.Clamp(newPosition.x, -horizontalPadding, horizontalPadding);
        newPosition.z = Mathf.Clamp(newPosition.z, veritcalPadding[0], veritcalPadding[1]);
        minimapCamera.position = newPosition;

        if(player.position.y < 6f)
        {
            // set culling mask to "Ground"
            cameraComponent.cullingMask = LayerMask.GetMask(new string[] { "Ground", "GroundMinimap", "Minimap_symbols" });
        }
        else if(player.position.y < 12f)
        {
            // set culling mask to "1stFloor"
            cameraComponent.cullingMask = LayerMask.GetMask(new string[] { "1stFloor", "1stFloorMinimap", "Minimap_symbols" });
        }
    }
}
