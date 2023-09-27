using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharCtrl : MonoBehaviour
{
    public enum eROTATE
    {
        CAMERA,
        MOVE
    }
    public eROTATE _eRotate = eROTATE.CAMERA;
    //--------------------------------------------------------
    [Header("이동 속도"), SerializeField]
    float _speed = 5f;
    //--------------------------------------------------------
    [Header("컨트롤러"), SerializeField]
    InputCtrlBase _controller;
    [Header("카메라 컨트롤러"), SerializeField]
    CameraCtrl _camCtrl;
    [Header("애니메이션 컨트롤러"), SerializeField]
    AnimCtrl _animCtrl;
    //--------------------------------------------------------
    [Header("모델링 데이터 트랜스폼"), SerializeField]
    Transform _characterBodyTrsf;
    //---------------------------------------------------------------------
    void Update() { Move(); }
    //---------------------------------------------------------------------
    void Move()
    {
        Debug.DrawRay(
                _camCtrl.transform.position,
                new Vector3(_camCtrl.transform.forward.x, 0f, _camCtrl.transform.forward.z),
                Color.red
            );
        
        if (_controller == null) return;

        Vector2 moveInput = new Vector2(_controller.InputDir.x, _controller.InputDir.y);
        bool isWalk = moveInput != Vector2.zero;

        if (_animCtrl != null)
            _animCtrl.Walk(isWalk);

        if (isWalk)
        {
            Vector3 lookForward = new Vector3(_camCtrl.transform.forward.x, 0f, _camCtrl.transform.forward.z);
            Vector3 lookRight = new Vector3(_camCtrl.transform.right.x, 0f, _camCtrl.transform.right.z);

            Vector3 moveDir = lookForward * moveInput.y + lookRight * moveInput.x;

            switch (_eRotate)
            {
                case eROTATE.CAMERA:_characterBodyTrsf.forward = lookForward; break;
                case eROTATE.MOVE:_characterBodyTrsf.forward = moveDir; break;
            }

            transform.position += Time.deltaTime * _speed * moveDir.normalized;
        }
    }
}
