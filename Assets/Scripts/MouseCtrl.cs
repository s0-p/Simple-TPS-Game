using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCtrl : InputCtrlBase
{
    public void Update()
    {
        if(Input.GetMouseButton(1))
            InputDir = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
    }
}
