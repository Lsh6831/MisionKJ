using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    private static GameManager m_instance; //싱글턴이 할당될 static 변수
    public bool gamesituation = true;
    private bool isDie = false;

    // 싱글턴 접근용 프로퍼티
    public static GameManager instance
    {
        get
        {
            //만약 싱글턴 변수에 아직 오브젝트가 할당되지 않았다면
            if (m_instance == null)
            {
                // 씬에서 GameManager 오브젝트를 찾아서 할당
                m_instance = FindObjectOfType<GameManager>();
            }
            // 싱글턴 오브젝트 반환
            return m_instance;
        }
    }
    public GameObject menu;
    public int clickCount = 0;
    public GameObject die;
    public TextMeshProUGUI lifeText;
    public Vector3 playerSpawn;
    public GameObject player;

    public GameObject grenade;
    public GameObject gameOver;
    public GameObject assultRifle;
    public float life = 3;
    private bool isGameOver = false;

    public GameObject camera1;
    public GameObject camera2;
    public GameObject gameEnding;
    public GameObject map;


    private void Update()
    {
        ESCKeyDown();
        Respon();
        Map();
    }
    private void ESCKeyDown()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            clickCount++;
            if (!IsInvoking("DoubleClick"))
                if (!gamesituation)
                {
                    menu.SetActive(false);
                    map.SetActive(false);
                    if (!isDie)
                    {
                        gamesituation = true;
                        GameStart();
                    }
                }
            Invoke("DoubleClick", 1.0f);

        }
        else if (clickCount == 2)
        {
            CancelInvoke("DoubleClick");
            menu.SetActive(true);
            GameStop();
            gamesituation = false;
            clickCount=0;

        }
        else if (clickCount > 2)
        {
            clickCount = 0;
        }
    }
    private void Map()
    {
        if(Input.GetKeyDown(KeyCode.M))
        {
        map.SetActive(true);
        GameStop();
        gamesituation = false;
        }
    }
    private void Respon()
    {
        if (isDie == true && Input.GetKeyDown(KeyCode.F) && life != 0)
        {
            Debug.Log("살아나라");
            isDie = false;
            gamesituation = true;
            GameStart();
            HealthHP();
            MaxGrenade();
            MaxMagazine();
            player.GetComponent<CharacterController>().enabled=false;
            player.transform.position = playerSpawn;
            player.GetComponent<CharacterController>().enabled=true;


            die.SetActive(false);
        }
    }
    private void DoubleClick()
    {
        clickCount = 0;

    }
    public void GameStart()
    {
        Debug.Log("restart");
        Time.timeScale = 1f;
        gamesituation = true;
    }
    public void GameStop()
    {
        Time.timeScale = 0f;
        gamesituation = false;
    }
    public void Die()
    {
        life--;
        lifeText.text = "LIFE " + life;
        gamesituation = false;
        GameStop();
        isDie = true;
        if (life > 0)
        {
            die.SetActive(true);
        }
        else
        {
            gameOver.SetActive(true);
        }
    }
    public void HealthHP()
    {
        GameObject.Find("Player").GetComponent<Status>().IncreaseHP(100);
        //    playerController.HealthHP();
    }
    public void MaxMagazine()
    {
        // GameObject.Find("arms_assault_rifle_01").SetActive(true);
        assultRifle.SetActive(true);
        GameObject.Find("arms_assault_rifle_01").GetComponent<WeaponAssultRifle>().ChargeMaxMagazine();
        GameObject.Find("Player").GetComponent<WeaponSwitchSystem>().SwitchingWeapon(WeaponType.Main);
    }
    public void MaxGrenade()
    {
        grenade.SetActive(true);
        // GameObject.Find("arms_hand_grenade").SetActive(true);
        GameObject.Find("arms_hand_grenade").GetComponent<WeaponGrenade>().ChargeMaxGrenade();
        // GameObject.Find("arms_hand_grenade").SetActive(false);
        grenade.SetActive(false);
        // GameObject.Find("arms_assault_rifle_01").SetActive(true);
        // assultRifle.SetActive(true);

        GameObject.Find("Player").GetComponent<WeaponSwitchSystem>().SwitchingWeapon(WeaponType.Main);

    }

    public IEnumerator GameEnd()
    {
         yield return new WaitForSeconds(25f);
        camera1.SetActive(false);
        camera2.SetActive(true);
        
         yield return new WaitForSeconds(35f);
         gameEnding.SetActive(true);
         menu.SetActive(true);
            GameStop();
            gamesituation = false;


    }







}