using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    public Transform gunPivot; // 총 배치의 기준점
    public Transform leftHandMount;  // 총읜 왼쪽 손잡이,왼손이 위치할 지점
    public Transform rightHandMount;  // 총의 오른쪽 손잡이. 오른손이 위치할 지점
    private Animator animator;
   
    private void Awake()
    {
        // "Player" 오브젝트 기준으로 자식 오브젝트인
        // "arms_asusault_rifle_01" 오브젝트에 Animator 컴포넌트가 있다
        animator = GetComponentInChildren<Animator>();
    }

    public float MoveSpeed
    // MoveSpeed 프로퍼티
    {
        set => animator.SetFloat("movementSpeed",value);
        get => animator.GetFloat("movementSpeed");
    }

    public void OnReload()
    {
        animator.SetTrigger("onReload");
    }

    // Assault Rifle 마우스 오른쪽 클릭 액션(default/aim mode)
    public bool AimModeIs
    {
        set => animator.SetBool("IsAimMode", value);
        get => animator.GetBool("IsAimMode");
    }

    public void Play(string stateName,int layer,float normalizedTime)
    {
        animator.Play(stateName,layer,normalizedTime);
    }
    public bool CurrentAnimationIs(string name)
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName(name);
    }
    	public void SetFloat(string paramName, float value)
	{
		animator.SetFloat(paramName, value);
	}

        // 에니메이터의 IK갱신
    private void OnAnimatorIK(int layerIndex)
    {// 총의 기준점 gunPivo을 3D 모델의 오른쪽 팔꿈치 위치로 이동
        gunPivot.position = animator.GetIKHintPosition(AvatarIKHint.RightElbow);

        //IK를 사용하여 왼손의 위치와 회전을 총의 왼쪽 손잡이에 맞춤
        animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0.5f);
        animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0.5f);

        animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandMount.position);
        animator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandMount.rotation);

        //IK를 사용하여 오른손의 위치와 회전을 총의 오른쪽 손잡이에 맞춤
        animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0.5f);
        animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0.5f);

        animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandMount.position);
        animator.SetIKRotation(AvatarIKGoal.RightHand, rightHandMount.rotation);
    }
   
}
