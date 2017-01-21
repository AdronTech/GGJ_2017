using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeperationBehaviour : Steering
{
    public List<MyPhysics> targets;

    public float maxAcc;
    public float radius;

    public SeperationBehaviour()
    {
        targets = new List<MyPhysics>();
    }

    public override SteeringOutput getSteering()
    {
        SteeringOutput steering = new SteeringOutput();

        foreach (MyPhysics target in targets)
        {
            if (target == my) continue;

            Vector3 diff = my.pos - target.pos;
            float dist = diff.magnitude;
            if(dist < radius)
                steering.linear += diff / (dist * dist);
        }

        if (targets.Count > 0)
            steering.linear /= targets.Count;

        steering.linear = steering.linear.normalized * my.maxVel;

        return steering;
    }
}
