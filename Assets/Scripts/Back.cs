using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Back : MonoBehaviour
{
    private GameManager gm;

    public void BackButton() {
        SceneManager.LoadScene("Menu", 0);
    }

    public void BackDemoButton()
    {
        gm = FindObjectOfType<GameManager>();


        DetermineBackButtonState();


    }

    public void Continue()
    {
        SceneManager.LoadScene("Bedroom", 0);
    }


    void DetermineBackButtonState() {
        if (gm.currentTokens == 8)
        {
            SceneManager.LoadScene("End", 0);
        }
        else
        {

            SceneManager.LoadScene("Menu", 0);
        }
    }
}
