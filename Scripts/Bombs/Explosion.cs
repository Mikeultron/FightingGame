using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField]
    private float bombDamage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerDamage damage = other.GetComponent<PlayerDamage>();
        if(damage != null)
        {
            damage.TakeDamage(bombDamage);
        }
        Debug.Log(other.name);
    }
}
