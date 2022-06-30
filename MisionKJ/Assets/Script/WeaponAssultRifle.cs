using System.Collections; //코르틴 사용을 위함 이름 공간 정의
using UnityEngine;

public class WeaponAssultRifle : MonoBehaviour
{
    [Header("Fire Effects")]
    [SerializeField]
    private GameObject muzzleFalshEffect; // 총구 임펙트 (on//off)

    [Header("Audio Clips")]
    [SerializeField]
    private AudioClip audioClipTakeOutWeapon; // 무기 장착 사운드
    [SerializeField]
    private AudioClip audioClipFire; // 공격 사운드

    [Header("Weapon Setting")]
    [SerializeField]
    private WeaponSetting weaponSetting; // 무기 설정

    private float lastAttackTime =0; // 마지막 발사시간 체크;

    private AudioSource audioSource; //사운드 재생 컴포넌트
    private PlayerAnimatorController animator; // 애니메이션 재생 제어

    private void Awake() 
    {
      audioSource = GetComponent<AudioSource>();   
      animator =GetComponentInParent<PlayerAnimatorController>();
    }
    private void OnEnable() 
    {
      // 무기 장착 사운드 재생
      PlaySound(audioClipTakeOutWeapon);  
      // 종구 임펜트 오브젝ㅌ으 비활성화
      muzzleFalshEffect.SetActive(false);
    }
    private void PlaySound(AudioClip clip)
    {
        audioSource.Stop();  // 기존에 재생중인 사운드 정지
        audioSource.clip = clip;  // 새로운 사운드 clip으로 교체 후
        audioSource.Play();  // 사운드 재생
    }

    public void StartWeaponAction(int type=0)
    {
        // 마우스 왼쪽 클릭 (공격 시작)
        if(type ==0)
        {
            // 연속 공격
            if( weaponSetting.isAutomaticAttack==true)
            {
                StartCoroutine("OnAttackLoop");
            }
            // 단발 공격
            else
            {
                OnAttack();
            }
        }
    }
    public void StopWeaPonAction(int type=0)
    {
        // 마우스 왼쪽 클릭 (공격 종료)
        if( type ==0)
        {
            StopCoroutine("OnAttackLoop");
        }
    }
    
    private IEnumerator OnAttackLoop()
    {
        while(true)
        {
            OnAttack();
           
            yield return null;
        }
    }
    public void OnAttack()
    {
        if( Time.time - lastAttackTime>weaponSetting.attackRate)
        // 현재시간 - 라스트어택타임 이 무기 딜레이보다 클때
        {
            //뛰고있을 때는 공격할 수 없다
            if( animator.MoveSpeed>0.5f)
            {
                return;
            }

            // 공격주기가 되어야 공격할 수 있도록 하기 위해 현재 시간 저장
            lastAttackTime = Time.time;

            // 무기 애니메이션 재생
            animator.Play("Fire",-1,0);
            // 총구 이펙트 재생
            StartCoroutine("OnMuzzleFlashEffect");
            // 공격 사운드 재생
            PlaySound(audioClipFire);
        }
    }
    private IEnumerator OnMuzzleFlashEffect()
    {
        muzzleFalshEffect.SetActive(true);

        yield return new WaitForSeconds(weaponSetting.attackRate*0.3f);
        //0.3초 활성화후 비활성화

        muzzleFalshEffect.SetActive(false);
    }
}
