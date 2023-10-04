using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    [Header("»ç¸Á ½Ã °¡¶ó¾É´Â ¼Óµµ"), SerializeField]
    float _sinkSpeed = 1f;
    bool _isSinking = false;
    //--------------------------------------------------------
    Rigidbody _rigidbody;
    //--------------------------------------------------------
    NavMeshAgent _navMash;
    //--------------------------------------------------------
    Transform _playerTrsf;
    //---------------------------------------------------------------------
    void Awake()
    {
        _isSinking = false;
        _rigidbody = GetComponent<Rigidbody>();
        _navMash = GetComponent<NavMeshAgent>();
        _playerTrsf = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update()
    {
        if (_navMash.enabled)
            _navMash.SetDestination(_playerTrsf.position);
        if (_isSinking)
            transform.Translate(Time.deltaTime * _sinkSpeed * Vector3.down);
    }
    //---------------------------------------------------------------------
    public void KnockBack(Vector3 dir, float power) { _rigidbody.AddForce(dir * power); }
    public void StartSink()
    {
        _isSinking = true;
        _navMash.enabled = false;
        _rigidbody.isKinematic = true;
    }
}
