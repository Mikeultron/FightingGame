using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : MonoBehaviour
{
    [SerializeField]
    private float hitDamage;

    private void OnTriggerEnter2D(Collider2D other)
    {   
        PlayerDamage damage = other.GetComponent<PlayerDamage>();
        if(damage != null)
        {
            damage.TakeDamage(hitDamage);
        }
        Debug.Log(other.name);
    }
}
