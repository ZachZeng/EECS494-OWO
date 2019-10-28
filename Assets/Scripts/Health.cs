using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Health : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] int maxHealth = 100;
    int currentHealth;
    public event Action<float> onHealthChange = delegate { };

    private void OnEnable()
    {
        currentHealth = maxHealth;
    }
    public void ModifyHealth(int amount)
    {
        currentHealth += amount;
        float currentHealthPct = (float)currentHealth / (float)maxHealth;
        onHealthChange(currentHealthPct);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ModifyHealth(-10);
        }
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
