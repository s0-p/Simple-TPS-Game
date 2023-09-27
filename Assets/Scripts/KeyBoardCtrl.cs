using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBoardCtrl : InputCtrlBase
{
    public void Update()
    {
        InputDir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }
}
