using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject bombPrefab;
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Instantiate(bombPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
