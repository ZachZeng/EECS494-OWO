using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class Health : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] int maxHealth;
    public int currentHealth;
    public event Action<float> onHealthChange = delegate { };

    PlayerRespawn respawner;
    bool respawned = false;

    public GameObject floatingText;




    private void Awake()
    {
        currentHealth = maxHealth;
        respawner = GetComponent<PlayerRespawn>();

    }

    public void ModifyHealth(int amount)
    {
        if (floatingText != null)
        {
            showFloatingText(amount);
        }


        if (!respawned)
        {
            currentHealth += amount;
            float currentHealthPct = (float)currentHealth / (float)maxHealth;
            onHealthChange(currentHealthPct);
        }
    }

    void showFloatingText(int amount)
    {
        GameObject ft = Instantiate(floatingText, transform.position, Quaternion.identity, transform);
        ft.GetComponent<TextMesh>().text = amount.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ModifyHealth(-10);
        }

        if(currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }
        if (currentHealth <= 0)
        {
            if (gameObject.tag.Contains("Boss"))
            {
                GameObject.Find("Boss").GetComponent<BossControl>().isBossDead = true;
                GameObject.Find("DoorControl").GetComponent<ArenaWall>().letWallDown();
            }
            if (gameObject.tag == "Player" && !respawned)
            {
                if(gameObject.name == "Knight")
                {
                    gameObject.GetComponent<PlayerMovement>().canMove = false;
                }
                else if(gameObject.name == "Mage")
                {
                    gameObject.GetComponent<MagePlayerMovement>().canMove = false;
                    if(gameObject.GetComponent<MageAttack>().isHailCasting)
                    {
                        gameObject.GetComponent<MageAttack>().StopCast();
                    }
                }
                StartCoroutine(Respawn());
            }
            else if (gameObject.tag != "Player")
            {
                if (gameObject.name == "Obstacle_Road") {
                    Escort_State.instance.setBlockState(false);
                    Destroy(gameObject);
                }
                else if(gameObject.name.Contains("HM_cannon_1"))
                {
                    Destroy(gameObject);
                }
                else
                {
                    GetComponent<Animator>().SetTrigger("Die");
                    Destroy(gameObject, 1f);
                }

                
            }
        }
    }


    IEnumerator Respawn()
    {
        respawned = true;
        gameObject.GetComponent<Bouding>().enabled = false;
        gameObject.transform.position = new Vector3(-90f, 0, -3f);
      
        yield return new WaitForSeconds(3);
        respawner.Respawn();
        for (int i = 1; i < 3; i++)
        {
            onHealthChange(i * 0.5f);
            yield return new WaitForSeconds(1f);
        }
        //yield return new WaitForSeconds(4);
        if (gameObject.name == "Knight")
        {
            gameObject.GetComponent<PlayerMovement>().canMove = true;
            Debug.Log("restore movement");
        }
        else if (gameObject.name == "Mage")
        {
            gameObject.GetComponent<MagePlayerMovement>().canMove = true;
        }
        //onHealthChange(1);
        currentHealth = 100;
        gameObject.GetComponent<Bouding>().enabled = true;
        respawned = false;

    }
}
