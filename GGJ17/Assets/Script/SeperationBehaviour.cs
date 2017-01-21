using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeperationBehaviour : Steering
{
    private List<MyPhysics> targets;

    public float threshold;
    public float decayCoefficient;

    public float maxAcc;
    public float radius;

    public SeperationBehaviour()
    {
        targets = new List<MyPhysics>();
    }

    public override SteeringOutput getSteering()
    {
        targets.Clear();

        Collider[] colls = Physics.OverlapSphere(my.pos, radius);

        foreach (Collider col in colls)
        {
            if (col.gameObject.GetComponent<SeperationBehaviour>() != null)
                targets.Add(col.gameObject.GetComponent<MyPhysics>());
        }

SteeringOutput steering = new SteeringOutput();

        foreach (MyPhysics target in targets)
        {
            Vector3 dir = my.pos - target.pos;
            float dist = dir.magnitude;

            if(dist < threshold)
            {
                float strength = Math.Min(decayCoefficient / (dist * dist), maxAcc);

                dir.Normalize();
                steering.linear += strength * dir;
            }
        }

        return steering;
    }
}
