using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    [Header("공격 대상 리스트"), SerializeField]
    List<Collider> _targetList;
    public List<Collider> TargetList => _targetList;
    //---------------------------------------------------------------------
    void Awake() { _targetList = new List<Collider>(); }
    void OnTriggerEnter(Collider other) { _targetList.Add(other); }
    void OnTriggerExit(Collider other) { _targetList.Remove(other); }
}
