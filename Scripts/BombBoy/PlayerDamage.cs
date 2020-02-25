using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    public GameObject deathEffect;   
    private Animator anim;
    [SerializeField]
    private float health;
    [SerializeField]
    private float maxHealth;
    [SerializeField]
    private int firstLayer;
    [SerializeField]
    private int secondLayer;
    [SerializeField]
    private float delay = 2f;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        anim = GetComponent<Animator>();
    }

    public void TakeDamage(float damage)
    {    
        health -= damage;
        if(health <= 0)
        {
            health = 0;
            DeathEffect();
            Destroy(gameObject);
            RespawnPlayer();
        }
        else
        {
            StartCoroutine(HitEffect());
        }
    }

    IEnumerator HitEffect()
    {   
        anim.SetBool("hit", true);
        Physics2D.IgnoreLayerCollision(firstLayer, secondLayer, true);
        yield return new WaitForSeconds(delay);
        anim.SetBool("hit", false);
        yield return new WaitForSeconds(.2f);
        Physics2D.IgnoreLayerCollision(firstLayer, secondLayer, false);
    }    

    void DeathEffect()
    {
        if(deathEffect != null)
        {
            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(effect, 2f);
        }
    }

    void RespawnPlayer()
    {
        float respawnTime = 3f;
        respawnTime -= Time.deltaTime;
        if(respawnTime <= 0)
        {
            Instantiate(gameObject);
        }
    }
}
