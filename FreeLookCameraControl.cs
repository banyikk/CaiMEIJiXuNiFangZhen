using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeLookCameraControl : MonoBehaviour
{
    private CinemachineFreeLook cinemachineFreeLook;
    private float xRotation;
    private float yRotation;
    void Start()
    {
        cinemachineFreeLook=GetComponent<CinemachineFreeLook>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            float x = -Input.GetAxis("Mouse X");
            float y = -Input.GetAxis("Mouse Y");
            
            cinemachineFreeLook.m_XAxis.m_InputAxisValue = x;
            cinemachineFreeLook.m_YAxis.m_InputAxisValue = y;
        }
        if (Input.GetMouseButtonUp(0))
        {
            
            cinemachineFreeLook.m_XAxis.m_InputAxisValue = 0;
            cinemachineFreeLook.m_YAxis.m_InputAxisValue = 0;

        }
    }
}
