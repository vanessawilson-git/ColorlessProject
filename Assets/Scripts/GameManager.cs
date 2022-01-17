using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GameManager : MonoBehaviour
{
    public int currentTokens;
    public TMP_Text tokenText;
    public GameObject bed;
    public GameObject bass;
    public GameObject chair;
    public GameObject flowers;

    public Material bedM;
    public Material bassM;
    public Material chairM;
    public Material flowersM;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RenderBluethingsOnTokenComplete();
    }

    public void AddGold(int tokensToAdd)
    {
        currentTokens += tokensToAdd;
        tokenText.text = "Tokens: " + currentTokens + "/8";
    }


    void RenderBluethingsOnTokenComplete() {
        if (currentTokens == 8)
        {

            bed.GetComponent<Renderer>().material = bedM;
            bass.GetComponent<Renderer>().material = bassM;
            chair.GetComponent<Renderer>().material = chairM;
            flowers.GetComponent<Renderer>().material = flowersM;

        }
    }
}
