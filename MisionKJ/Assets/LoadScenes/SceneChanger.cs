using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    
    public void GameStartButoon()
    {
        LoadingSceneManager.LoadScene("Map_v1");
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player")
        {
            LoadingSceneManager.LoadScene("Map_v2");
        }
    }
}
