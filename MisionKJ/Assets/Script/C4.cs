using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class C4 : MonoBehaviour
{
    public GameObject onC4Point;
    public GameObject offC4Point;
    [SerializeField]
    MeshRenderer sr;
    public GameObject c4;

    public string m_Timer = @"00:00.000"; 
    private bool isCountDown = false;
     public float m_TotalSeconds = 30;
     public TextMeshProUGUI  m_Text; 
     public GameObject textBackGround;

    [SerializeField]
    private bool c4OnOff = false;
    [SerializeField]
    private bool c4Install = false;
    void start()
    {
        m_Timer = CountdownTimer(false);
        sr = c4.GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        C4InstallLoad();
        if(isCountDown)
        {
            m_Timer = CountdownTimer();
        }
        if (m_TotalSeconds <= 0) 
        { 
            SetZero(); 
            //... 여기에 카운트 다운이 종료 될때 [이벤트]를 넣으면 됩니다. 
            Debug.Log("쾅쾅");
            textBackGround.SetActive(false);
        } 
        m_Text.text = m_Timer; 
    }

    private void OnclickC4Button()
    {
        c4OnOff = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("충돌");
            if (c4OnOff)
            {
                onC4Point.SetActive(true);
                  c4Install = true;
            }
            else
            {
                offC4Point.SetActive(true);
            }
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            onC4Point.SetActive(false);
            offC4Point.SetActive(false);
            c4Install = false;
        }
    }
    private void C4InstallLoad()
    {   
        if(c4Install&&Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine("C4Install");
            Debug.Log("설치시작");
        }
        else if(c4Install&&Input.GetKeyUp(KeyCode.E))
        {
            StopCoroutine("C4Install");
            Debug.Log("설치중단");
        }
        
    }

    private IEnumerator C4Install()
    {
        yield return new WaitForSeconds(2f);
        Debug.Log("설치성공");
        sr.material.color = Color.white;
        isCountDown=true;
        textBackGround.SetActive(true);
    }
     private string CountdownTimer(bool IsUpdate = true) 
    { 
        if(IsUpdate) 
            m_TotalSeconds -= Time.deltaTime; 

        TimeSpan timespan = TimeSpan.FromSeconds(m_TotalSeconds); 
        string timer = string.Format("{0:00}:{1:00}.{2:000}", 
            timespan.Minutes, timespan.Seconds, timespan.Milliseconds); 

        return timer; 
    } 
    private void SetZero() 
    { 
        m_Timer = @"00:00:00.000"; 
        m_TotalSeconds = 0; 
        isCountDown = false; 
    } 

}
