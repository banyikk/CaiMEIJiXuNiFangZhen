using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraMove : MonoBehaviour
{


    public float originFov = 60f;
    private bool invertCamera = false;
    private float mouseSensitivity = 2f;
    private float xMaxLookAngle=50f;
    private float yMaxLookAngle = 50f;
    private Camera mainCamera;
    private float pitch;
    private float initialYaw;
    private float yaw ;

    private KeyCode zoomKey = KeyCode.Mouse0;
    public float zoomFOV = 30f;
    private float zoomStepTime = 5f;
    //Ëõ·Åfov
    private bool isZoomed = false;
    private void Awake()
    {
        mainCamera = GetComponent<Camera>();
        mainCamera.fieldOfView = originFov;
       
    }
    void Start()
    {
        pitch=mainCamera.transform.localEulerAngles.x;
        yaw=mainCamera.transform.localEulerAngles.y;
        initialYaw=yaw;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(zoomKey)&&!IsPointerOverUI())
        {  
          isZoomed = true;   
        }
        else if (Input.GetKeyUp(zoomKey))
        {
            
            isZoomed=false;
        }
        
        if (isZoomed)
        {
            
            if (!invertCamera)
            {
                pitch -= mouseSensitivity * Input.GetAxis("Mouse Y");
                yaw+= mouseSensitivity * Input.GetAxis("Mouse X");
            }
            else
            {
                pitch += mouseSensitivity * Input.GetAxis("Mouse Y");
                yaw -= mouseSensitivity * Input.GetAxis("Mouse X");

            }

            
            pitch = Mathf.Clamp(pitch, -yMaxLookAngle, yMaxLookAngle);
            //
            float yawOffset = Mathf.DeltaAngle(initialYaw, yaw);
            yawOffset = Mathf.Clamp(yawOffset, -xMaxLookAngle, xMaxLookAngle);

            yaw = initialYaw + yawOffset;
            
            
            mainCamera.transform.localEulerAngles = new Vector3(pitch, yaw, 0);
            mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, zoomFOV, zoomStepTime * Time.deltaTime);
        }
        else
        {
            
            mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, originFov, zoomStepTime * Time.deltaTime);
        }

    }

    private bool IsPointerOverUI()
    {
        if (EventSystem.current==null)
        {
            return false;
        }
        if (Input.touchCount>0)
        {
            return EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId);
        }
        return EventSystem.current.IsPointerOverGameObject();
    }
}
