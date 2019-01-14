using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(C_Input))]
[RequireComponent(typeof(C_Animation))]

public class C_LockOn : MonoBehaviour
{
    public bool lockedOn;

    [Range(0f,1f)]
    public float chestIKWeight = .5f;
    [Range(0f, 1f)]
    public float headIKWeight = 1f;
    public GameObject lockOnTarget;
    public Transform _lookAtThis;
    public Transform _drawOnThis;

    public float rotationSpeed;

    public C_Animation.AnimationState[] ikDisabledStates;

    Animator _anim;
    C_Input _cI;
    C_Animation _cA;


    private void Start()
    {
        _anim = GetComponent<Animator>();
        _cI = GetComponent<C_Input>();
        _cA = GetComponent<C_Animation>();

        _lookAtThis = lockOnTarget.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Head);
        _drawOnThis = lockOnTarget.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Chest);
    }


    private void Update()
    {
        if (lockedOn)
        {
            Quaternion _lockOnRotation = Quaternion.LookRotation(lockOnTarget.transform.position - transform.position);

            transform.rotation = Quaternion.Slerp(transform.rotation, _lockOnRotation, Time.deltaTime * rotationSpeed);
        }
    }


    private void OnAnimatorIK(int layerIndex)
    {
        if (lockedOn)
        {
            if (_lookAtThis != null)
            {
                // distance between face and object to look at
                //float distanceFaceObject = Vector3.Distance(_anim.GetBoneTransform(HumanBodyBones.Head).position, _lookAtThis.position);

                _anim.SetLookAtWeight(headIKWeight, chestIKWeight);
                _anim.SetLookAtPosition(_lookAtThis.position);

                // blend based on the distance
                //_anim.SetLookAtWeight(Mathf.Clamp01(headIKWeight - distanceFaceObject), Mathf.Clamp01(chestIKWeight - distanceFaceObject));
            }
        }
    }
}