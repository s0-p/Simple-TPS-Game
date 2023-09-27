using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    [Header("Ÿ�ٰ��� ����"), SerializeField]
    Vector3 _offset = new Vector3(0f, 4f, 7f);
    [Header("���� Ÿ�� Ʈ������"), SerializeField]
    Transform _targetTrsf;
    //---------------------------------------------------------------------
    void LateUpdate() 
    {
        transform.position = _targetTrsf.position
                             + Vector3.up * _offset.y - Vector3.forward * _offset.z;
        transform.LookAt(_targetTrsf);
    }
}
