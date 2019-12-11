using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossControl : MonoBehaviour
{
    // Start is called before the first frame update
    public HashSet<Collider> encountered = new HashSet<Collider>();
    public float attackFreq;
    public float specialFrq;
    public float speed = 2.0f;
    public GameObject target;
    float specialTimer;
    Animator am;
    public bool isAttacking;
    public bool isTrapped;
    public bool spATK;
    public GameObject hitbox;
    Collider col;
    public int ATK;
    public bool meetBoss;
    public bool isBossDead;
    public ParticleSystem onGround;
    public GameObject bullet;
    Vector3 offset;
    SkinnedMeshRenderer srd;
    public GameObject mRender;
    public float frozenTime;
    public bool getAttacked;
    Material original;
    public Material flash;
    public Material frozen;
    public float attackDistance = 2.0f;
    public GameObject central;
    public float radious;
    public bool isgrounded;
    // particle effects
    public ParticleSystem ps;
    public ParticleSystem ps1;
    public GameObject wall;
    bool first = true;
    bool second = true;
    float oriSpeed;
    float groundTime;
    bool damaged = false;
    float timer;

    void Start()
    {
        gameObject.transform.position = new Vector3(76.5f, 30, 41);
        am = GetComponent<Animator>();
        col = hitbox.GetComponent<BoxCollider>();
        offset = new Vector3(0, 2f, -5f);
        srd = mRender.GetComponent<SkinnedMeshRenderer>();
        original = srd.material;
        target = GameObject.Find("Escort Object");
        isgrounded = false;
        oriSpeed = speed;
        
    }

    private void OnTriggerEnter(Collider other)
    {
        isTrapped |= other.gameObject.name == "CastRange(Clone)";
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(central.transform.position, transform.position) > radious)
        {
            target = GameObject.Find("Escort Object");
        }
        if (meetBoss)
        {
            if (!isgrounded)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(76.5f, 0.1f, 41), Time.deltaTime * 30);
            }
            if (transform.position.y <= 0.2f)
            {
                isgrounded = true;
            }
            if (isgrounded && first)
            {
                ps.Stop();
                ps.Play();
                ps1.Stop();
                ps1.Play();
                first = false;
                groundTime = Time.time;

            }

            if (!first && second)
            {
                StartCoroutine(SmoothRise());
                second = false;
            }
            if (!isTrapped && isgrounded && Time.time - groundTime > 2f)
            {
                Debug.Log("Move!");
                if(!isAttacking)
                {
                    am.SetBool("Walk Forward", true);
                    transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Time.deltaTime * speed);
                    transform.LookAt(target.transform);
                }

                if (Vector3.Distance(target.transform.position, transform.position) < attackDistance)
                {
                    isAttacking = true;
                    timer += Time.deltaTime;
                    specialTimer += Time.deltaTime;
                    speed = 0;
                    am.SetBool("Walk Forward", false);
                    if (specialTimer >= specialFrq)
                    {
                        am.SetTrigger("Attack 02");
                        GameObject b = Instantiate(bullet, transform.position + offset, Quaternion.identity);
                        b.GetComponent<TowerBullet>().Curr_target = GameObject.Find("Escort Object").transform;
                        b.GetComponent<TowerBullet>().dmg = ATK;
                        specialTimer = 0f;
                    }
                    else
                    {
                        spATK = true;
                        am.SetTrigger("Attack 01");
                    }
                    if (timer >= attackFreq)
                    {
                        LaunchAttack();
                        StartCoroutine(Wait());
                        timer = 0;
                    }
 
                }
                else
                {
                    speed = oriSpeed;
                    isAttacking = false;
                }
                    
            }
            JudageGetAttacked();
            JudgeTrapped();
        }
    }

    IEnumerator SmoothRise()
    {
        Vector3 startPos = wall.transform.position;
        Vector3 endPos = new Vector3(startPos.x, startPos.y + 4f, startPos.z);
        float changeTime = 0f;
        while (changeTime <= 2.0f)
        {
            wall.transform.position = Vector3.Lerp(startPos, endPos, changeTime / 2.0f);
            changeTime += Time.deltaTime;
            yield return null;
        }
    }
    void JudgeTrapped()
    {
        if (isTrapped)
        {
            StartCoroutine(Frozen());
        }
    }


    void JudageGetAttacked()
    {
        if (getAttacked)
        {
            am.ResetTrigger("Attack 01");
            am.ResetTrigger("Attack 02");
            am.SetTrigger("Take Damage");
        }
    }

    IEnumerator Frozen()
    {
        srd.material = frozen;
        speed = 0;
        yield return new WaitForSeconds(frozenTime);
        isTrapped = false;
        srd.material = original;
        speed = oriSpeed;
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2);
        spATK = false;
    }



    void LaunchAttack()
    {
        encountered.Clear();
        Collider[] cols = Physics.OverlapBox(col.bounds.center, col.bounds.extents, col.transform.rotation);
        foreach (Collider c in cols)
        {
            if (c.gameObject.CompareTag("Player"))
            {
                if (!encountered.Contains(c))
                {
                    encountered.Add(c);
                    c.gameObject.GetComponent<PlayerHealthControl>().getAttack(ATK);
                }
            }
            if (c.gameObject.tag == "Escort_Object")
            {
                if (!encountered.Contains(c))
                {
                    encountered.Add(c);
                    Escort_State.instance.decreaseCurrentEscortHealth(ATK);
                }
            }
        }
    }
}
