using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAssaultRifle : MonoBehaviour
{
    [Header("Fire Effects")]
    [SerializeField]
    private GameObject muzzleFalshEffect; // 총구 임펙트 (on//off)

    [Header("Spawn Points")]
    [SerializeField]
    private Transform bulletSpawnPoit; // 총알 생성 위치

    [Header("Audio Clips")]
    private AudioClip audioClip; // 사운드
    private ImpactMemoryPool impactMemoryPool; // 공격 효과 생성 후 활성/비활성 관리
    public float attackDistance; // 공격 사거리
    public int damage; // 공격 데미지
    private Camera mainCamera; //광선 발사

    private Animator animator;

  private void Awake() 
    {
        
        impactMemoryPool = GetComponent<ImpactMemoryPool>();
        animator = GetComponentInParent<Animator>();
        audioClip = GetComponent<AudioClip>();

    }
    public void OnAttack()
    {        
            TwostepRaycast();
    }

    private void TwostepRaycast()
    {
        Ray ray;
        RaycastHit hit;
        Vector3 targetPoint = Vector3.zero
;       // 화면의 중앙 좌표 (AIm 기준으로 Raycast 연산)
        ray = mainCamera.ViewportPointToRay(Vector2.one * 0.5f);
        // 공격 사거리(attackDIstance) 안에 부딕히는 오브젝트가 있으면 targetPoint 는 광선에 부딕힌 위치
        if(Physics.Raycast(ray,out hit,attackDistance))
        {
            targetPoint = hit.point;
        }
        //공격 사거리 안에 부딕히는 오브젝트가 없으면 targetPoint는 최대 사거리 위치
        else
        {
            targetPoint = ray.origin + ray.direction * attackDistance;
        }
        Debug.DrawRay(ray.origin, ray.direction * attackDistance, Color.red);

        // 첫번째 Raycast연산으로 얻어진 targetPoint를 목표지점으로 설정하고,
        // 종구를 시작지점으로 하여 Raycast 연산
        Vector3 attackDirection = (targetPoint - bulletSpawnPoit.position).normalized;
        if(Physics.Raycast(bulletSpawnPoit.position,attackDirection,out hit, attackDistance))
        {
            impactMemoryPool.SpawnImpact(hit);

            if(hit.transform.CompareTag("ImpactEnemy"))
            {
                hit.transform.GetComponent<PlayerController>().TakeDamage(damage);
            }
            else if( hit .transform.CompareTag("InteractionObject"))
            {
                hit.transform.GetComponent<InteractionObject>().TakeDamage(damage);
            }
        }
        Debug.DrawRay(bulletSpawnPoit.position,attackDirection*attackDistance,Color.blue);

    }


}
