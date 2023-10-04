using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    [Header("�̵� �ӵ�"), SerializeField]
    float _moveSpeed = 5f;
    [Header("ȸ�� �ӵ�"), SerializeField]
    float _rotSpeed = 10f;
    //--------------------------------------------------------
    [Header("��Ʈ�ѷ�"), SerializeField]
    InputCtrlBase _controller;
    PlayerAnimCtrl _animCtrl;
    //--------------------------------------------------------
    [Header("��Ʈ ��ǿ� Ʈ������"), SerializeField]
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
    public void FreezeMove(PlayerHealth playerHealth)
    {
        StartCoroutine(Crt_FreezeMove(0.7f, playerHealth));
    }
    IEnumerator Crt_FreezeMove(float time, PlayerHealth health)
    {
        if (health.CurHealth <= 0f)
        {
            enabled = false;
            yield break;
        }
        enabled = false;
        yield return new WaitForSeconds(time);

        if (health.CurHealth > 0)
            enabled = true;
    }
}
