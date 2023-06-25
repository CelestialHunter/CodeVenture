using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableCamera : MonoBehaviour
{
    public Camera playerCamera;
    public Camera variableCamera;
    public Transform portalPlane;
    public Transform variablePlane;

    public RenderTexture viewTexture;

    private void Awake()
    {
        variableCamera = GetComponent<Camera>();
        playerCamera = GameObject.Find("PlayerCamera").GetComponent<Camera>();
        variablePlane = GameObject.Find("VariablePeerObj").transform;
    }
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerOffsetFromPortal = playerCamera.transform.position - variablePlane.position;
        variableCamera.transform.position = portalPlane.position + playerOffsetFromPortal;

        float angularDifferenceBetweenPortalRotations = Quaternion.Angle(portalPlane.rotation, variablePlane.rotation);

        Quaternion portalRotationalDifference = Quaternion.AngleAxis(angularDifferenceBetweenPortalRotations, Vector3.up);
        Vector3 newCameraDirection = portalRotationalDifference * playerCamera.transform.forward;
        variableCamera.transform.rotation = Quaternion.LookRotation(newCameraDirection, Vector3.up);

        //variableCamera.transform.rotation = playerCamera.transform.rotation;
    }

    void CreateViewTexture()
    {
        if (viewTexture == null || viewTexture.width != Screen.width || viewTexture.height != Screen.height)
        {
            if (viewTexture != null)
            {
                viewTexture.Release();
            }
            viewTexture = new RenderTexture(Screen.width, Screen.height, 0);
            variableCamera.targetTexture = viewTexture;          
        }
    }

    public void Render()
    {
        portalPlane.gameObject.SetActive(false);

        var m = transform.localToWorldMatrix * variablePlane.worldToLocalMatrix * playerCamera.transform.localToWorldMatrix;
        variableCamera.transform.SetPositionAndRotation(m.GetColumn(3), m.rotation);

        variableCamera.Render();

        portalPlane.gameObject.SetActive(true);
    }
}
