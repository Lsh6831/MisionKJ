using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHUD : MonoBehaviour
{
    private WeaponBase weapon; // 현재 정보가 출력되는 무기
    [Header("Components")]
    [SerializeField]
    private Status status; // 플레이어의 상태 (이동속도, 체력)

    [Header("Weapon Base")]
    [SerializeField]
    private TextMeshProUGUI textWeaponName; // ���� �̸�
    [SerializeField]
    private Image imageWeaponIcon; // ���� ������
     [SerializeField]
    private Sprite[] spriteWeaponIcons; // ���� �����ܿ� ���Ǵ� sprite �迭
    [SerializeField]
    private Vector2[] sizeWeaponIcons; // 무기 아이콘의 Ui 크기 배열

    [Header("Ammo")]
    [SerializeField]
    private TextMeshProUGUI textAmmo; //���� / �ִ� ź �� ��� Text

    [Header("Magazine")]
    [SerializeField]
    private GameObject magazineUIPrefab; //źâ UI ������
    [SerializeField]
    private Transform magazineParent; // źâ UI�� ��ġ�Ǵ� Panael
    [SerializeField]
    private int maxMagazineCount; // 처음 생성하는 최대 탄창 수

    private List<GameObject> magazineList; // źâ UI ����Ʈ

    [Header("HP & BloodScreen UI")]
    [SerializeField]
    private TextMeshProUGUI textHP; // 플레이어의 체력을 출력하는 Text
    [SerializeField]
    private Image imageBloodScreen; // 플레이어가 공격받았을 떄 화면에 표시되는 이미지
    [SerializeField]
    private AnimationCurve curveBloodScreen;
   


    private void Awake()
    {
        
        // �޼ҵ尡 ��ϵǾ� �ִ� �̺�Ʈ Ŭ����(weapon.xx)��
        // Invoke() �޼ҵ尡 ȣ��� �� ��ϵ� �޼ҵ�(�Ű�����) �� ����ȴ�
        
        status.onHPEvent.AddListener(UpdateHPHUD);
    } 

    public void SetupAllWeapons(WeaponBase[] weapons)
    {
        SetupMagazine();

        //사용 가능한 모든 무기 이벤트의 등록
        for (int i = 0; i < weapons.Length; ++i)
        {
            weapons[i].onAmmoEvent.AddListener(UpdateAmmoHUD);
            weapons[i].onMagazineEvent.AddListener(UpdateMagazineHUD);
        }
    }
       public void SwitchingWeapon(WeaponBase newWeapon)
    {
        weapon = newWeapon;

        SetupWeapon();
    }

    private void SetupWeapon()
    {
        textWeaponName.text = weapon.WeaponName.ToString();
        imageWeaponIcon.sprite = spriteWeaponIcons[(int)weapon.WeaponName];
        imageWeaponIcon.rectTransform.sizeDelta = sizeWeaponIcons[(int)weapon.WeaponName];
    }

    private void UpdateAmmoHUD(int currentAmmo, int maxAmmo)
    {
        textAmmo.text = $"<size=40>{currentAmmo}/</size>{maxAmmo}";
    }
    private void UpdateHPHUD(int previous,int current)
    {
       textHP.text = "HP " + current;

        // 체력이 증가했을 때는 화면에 빨간색 이미지를 출력하지 않도록 return
        if (previous <= current) return;

        if (previous - current > 0)
        {
            StopCoroutine("OnBloodScreen");
            StartCoroutine("OnBloodScreen");
        }
    }

     private IEnumerator OnBloodScreen()
    {
        float percent = 0;

        while (percent < 1)
        {
            percent += Time.deltaTime;

            Color color = imageBloodScreen.color;
            color.a = Mathf.Lerp(1, 0, curveBloodScreen.Evaluate(percent));
            imageBloodScreen.color = color;

            yield return null;
        }
    }

    private void SetupMagazine()
    {      
		// weapon�� ��ϵǾ� �ִ� �ִ� źâ ������ŭ Image Icon�� ����
		// magazineParent ������Ʈ�� �ڽ����� ��� �� ��� ��Ȱ��ȭ/����Ʈ�� ����
		magazineList = new List<GameObject>();
        for (int i = 0; i < maxMagazineCount; ++i)
        {
            GameObject clone = Instantiate(magazineUIPrefab);
            clone.transform.SetParent(magazineParent);
            clone.SetActive(false);

            magazineList.Add(clone);
        }
    }

    private void UpdateMagazineHUD(int currentMagazine)
    {
        // ���� ��Ȱ��ȭ�ϰ�, currentmagazine ������ŭ Ȱ��ȭ
        for (int i = 0; i < magazineList.Count; ++i)
        {
            magazineList[i].SetActive(false);
        }
        for (int i = 0; i < currentMagazine; ++i)
        {
            magazineList[i].SetActive(true);
        }
    }

}
