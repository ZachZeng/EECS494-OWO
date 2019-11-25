using UnityEngine;
using System.Collections;

public class Tower : MonoBehaviour
{


    public float areaRaidus;
    public bool canAttack;
    public bool Catcher = false;
    public Transform shootElement;
    public GameObject Towerbug;
    public Transform LookAtObj;
    public GameObject bullet;
    public GameObject DestroyParticle;
    public Vector3 impactNormal_2;
    private Transform[] targets;
    public int dmg = 10;
    public float shootDelay;
    bool isShoot;
    public Animator anim_2;
    private float homeY;
    Transform target;


    // for Catcher tower 

    void Start()
    {
        anim_2 = GetComponent<Animator>();
        homeY = LookAtObj.transform.localRotation.eulerAngles.y;
        
        canAttack = false;

    }


    private int FindNearestIndex()
    {
        Transform mage = GameObject.Find("Mage").transform;
        Transform knight = GameObject.Find("Knight").transform;
        Transform payload = GameObject.Find("Escort Object").transform;
        targets = new Transform[3] { mage, knight, payload };

        int length = targets.Length;
        int min_index = -1;
        float min_dist = areaRaidus;
        for (int i = 0; i < length; ++i)
        {
            float dist = Vector3.Distance(targets[i].position, transform.position);
            if (dist < min_dist)
            {
                min_index = i;
                min_dist = dist;
            }
        }
        return min_index;

    }



    void Update()
    {

        if(GameController.instance.isGameBegin)
        {
            if (FindNearestIndex() != -1)
            {
                target = targets[FindNearestIndex()];
                canAttack = true;
            }
            else
            {
                canAttack = false;
            }



            // Tower`s rotate
            if (canAttack)
            {
                if (target)
                {
                    Vector3 dir = target.transform.position - LookAtObj.transform.position;
                    dir.y = 0;
                    Quaternion rot = Quaternion.LookRotation(dir);
                    LookAtObj.transform.rotation = Quaternion.Slerp(LookAtObj.transform.rotation, rot, 5 * Time.deltaTime);
                }

                else
                {
                    Quaternion home = new Quaternion(0, homeY, 0, 1);
                    LookAtObj.transform.rotation = Quaternion.Slerp(LookAtObj.transform.rotation, home, Time.deltaTime);
                }

                // Shooting
                if (!isShoot)
                {
                    StartCoroutine(shoot());
                }
            }
        }
    }

    IEnumerator shoot()
    {
        isShoot = true;
        yield return new WaitForSeconds(shootDelay);


        if (target && Catcher == false)
        {
            GameObject b = GameObject.Instantiate(bullet, shootElement.position, Quaternion.identity) as GameObject;
            Transform ta = target.transform;
            b.GetComponent<TowerBullet>().Curr_target = ta;
            b.GetComponent<TowerBullet>().twr = this;
            b.GetComponent<TowerBullet>().dmg = dmg;

        }

        if (target && Catcher == true)
        {
            anim_2.SetBool("Attack", true);
            anim_2.SetBool("T_pose", false);
        }

        isShoot = false;
    }

    void StopAttack()

    {
        target = null;
        anim_2.SetBool("Attack", false);
        anim_2.SetBool("T_pose", true);
    }


}



