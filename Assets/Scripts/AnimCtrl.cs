using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimCtrl : MonoBehaviour
{
    [SerializeField]
    Animator _animator;
    //---------------------------------------------------------------------
    public void Walk(bool isWalk) { _animator.SetBool("isWalk", isWalk); }
}
