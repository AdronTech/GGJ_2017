using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeperationBehaviour : Steering
{
    private List<MyPhysics> targets;

    public float maxAcc;
    public float radius;

    public SeperationBehaviour()
    {
        targets = new List<MyPhysics>();
    }

    public override SteeringOutput getSteering()
    {
        // Detect nearby creatures
        targets.Clear();

        Collider[] colls = Physics.OverlapSphere(my.pos, radius);

        foreach (Collider col in colls)
        {
            SeperationBehaviour other = col.gameObject.GetComponent<SeperationBehaviour>();
            if (other != null && other != this)
                targets.Add(other.gameObject.GetComponent<MyPhysics>());
        }

        SteeringOutput steering = new SteeringOutput();

        foreach (MyPhysics target in targets)
        {
            Vector3 diff = my.pos - target.pos;
            steering.linear += diff / (diff.magnitude * diff.magnitude);
        }

        if (targets.Count > 0)
            steering.linear /= targets.Count;

        steering.linear = steering.linear.normalized * my.maxVel;

        return steering;
    }
}
