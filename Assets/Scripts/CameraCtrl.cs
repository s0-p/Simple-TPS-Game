using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour
{
    [Header("컨트롤러"), SerializeField]
    InputCtrlBase _controller;
    //---------------------------------------------------------------------
    void Update() { LookAround(); }
    //---------------------------------------------------------------------
    public void LookAround()
    {
        if (_controller == null) return;

        Vector3 camAngle = transform.eulerAngles;
        Vector2 mouseDelta = new Vector2(_controller.InputDir.x, _controller.InputDir.y);

        float angleX = camAngle.x - mouseDelta.y;
        if (angleX < 180)
            angleX = Mathf.Clamp(angleX, -1f, 70f);
        else
            angleX = Mathf.Clamp(angleX, 335f, 361f);

        transform.rotation = Quaternion.Euler(angleX, camAngle.y + mouseDelta.x, camAngle.z);
    }
}
