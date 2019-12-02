using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ObstacleFeedback:
public class ObstacleFeedback : MonoBehaviour
{
    // Start is called before the first frame update
    int previousHealth;
    Health health;
    public Material originalMaterial;
    public Material beAttackedMaterial;
    public ParticleSystem particle;
    public bool beAttackState = false;
    void Start()
    {
        if (gameObject.name != "Obstacle_Road" || originalMaterial == null || beAttackedMaterial == null || particle == null)
        {
            gameObject.GetComponent<ObstacleFeedback>().enabled = false;
        }
        else {
            health = gameObject.GetComponent<Health>();
            previousHealth = health.currentHealth;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (health.currentHealth < previousHealth|| beAttackState) {
            previousHealth = health.currentHealth;
            StartCoroutine(beAttacked());
        }
    }
    IEnumerator beAttacked() {
        particle.Play();
        gameObject.GetComponent<MeshRenderer>().material = beAttackedMaterial;
        yield return new WaitForSeconds(0.3f);
        particle.Stop();
        gameObject.GetComponent<MeshRenderer>().material = originalMaterial;
    }
    private void OnDestroy()
    {
        StartCoroutine(beAttacked());
    }
}
