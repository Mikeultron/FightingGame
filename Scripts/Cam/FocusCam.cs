using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusCam : MonoBehaviour
{
    
    public float halfBoundX = 20f;    
    public float halfBoundY = 15f;
    public float halfBoundZ = 15f;
    public Bounds FocusBounds;

    void Update()
    {
        Vector3 position = gameObject.transform.position;
        Bounds bounds = new Bounds();
        bounds.Encapsulate(new Vector3(position.x - halfBoundX, position.y - halfBoundY, position.z - halfBoundZ));
        bounds.Encapsulate(new Vector3(position.x + halfBoundX, position.y + halfBoundY, position.z + halfBoundZ));
        FocusBounds = bounds;
    }
}
