using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escort_Enemy_example : MonoBehaviour
{
    // 这是个实例的脚本，用来展示敌人以及同伴如何和需要保护的车辆的交互（扣血）
    // 每隔1秒钟，拥有脚本的敌人就会对车辆造成10点health的伤害
    float t = 0.0f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        if (t > 1.0f) {
            t = 0;
            if (Escort_State.instance.getStatus()){
                Escort_State.instance.decreaseCurrentEscortHealth(10);
            }
            Debug.Log(Escort_State.instance.getCurrentEscortHealth());
            Debug.Log(Escort_State.instance.getStatus() ? "Status:live" : "Status:die");
        }
    }
}
