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
            GameManager.instance.GameStop();
            Time.timeScale = 0f;

        }

        if (canvasOpen && Input.GetKeyDown(KeyCode.Escape))
        {

            itemBoxOn.SetActive(true);
            itemBoxOff.SetActive(false);
            canvasOpen = false;
            itemCanvas.SetActive(false);
            GameManager.instance.GameStart();
            // Time.timeScale = 1f;


        }
    }
    public void HealthHPButton()
    {
      GameManager.instance.HealthHP();
    }
    public void MaxMagazineButton()
    {  
      GameManager.instance.MaxMagazine();
    }
    public void MaxGrenadeButton()
    {
       GameManager.instance.MaxGrenade();
        
    }


}
