using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("초기 체력"), SerializeField]
    int _initHealth = 100;
    int _curHealth;
    public int CurHealth => _curHealth;
    //--------------------------------------------------------
    [Header("체력 슬라이더"), SerializeField]
    Slider _sliderHealth;
    //--------------------------------------------------------
    [Header("사망 오디오 클립"), SerializeField]
    AudioClip _acDeath;
    AudioSource _audioSrc;
    //--------------------------------------------------------
    PlayerAnimCtrl _anim;
    PlayerCtrl _playerCtrl;
    //--------------------------------------------------------
    [Header("데미지 이펙트용 이미지"), SerializeField]
    Image _imgDamage;
    [Header("데미지 이펙트 연출 속도"), SerializeField]
    float _damageEffSpeed = 5f;
    [Header("데미지 이펙트 색상"), SerializeField]
    Color _damageColor = new Color(1f, 0f, 0f, 0.1f);
    //--------------------------------------------------------
    bool _isDead = false;
    bool _isHitted = false;
    //---------------------------------------------------------------------
    void Awake()
    {
        _curHealth = _initHealth;
        _audioSrc = GetComponent<AudioSource>();
        _anim = GetComponent<PlayerAnimCtrl>();
        _playerCtrl = GetComponent<PlayerCtrl>();
        _isDead = false;
        _isHitted = false;
    }
    void Update()
    {
        if (_isDead) return;
        if (_imgDamage == null) return;
        if (_isHitted)
            _imgDamage.color = _damageColor;
        else
            _imgDamage.color = Color.Lerp(_imgDamage.color, Color.clear, _damageEffSpeed * Time.deltaTime);

        _isHitted = false;
    }
    //---------------------------------------------------------------------
    public void TakeDamage(int damage)
    {
        _isHitted = true;
        _curHealth -= damage;
        _sliderHealth.value = _curHealth;
        _playerCtrl.FreezeMove(this);

        if (_curHealth <= 0 && !_isDead)
            OnDead();
        else
            _anim.OnHitted();
    }
    //---------------------------------------------------------------------
    void OnDead()
    {
        _isDead = true;
        _anim.OnDead();

        _audioSrc.clip = _acDeath;
        _audioSrc.Play();

        _playerCtrl.enabled = false;
    }
}
