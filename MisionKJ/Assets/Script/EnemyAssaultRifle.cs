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
    private Transform casingSpawnPoint; // 탄피 생성 위치
    [SerializeField]
    private Transform bulletSpawnPoit; // 총알 생성 위치

    [Header("Audio Clips")]
    private AudioClip audioClip; // 사운드
    [SerializeField]

    private CasingMemoryPool casingMemoryPool; // 탄피 생성 후 활성/비활성 관리
    private ImpactMemoryPool impactMemoryPool; // 공격 효과 생성 후 활성/비활성 관리
    public float attackDistance; // 공격 사거리
    public int damage; // 공격 데미지
    private Camera mainCamera; //광선 발사

    private Animator animator;

  private void Awake() 
    {
        
        casingMemoryPool = GetComponent<CasingMemoryPool>();
        impactMemoryPool = GetComponent<ImpactMemoryPool>();
        animator = GetComponentInParent<Animator>();
        audioClip = GetComponent<AudioClip>();

    }
    private void OnEnable() 
    {
      
    //   muzzleFalshEffect.SetActive(false);
    } 
    public void StartWeaponAction(int type=0)
    {
        
        // 마우스 왼쪽 클릭 (공격 시작)
        if (type ==0)
        {           
                OnAttack();
            
        }

    }
    public void OnAttack()
    {        
            
            
            
            //animator.Play("Fire",-1,0);
            string animation = "Fire";
            animator.Play(animation, -1, 0);
           
            // 공격 사운드 재생
            // PlaySound(audioClipFire);
            // 탄피 생성
            casingMemoryPool.SpawnCasing(casingSpawnPoint.position, transform.right);

            // 광선을 발사해 원하는 위치 공격 (+impact Effect)
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
