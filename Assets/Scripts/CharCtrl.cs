using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharCtrl : MonoBehaviour
{
    [Header("이동 속도"), SerializeField]
    float _moveSpeed = 5f;
    [Header("회전 속도"), SerializeField]
    float _rotSpeed = 10f;
    //--------------------------------------------------------
    [Header("컨트롤러"), SerializeField]
    InputCtrlBase _controller;
    PlayerAnimCtrl _animCtrl;
    //--------------------------------------------------------
    [Header("루트 모션용 트랜스폼"), SerializeField]
    Transform _characterBodyTrsf;
    //---------------------------------------------------------------------
    void Awake() { _animCtrl = GetComponent<PlayerAnimCtrl>(); }
    void Update() { Move(); }
    //---------------------------------------------------------------------
    void Move()
    {
        if (_controller == null) return;

        Vector2 moveInput = new Vector2(_controller.InputDir.x, _controller.InputDir.y);
        float speed = moveInput.magnitude * _moveSpeed;

        if (_animCtrl != null)
            _animCtrl.Run(speed);

        if (speed > 0f)
        {
            Vector3 moveDir = new Vector3(moveInput.x, 0f, moveInput.y).normalized;

            _characterBodyTrsf.SetPositionAndRotation(
                _characterBodyTrsf.position + Time.deltaTime * _moveSpeed * moveDir,
                Quaternion.Lerp(_characterBodyTrsf.rotation, Quaternion.LookRotation(moveDir), Time.deltaTime * _rotSpeed)
                );
        }
    }
}
