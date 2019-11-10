using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Health : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] int maxHealth;
    public int currentHealth;
    public event Action<float> onHealthChange = delegate { };

    private void Awake()
    {
        currentHealth = maxHealth;
    }
    public void ModifyHealth(int amount)
    {
        currentHealth += amount;
        float currentHealthPct = (float)currentHealth / (float)maxHealth;
        onHealthChange(currentHealthPct);
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
            if(gameObject.tag == "Player")
            {
                if(gameObject.name == "Knight")
                {
                    gameObject.GetComponent<PlayerMovement>().canMove = false;
                }
                else if(gameObject.name == "Mage")
                {
                    gameObject.GetComponent<MagePlayerMovement>().canMove = false;
                    if(gameObject.GetComponent<MageAttack>().isCasting)
                    {
                        gameObject.GetComponent<MageAttack>().StopCast();
                    }
                }
                StartCoroutine(Respawn());
            }
            else
            {
                if (gameObject.name == "Obstacle_Road") {
                    Escort_State.instance.setBlockState(false);
                }
                Destroy(gameObject);
            }
        }
    }


    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(3);
        if (gameObject.name == "Knight")
        {
            gameObject.GetComponent<PlayerMovement>().canMove = true;
            Debug.Log("restore movement");
        }
        else if (gameObject.name == "Mage")
        {
            gameObject.GetComponent<MagePlayerMovement>().canMove = true;
        }
        onHealthChange(1);
        currentHealth = 100;
    }
}
