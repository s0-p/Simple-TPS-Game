using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAnimCtrl : MonoBehaviour
{
    Animator _animator;
    //--------------------------------------------------------
    float _lastAttackTime, _lastSkillTime, _lastDashTime;
    bool _isAttacking = false;
    //--------------------------------------------------------
    [Header("일반 공격 애니 이벤트"), SerializeField]
    UnityEvent _OnNormalAttack;
    [Header("[ 스킬 공격 애니 이벤트 ]"), SerializeField]
    UnityEvent _OnSkillAttack;
    [Header("[ 대쉬 공격 애니 이벤트 ]"), SerializeField]
    UnityEvent _OnDashAttack;
    //---------------------------------------------------------------------
    void Awake() 
    {
        _animator = GetComponentInChildren<Animator>();
        _animator.applyRootMotion = true;
    }
    //---------------------------------------------------------------------
    public void Run(float speed) { _animator.SetFloat("Speed", speed); }
    public void OnDead() { _animator.SetTrigger("Die"); }
    public void OnHitted() { _animator.SetTrigger("Damage"); }
    public void OnAttackStart()
    {
        _isAttacking = true;
        SetCombo(_isAttacking);
        StartCoroutine(StartAttack());
    }
    public void OnAttackFinish()
    {
        _isAttacking = false;
        SetCombo(_isAttacking);
    }
    public void OnSkillStart()
    {
        if (Time.time - _lastSkillTime > 1f)
        {
            SetSkill(true);
            _lastSkillTime = Time.time;
            if (_OnSkillAttack != null)
                _OnSkillAttack.Invoke();
        }
    }
    public void OnSkillFinish() { SetSkill(false); }
    public void OnDashStart()
    {
        if (Time.time - _lastDashTime > 1f)
        {
            DoDash();
            _lastDashTime = Time.time;
            if (_OnDashAttack != null)
                _OnDashAttack.Invoke();
        }
    }
    public void OnDashFinish() { }
    //---------------------------------------------------------------------
    IEnumerator StartAttack()
    {
        if (Time.time - _lastAttackTime > 1f)
        {
            _lastAttackTime = Time.time;
            while (_isAttacking)
            {
                DoAttackStart();
                if (_OnNormalAttack != null)
                    _OnNormalAttack.Invoke();
                yield return new WaitForSeconds(1f);
            }
        }
    }
    void DoAttackStart() { _animator.SetTrigger("AttackStart"); }
    void SetCombo(bool isCombo) { _animator.SetBool("Combo", isCombo); }
    void SetSkill(bool isSkill) { _animator.SetBool("Skill", isSkill); }
    void DoDash() { _animator.SetTrigger("Dash"); }
}
