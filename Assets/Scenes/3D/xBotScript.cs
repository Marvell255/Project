using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class xBotScript : MonoBehaviour
{
    private Animator _animator;
    public Transform Target;
    public float PositionWeight=1;
    public float RotationWeight=1;

    public xBotScript(float positionWeight)
    {
        PositionWeight = positionWeight;
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnAnimatorIK(int layerIndex)
    {
        _animator.SetIKPositionWeight(AvatarIKGoal.RightHand, PositionWeight);
        _animator.SetIKRotationWeight(AvatarIKGoal.RightHand, RotationWeight);
        _animator.SetIKPosition(AvatarIKGoal.RightHand, Target.position);
        _animator.SetIKRotation(AvatarIKGoal.RightHand, Target.rotation);
    }
}
