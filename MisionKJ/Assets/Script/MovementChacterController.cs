using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
// 이 명령이 포함된 스크립트를 게임 오브젝트에 컴포넌트로 적용하면 해당 컴포넌트도 같이 추가된다.
public class MovementChacterController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;  //이동 속도
    private Vector3 moveForce; //이동 힘 (x,z와 y축을 별도로 계산해 실제 이동에 적용)

    [SerializeField]
    private float jumpForce; // 점프 힘
    [SerializeField]
    private float gravity; // 중력 계수
    public float MoveSpeed
    {
        set => moveSpeed = Mathf.Max(0, value);  // Mathf.Max = 이동속도가 음수가 되지 않게 한다.
        get => moveSpeed;
    }
    [SerializeField]
    private CharacterController characterController; // 플레이어 이동 제어를 위한 컴포넌트

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        // 1초당 moveForce 속력으로 이동
        characterController.Move(moveForce * Time.deltaTime);
        

        // 허공에 떠있으면 중력만큼 y축 이동속도 감소
        if (!characterController.isGrounded)
        {
            moveForce.y += gravity * Time.deltaTime;
        }

    }

    public void MoveTo(Vector3 direcion)
    {
        // 이동 방향 = 캐릭터의 회전값 * 방향 값
        direcion = transform.rotation * new Vector3(direcion.x, 0, direcion.z);

        // 이동 힘 = 이동방향 * 속도
        moveForce = new Vector3(direcion.x * moveSpeed, moveForce.y, direcion.z * moveSpeed);
    }
    public void Jump()
    {
        // 플레이어가 바닥에 있을 때만 점프 가능
        if (characterController.isGrounded)
        {
            moveForce.y = jumpForce;
        }
    }
}
