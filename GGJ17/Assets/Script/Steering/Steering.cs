using System;
using UnityEngine;

[RequireComponent(typeof(MyPhysics))]
public abstract class Steering : MonoBehaviour {

    public float weight;
    protected MyPhysics my;

    public void Awake()
    {
        my = GetComponent<MyPhysics>();
    }

    public abstract SteeringOutput getSteering();

}
