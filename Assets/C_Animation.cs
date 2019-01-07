using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

[RequireComponent(typeof(C_Input))]
[RequireComponent(typeof(Animator))]

public class C_Animation : MonoBehaviour
{
    public enum AnimationState { Idle, Moving, Dodging }

    public string leftStickHParam;
    public string leftStickVParam;
    public string leftStickMagParam;
    public string leftStickAngleParam;
    public string lTargetingParam;
    public string dodgeParam;
    public float freeMovementDamping = .05f;

    public AnimationState animationState;

    Animator _animator;
    AnimatorStateInfo _aState;
    AnimatorClipInfo[] _aClip;
    string _aClipName;
    C_Input _cI;


    private void Start()
    {
        _animator = GetComponent<Animator>();
        _cI = GetComponent<C_Input>();
    }

    void Update()
    {
        ApplyInput();
    }

    void GetAnimationInfo()
    {
        _aState = _animator.GetCurrentAnimatorStateInfo(0);
        _aClip = _animator.GetCurrentAnimatorClipInfo(0);
        _aClipName = _aClip[0].clip.name;
    }

    void ApplyInput()
    {
        _animator.SetFloat(leftStickHParam, _cI.leftStickHorizontal);
        _animator.SetFloat(leftStickVParam, _cI.leftStickVertical);
        _animator.SetFloat(leftStickMagParam, _cI.leftStickMagnitude, freeMovementDamping, Time.deltaTime);
        _animator.SetFloat(leftStickAngleParam, _cI.leftStickAngle);
        _animator.SetBool(lTargetingParam, _cI.lTargeting);

        if (_cI.dodge)
            _animator.SetTrigger(dodgeParam);
    }
}
