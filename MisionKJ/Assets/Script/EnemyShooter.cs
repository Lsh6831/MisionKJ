using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    public Transform gunPivot; // 총 배치의 기준점
    public Transform leftHandMount;  // 총읜 왼쪽 손잡이,왼손이 위치할 지점
    public Transform rightHandMount;  // 총의 오른쪽 손잡이. 오른손이 위치할 지점
    private Animator playerAnimator;  // 애니메이터 컴포넌트
     void Start()
    {
        Debug.Log("EnemyShooter Start!!!");
        playerAnimator=GetComponent<Animator>();           
        // OnAnimatorIK();    
    }

    // 에니메이터의 IK갱신
    private void OnAnimatorIK(int layerIndex)
    {// 총의 기준점 gunPivo을 3D 모델의 오른쪽 팔꿈치 위치로 이동
        Debug.Log("On AnimatorIK 실행");
        gunPivot.position = playerAnimator.GetIKHintPosition(AvatarIKHint.RightElbow);

        //IK를 사용하여 왼손의 위치와 회전을 총의 왼쪽 손잡이에 맞춤
        playerAnimator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0.5f);
        playerAnimator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0.5f);

        playerAnimator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandMount.position);
        playerAnimator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandMount.rotation);

        //IK를 사용하여 오른손의 위치와 회전을 총의 오른쪽 손잡이에 맞춤
        playerAnimator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0.5f);
        playerAnimator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0.5f);

        playerAnimator.SetIKPosition(AvatarIKGoal.RightHand, rightHandMount.position);
        playerAnimator.SetIKRotation(AvatarIKGoal.RightHand, rightHandMount.rotation);
    }

}
