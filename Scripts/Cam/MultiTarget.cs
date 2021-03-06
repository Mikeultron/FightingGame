﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class MultiTarget : MonoBehaviour
{
   public List<Transform> targets;
   public float smoothTime = .5f;
   public Vector3 offset;
   public Vector3 velocity;
   public float minZoom = 40f;
   public float maxZoom = 10f;
   public float zoomLimiter = 50f;
   private Camera camera;   
   void Start()
   {
       camera = GetComponent<Camera>();
   }   

   private void LateUpdate() 
   {
        for(int i = 0; i < targets.Count; i++)
        {
            if(targets[i] == null)
            {
                return;
            }
        }
        if(targets.Count == 0)
        {            
            return;
        }
        Move();
        Zoom();
   }

    void Zoom()
    {
        float newZoom = Mathf.Lerp(maxZoom, minZoom, GetGreatestDistance() / zoomLimiter);
        camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, newZoom, Time.deltaTime);
    }

    float GetGreatestDistance()
    {
        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for(int i = 0; i < targets.Count; i++)
        {            
            bounds.Encapsulate(targets[i].position);
        }

        return bounds.size.x;
    }

    void Move()
    {
        Vector3 centerPoint = GetCenterPoint();
        Vector3 newPosition = centerPoint + offset;

        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
    }
   Vector3 GetCenterPoint()
   {
       if(targets.Count == 1)
       {
           return targets[0].position;
       }

       var bounds = new Bounds(targets[0].position, Vector3.zero);
       for(int i = 0; i < targets.Count; i++)
       {
           bounds.Encapsulate(targets[i].position);
       }

       return bounds.center;
   }
}
