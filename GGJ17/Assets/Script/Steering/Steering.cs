using System;
using UnityEngine;

[RequireComponent(typeof(MyPhysics))]
public abstract class Steering : MonoBehaviour {

    public float weight;
    protected MyPhysics my;

    public bool Enabled;

    public void Awake()
    {
        my = GetComponent<MyPhysics>();
        Enabled = true;
    }

    public abstract SteeringOutput getSteering();

}
