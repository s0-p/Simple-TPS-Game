using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttck : MonoBehaviour
{
    [Header("공격력"), SerializeField]
    public int _power = 10;

    [Header("공격 시간 간격"), SerializeField]
    float _attackTime = 0.5f;
    float _attackTimer = 0f;
    bool _isPlayerInRange = false;
    //--------------------------------------------------------
    EnemyHealth _enemyHealth;
    //--------------------------------------------------------
    GameObject _player;
    PlayerHealth _playerHealth;
    //---------------------------------------------------------------------
    void Awake()
    {
        _enemyHealth = GetComponent<EnemyHealth>();

        _player = GameObject.FindGameObjectWithTag("Player");
        if (_player != null)
            _playerHealth = _player.GetComponentInParent<PlayerHealth>();
    }
    void Update()
    {
        _attackTimer += Time.deltaTime;
        if (_attackTimer >= _attackTime && _isPlayerInRange && _enemyHealth.CurHealth > 0)
            Attack();
    }
    void OnTriggerEnter(Collider other) { if (other.gameObject == _player) _isPlayerInRange = true; }
    void OnTriggerExit(Collider other) { if (other.gameObject == _player) _isPlayerInRange = false; }
    //---------------------------------------------------------------------
    void Attack()
    {
        if (_playerHealth == null) return;

        _attackTimer = 0f;
        if (_playerHealth.CurHealth > 0)
            _playerHealth.TakeDamage(_power);
    }
}
