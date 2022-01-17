using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;
    public PlayerController player;
    public float invinsibilityLenght;
    public float invinsibilityCounter = 0;

    public Renderer playerRenderer;
    private float flashCounter;
    public float flashLenght = 0.1f;
    private bool isRespawining;
    private Vector3 respawnPoint;
    public float respawnLenght;


    // Start is called before the first frame update
    void Start()
    {
        respawnPoint = player.transform.position;
        currentHealth = maxHealth;
     
    }

    // Update is called once per frame
    void Update()
    {
        if (invinsibilityCounter > 0)
        {
            invinsibilityCounter -= Time.deltaTime;

            flashCounter -= Time.deltaTime;
            if (flashCounter <= 0)
            {
                playerRenderer.enabled = !playerRenderer.enabled;
                flashCounter = flashLenght;
            }

            if (invinsibilityCounter <= 0)
            {
                playerRenderer.enabled = true;
            }
        }
    }


    public void HurtPlayer(int dmg, Vector3 direction)
    {
        if (invinsibilityCounter <= 0)
        {
            currentHealth -= dmg;

            if (currentHealth <= 0)
            {
                Respawn();
            }
            else
            {
                player.KnockBack(direction);

                invinsibilityCounter = invinsibilityLenght;

                playerRenderer.enabled = false;
                flashCounter = flashLenght;
            }

           
        }

       
    }

     void Respawn()
    {
        //player.transform.position = respawnPoint;
        //currentHealth = maxHealth;

        if (!isRespawining)
        {
            StartCoroutine("RespawnCoroutine");
        }
   
    }

     IEnumerator RespawnCoroutine()
    {
        isRespawining = true;
       player.gameObject.SetActive(false);

        yield return new WaitForSeconds(respawnLenght);
        isRespawining = false;

        player.gameObject.SetActive(true);

        CharacterController charController = player.GetComponent<CharacterController>();
        charController.enabled = false;
        player.transform.position = respawnPoint;
        charController.enabled = true;

        currentHealth = maxHealth;

        invinsibilityCounter = invinsibilityLenght;
        playerRenderer.enabled = false;
        flashCounter = flashLenght;

     }



    //public void HealPlayer(int healAmount)
    //{
    //    currentHealth += healAmount;
    //    if (currentHealth > maxHealth)
    //    {
    //        currentHealth = maxHealth;
    //    }
    //}

}


