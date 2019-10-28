using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escort_Enemy_example : MonoBehaviour
{
    // 这是个实例的脚本，用来展示敌人以及同伴如何和需要保护的车辆的交互（扣血）
    // 每隔1秒钟，拥有脚本的敌人就会对车辆造成10点health的伤害
    float t = 0.0f;
    public int EnemyDamage = 5;
    public float EnemyDamageFrequency = 0.5f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionStay(Collision collision)
    {
        t = t + Time.deltaTime;
        if(collision.gameObject.tag == "Escort_Object")
        {
            if(t > EnemyDamageFrequency)
            {
                if (Escort_State.instance.getStatus())
                {
                    Escort_State.instance.decreaseCurrentEscortHealth(EnemyDamage);
                    t = 0;
                }
            } 
        }
        
        Debug.Log(Escort_State.instance.getCurrentEscortHealth());
        Debug.Log(Escort_State.instance.getStatus() ? "Status:live" : "Status:die");
    }
}
