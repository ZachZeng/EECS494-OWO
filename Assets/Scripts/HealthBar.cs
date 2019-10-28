﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    public Image foreground;
    [SerializeField]
    public float updateSpeed = 0.5f;

    public bool isEnemy = true;

    private void Awake()
    {
        GetComponent<Health>().onHealthChange += Handle_OnHealthChange;
    }

    void Handle_OnHealthChange(float pct)
    {
        StartCoroutine(ChangeToPct(pct));
    }

    IEnumerator ChangeToPct(float pct)
    {
        float preChangePct = foreground.fillAmount;
        float elasped = 0f;
        while (elasped < updateSpeed)
        {
            elasped += Time.deltaTime;
            foreground.fillAmount = Mathf.Lerp(preChangePct, pct, elasped / updateSpeed);
            yield return null;
        }

        foreground.fillAmount = pct;
    }




    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
