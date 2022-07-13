using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAssaultRifle : MonoBehaviour
{
    [Header("Fire Effects")]
    [SerializeField]
    // private GameObject muzzleFalshEffect; // 총구 임펙트 (on//off)

    [Header("Audio Clips")]
    private AudioClip audioClip; // 사운드
  private void Awake() 
    {
        
    audioClip = GetComponent<AudioClip>();

    }
    public void StartWeaponAction(int type=0)
    {

    }
    public void OnAttack()
    {        
        
            // 총구 이펙트 재생(default mode 일 때만 재생)
            StartCoroutine("OnMuzzleFlashEffect");
            // 공격 사운드 재생
            // PlaySound(audioClipFire);
    }
    //   private IEnumerator OnMuzzleFlashEffect()
    // {
    //     muzzleFalshEffect.SetActive(true);

    //     yield return new WaitForSeconds(0.3f);
    //     //0.3초 활성화후 비활성화

    //     muzzleFalshEffect.SetActive(false);
    // }

    

}
