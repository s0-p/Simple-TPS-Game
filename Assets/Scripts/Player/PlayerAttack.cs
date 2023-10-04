using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("�Ϲ� ���ݷ�"), SerializeField]
    int _normalDamage = 10;
    [Header("Ư�� ���ݷ�"), SerializeField]
    int _skillDamage = 10;
    [Header("�뽬 ���ݷ�"), SerializeField]
    int _dashDamage = 10;
    //--------------------------------------------------------
    [Header("��Ʈ ��ǿ� Ʈ������"), SerializeField]
    Transform _characterBodyTrsf;
    //--------------------------------------------------------
    [Header("�Ϲ� ���� Ÿ��"), SerializeField]
    TargetManager _normalTarget;
    [Header("Ư�� ���� Ÿ��"), SerializeField]
    TargetManager _skillTarget;
    //---------------------------------------------------------------------
    void DoAttack(List<Collider> colList, int damage, float delay, float knockPow)
    {
        foreach (Collider coll in colList)
        {
            if (coll == null) continue;

            EnemyHealth enemyHealth = coll.GetComponent<EnemyHealth>();
            if (enemyHealth != null && !enemyHealth.IsDead)
                StartCoroutine(enemyHealth.StartDamage(damage, _characterBodyTrsf.position, delay, knockPow));
        }
    }
    public void NormalAttack() { DoAttack(_normalTarget.TargetList, _normalDamage, 0.5f, 100f); }
    public void SkillAttack() { DoAttack(_skillTarget.TargetList, _skillDamage, 1f, 150f);}
    public void DashAttack() { DoAttack(_normalTarget.TargetList, _dashDamage, 1f, 150f); }
}
