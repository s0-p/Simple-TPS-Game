using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("체력"), SerializeField]
    int _initHealth = 100;
    int _curHealth;
    public int CurHealth => _curHealth;
    //--------------------------------------------------------
    [Header("피격 이펙트 연출 속도"), SerializeField]
    float _flashSpeed = 5f;
    [Header("피격 이펙트 색상"), SerializeField]
    Color _flashColor = new Color(1f, 0f, 0f, 0.1f);
    Material _mat;
    //--------------------------------------------------------
    AudioSource _as;
    [Header("사망 오디오 클립"), SerializeField]
    AudioClip _acDead;
    //--------------------------------------------------------
    EnemyMove _enemyMove;
    //--------------------------------------------------------
    bool _isDamaged;
    bool _isDead;
    public bool IsDead => _isDead;
    //---------------------------------------------------------------------
    void Awake()
    {
        _curHealth = _initHealth;
        _mat = GetComponentInChildren<Renderer>().material;
        _as = GetComponent<AudioSource>();
        _enemyMove = GetComponent<EnemyMove>();
    }
    void Update()
    {
        if (_isDamaged)
            _mat.SetColor("_OutlineColor", _flashColor);
        else
        {
            _mat.SetColor(
                    "_OutlineColor",
                    Color.Lerp(_mat.GetColor("_OutlineColor"), Color.black, _flashSpeed * Time.deltaTime)
                );
        }
    }
    public IEnumerator StartDamage(int damage, Vector3 playerPos, float delay, float pushBack)
    {
        yield return new WaitForSeconds(delay);

        if (_isDead) yield break;

        TakeDamage(damage);

        Vector3 dirToBack = (transform.position - playerPos).normalized;
        _enemyMove.KnockBack(dirToBack, pushBack);
    }
    //---------------------------------------------------------------------
    void TakeDamage(int damage)
    {
        _isDamaged = true;
        _curHealth -= damage;

		Debug.Log(_curHealth);
        if (_curHealth <= 0 && !_isDead)
            Death();
    }
    void Death()
    {
        _isDead = true;
        GetComponentInChildren<BoxCollider>().isTrigger = true;

        _as.clip = _acDead;
        _as.Play();

        StopAllCoroutines();

        StartSinking();
    }
    public void StartSinking()
    {
        _enemyMove.StartSink();

        Destroy(gameObject, 2f);
    }
}
