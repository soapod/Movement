using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

[RequireComponent(typeof(C_Input))]
[RequireComponent(typeof(Animator))]

public class C_Animation : MonoBehaviour
{
    public string leftStickVParam;
    public string leftStickHParam;
    public string leftStickMagParam;
    public string lTargetingParam;
    public float movementDampening = .05f;

    Animator _animator;
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

    void ApplyInput()
    {
        _animator.SetFloat(leftStickHParam, _cI.leftStickHorizontal);
        _animator.SetFloat(leftStickVParam, _cI.leftStickVertical);
        _animator.SetFloat(leftStickMagParam, _cI.leftStickMagnitude, movementDampening, Time.deltaTime);
        _animator.SetBool(lTargetingParam, _cI.lTargeting);
    }
}
