using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    public void OnClickAgain()
    {
        GameManager.instance.menu.SetActive(false);
        // GameManager.instance.gamesituation=true;
        GameManager.instance.GameStart();
        Debug.Log("다시다시다시");
    }
    
    public void Restart()
    {
        GameManager.instance.menu.SetActive(false);
        Scene scene = SceneManager.GetActiveScene();
       GameManager.instance.GameStart();
        SceneManager.LoadScene(scene.name);
    }

    public void GoMain()
    {
        GameManager.instance.GameStart();
        SceneManager.LoadScene("Main");
    }

    public void ExitGame()
    {
         Application.Quit();
    }

}
