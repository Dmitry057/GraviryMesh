
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class GravitySimulation : MonoBehaviour
{
  
    public Vector3 _startForce;
    [SerializeField]
    private bool isStatic;
    
    [SerializeField] private float maxDistance = 10f;
    
    [HideInInspector]
    public Rigidbody rb;

    private List<GravitySimulation> OtherSimulations = new List<GravitySimulation>();
    [HideInInspector]
    public const float G = 0.0000000000667f;

    private const float _unityMass = 10000000000; //adopts unity mass to real mass
    public virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = isStatic;
        var others = FindObjectsOfType<GravitySimulation>();
        foreach (var other in others)
        {
            if (other != this && other != null)
                OtherSimulations.Add(other);
        }
    }

    private void FixedUpdate()
    {
        if (isStatic)
            return;
        AttractUpdately();
    }

    public virtual void AttractUpdately()
    {
        foreach (var other in OtherSimulations)
        {
            Attract(other);
        }
    }
    public virtual void Attract(GravitySimulation attractedObj)
    {
        Rigidbody rbToAttract = attractedObj.rb;

        Vector3 direction = rbToAttract.position - rb.position;
        float distance = direction.magnitude;

        if (distance > maxDistance)
            return;
        
        float forceMagnitude = Mathf.Clamp(G*_unityMass*(rb.mass * rbToAttract.mass) / Mathf.Pow(distance, 2), 0, 1000);
        Vector3 force = direction.normalized * forceMagnitude;
        rb.AddForce(force);
    }
} 