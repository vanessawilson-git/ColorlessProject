using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenuScript : MonoBehaviour
{
    public void ExplorationMode() {

        SceneManager.LoadScene("Message",0);
    }

    public void TestBox()
    {

        SceneManager.LoadScene("TestBox",0);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void FightMode()
    {
        SceneManager.LoadScene("WIP", 0);
    }

}
