using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

[RequireComponent(typeof(C_Input))]
[RequireComponent(typeof(Animator))]

public class C_Animation : MonoBehaviour
{
    public enum AnimationState { Idle, Moving, Dashing, Rolling, Attacking }

    public AnimationState animationState;
    public string leftStickHParam;
    public string leftStickVParam;
    public string leftStickMagParam;
    public string leftStickAngleParam;
    public string lTargetingParam;
    public string dodgeParam;
    public string attackParam;
    public float animationDamping = .05f;

    public string idleAnimationTag;
    public string movementAnimationTag;
    public string dodgeAnimationTag;
    public string attackAnimationTag;

    public SkinnedMeshRenderer bodyRenderer;
    public float frontRayBuffer = .1f;
    public Vector3 frontRayHit;

    C_Input _cI;
    Animator _animator;
    AnimatorStateInfo _aState;
    AnimatorTransitionInfo _aTrans;
    AnimatorClipInfo[] _aClip;
    string _aClipName;


    private void Start()
    {
        _animator = GetComponent<Animator>();
        _cI = GetComponent<C_Input>();
    }


    void Update()
    {
        GetAnimationInfo();
        UpdateAnimationState();
        ApplyInput();
    }


    private void FixedUpdate()
    {
        BoundsRaycast();
    }


    void GetAnimationInfo()
    {
        _aState = _animator.GetCurrentAnimatorStateInfo(0);
        _aTrans = _animator.GetAnimatorTransitionInfo(0);
        _aClip = _animator.GetCurrentAnimatorClipInfo(0);
        _aClipName = _aClip[0].clip.name;
    }


    void UpdateAnimationState()
    {
        //if (_aTrans.IsUserName("To Idle"))
        //    animationState = AnimationState.Idle;

        if (_aState.IsTag(idleAnimationTag))
            animationState = AnimationState.Idle;


        //if (_aTrans.IsUserName("To Movement"))
        //    animationState = AnimationState.Moving;

        if (_aState.IsTag(movementAnimationTag))
            animationState = AnimationState.Moving;


        if (_aTrans.IsUserName("To Rolling"))
            animationState = AnimationState.Rolling;

        //if (_aState.IsTag(dodgeAnimationTag))
        //    animationState = AnimationState.Rolling;


        if (_aTrans.IsUserName("To Attack"))
            animationState = AnimationState.Attacking;

        //if (_aState.IsTag(lightAttackAnimationTag))
        //    animationState = AnimationState.Attacking;
    }


    void ApplyInput()
    {
        // movement
        _animator.SetFloat(leftStickHParam, _cI.leftStickHorizontal);
        _animator.SetFloat(leftStickVParam, _cI.leftStickVertical);
        _animator.SetFloat(leftStickMagParam, _cI.leftStickMagnitude, animationDamping, Time.deltaTime);
        _animator.SetFloat(leftStickAngleParam, _cI.leftStickAngle);

        // stick direction
        if(_cI.stickDirection == C_Input.StickDirection.Up)
            _animator.SetBool("Up", true);
        else _animator.SetBool("Up", false);

        if (_cI.stickDirection == C_Input.StickDirection.UpRight)
            _animator.SetBool("Up Right", true);
        else _animator.SetBool("Up Right", false);

        if (_cI.stickDirection == C_Input.StickDirection.Right)
            _animator.SetBool("Right", true);
        else _animator.SetBool("Right", false);

        if (_cI.stickDirection == C_Input.StickDirection.DownRight)
            _animator.SetBool("Down Right", true);
        else _animator.SetBool("Down Right", false);

        if (_cI.stickDirection == C_Input.StickDirection.Down)
            _animator.SetBool("Down", true);
        else _animator.SetBool("Down", false);

        if (_cI.stickDirection == C_Input.StickDirection.DownLeft)
            _animator.SetBool("Down Left", true);
        else _animator.SetBool("Down Left", false);

        if (_cI.stickDirection == C_Input.StickDirection.Left)
            _animator.SetBool("Left", true);
        else _animator.SetBool("Left", false);

        if (_cI.stickDirection == C_Input.StickDirection.UpLeft)
            _animator.SetBool("Up Left", true);
        else _animator.SetBool("Up Left", false);

        if (_cI.stickDirection == C_Input.StickDirection.Neutral)
            _animator.SetBool("Neutral", true);
        else _animator.SetBool("Neutral", false);

        // action inputs
        _animator.SetBool(lTargetingParam, _cI.lTargeting);

        if (_cI.dodge)
            _animator.SetTrigger(dodgeParam);

        if (_cI.attack)
            _animator.SetTrigger(attackParam);
    }


    Vector3 BoundsRaycast()
    {
        Vector3 center = bodyRenderer.bounds.center;
        float x = bodyRenderer.bounds.extents.x;
        float y = bodyRenderer.bounds.extents.y;
        float z = bodyRenderer.bounds.extents.z;
        float radius = 0;
        if (x > z) radius = x;
        else radius = z;

        RaycastHit hit;

        Debug.DrawRay(center, Vector3.forward * (radius + frontRayBuffer), Color.magenta);
        if (Physics.Raycast(center, Vector3.forward, out hit, radius + frontRayBuffer))
        {
            return hit.point;
        }
        else return Vector3.zero;
    }
}
