using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimCtrl : MonoBehaviour
{
    Animator _animator;
    //---------------------------------------------------------------------
    void Awake() { _animator = GetComponentInChildren<Animator>(); }
    //---------------------------------------------------------------------
    public void Run(float speed) { _animator.SetFloat("Speed", speed); }
}
