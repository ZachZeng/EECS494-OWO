using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*  Escort_State
 *  Since there is only one escort object we need in one scene, we can use a instance to store health information
 *  The property of the escort:
 *  1. Health, related varibale: curEscortHealth,initialEscortHealth 
 *  2. Status, live or die, related varibale: escortStatus
 *  3. ShieldState :TODO in the future, we need PBR rendering Pipeline
 *  4. GoalState, for convinience, use true to represent that the escort has reached the final goal
 */

public class Escort_State : MonoBehaviour
{
    public static Escort_State instance;
    //escortHealth: current health of this escort object
    private int curEscortHealth;
    //escortStatus: the life state of this escort object
    public bool escortStatus;
    //initialEscortHealth : the initial value of health
    [SerializeField]
    int initialEscortHealth = 1000;
    //shieldState: TODO
    bool shieldState;
    //goalState : if the escort reaches the final goal
    bool goalState;
    //isBlocked : if the bus is blocked
    bool isBlocked;
    bool wobbling;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        curEscortHealth = initialEscortHealth;
        escortStatus = true;
        shieldState = false;
        goalState = false;
        isBlocked = false;
        wobbling = false;
    }

    void Update()
    {
        if (curEscortHealth <= 0) {
            GameObject.Find("CameraRig").GetComponent<CameraController>().GameOverCamListener();
            GameObject.Find("Escort Object").GetComponent<ExplosionDeath>().explode();
        }
    }
    //Method for Health:
    public int getCurrentEscortHealth() {
        return instance.curEscortHealth;
    }
    public void decreaseCurrentEscortHealth(int value) {
        curEscortHealth -= value;
        Camera.main.GetComponent<shakeController>().enabled = true;
        if (!wobbling)
            payloadWobble();
    }
    public void increaseCurrentEscortHealth(int value)
    {
        curEscortHealth += value;
    }
    public int getMaxEscortHealth() {
        return initialEscortHealth;
    }
    //Method for status:
    public bool getStatus() {
        return escortStatus;
    }
    //Method for Shield state:
    public bool getShieldState() {
        return shieldState;
    }
    //Method for goalState:
    public bool getGoalState() {
        return goalState;
    }
    public bool getBlockState()
    {
        return isBlocked;
    }
    public void setBlockState(bool value)
    {
        isBlocked = value;
    }
    public void setGoalState() {
        goalState = true;
    }
    public void payloadWobble()
    {
        StartCoroutine(attackedWobble());
    }
    IEnumerator attackedWobble()
    {
        wobbling = true;
        for (float i = 0; i < 1;)
        {
            i += Time.deltaTime;
            transform.eulerAngles = new Vector3(Random.Range(-5, 5),transform.eulerAngles.y, Random.Range(-5, 5));
            yield return new WaitForSeconds(0.025f);
        }
        // transform.eulerAngles = new Vector3(0, 90, 0);
        wobbling = false;
    }
}
