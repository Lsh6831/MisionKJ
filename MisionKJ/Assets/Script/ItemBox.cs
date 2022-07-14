using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour
{
    public GameObject itemCanvas;
    public GameObject itemBoxOn;
    public GameObject itemBoxOff;
    private bool boxOpen = false;
    private bool canvasOpen = false;
    public GameObject grenade;
    public GameObject assultRifle;
    private PlayerController playerController;

    private void Awake()
    {
    }



    private void Update()
    {
        BoxOpenButon();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            itemBoxOn.SetActive(true);
            playerController = GetComponent<PlayerController>();
            boxOpen = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            itemBoxOn.SetActive(false);
            boxOpen = false;
        }
    }
    private void BoxOpenButon()
    {
        if (boxOpen && Input.GetKeyDown(KeyCode.E))
        {
            itemBoxOn.SetActive(false);
            itemBoxOff.SetActive(true);
            canvasOpen = true;
            itemCanvas.SetActive(true);
            Time.timeScale = 0f;
            GameObject.Find("Player").GetComponent<PlayerController>().isMove = false;

        }

        if (canvasOpen && Input.GetKeyDown(KeyCode.Escape))
        {

            itemBoxOn.SetActive(true);
            itemBoxOff.SetActive(false);
            canvasOpen = false;
            itemCanvas.SetActive(false);
            Time.timeScale = 1f;
             GameObject.Find("Player").GetComponent<PlayerController>().isMove = true;


        }
    }
    public void HealthHPButton()
    {
        GameObject.Find("Player").GetComponent<Status>().IncreaseHP(100);
        //    playerController.HealthHP();
    }
    public void MaxMagazineButton()
    {
        // GameObject.Find("arms_assault_rifle_01").SetActive(true);
        assultRifle.SetActive(true);
        GameObject.Find("arms_assault_rifle_01").GetComponent<WeaponAssultRifle>().ChargeMaxMagazine();
    }
    public void MaxGrenadeButton()
    {
        grenade.SetActive(true);
        // GameObject.Find("arms_hand_grenade").SetActive(true);
        GameObject.Find("arms_hand_grenade").GetComponent<WeaponGrenade>().ChargeMaxGrenade();
        // GameObject.Find("arms_hand_grenade").SetActive(false);
        grenade.SetActive(false);
        // GameObject.Find("arms_assault_rifle_01").SetActive(true);
        assultRifle.SetActive(true);
        
    }


}
