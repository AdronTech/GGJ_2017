using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CohesionBehaviour : Steering
{
    public List<MyPhysics> targets;

    public float maxAcc;
    public float radius;

    public CohesionBehaviour()
    {
        targets = new List<MyPhysics>();
    }

    public override SteeringOutput getSteering()
    {
        SteeringOutput steering = new SteeringOutput();

        if (targets.Count == 0)
            return steering;

        Vector3 center = new Vector3();
        center += my.pos;

        foreach (MyPhysics target in targets)
        {
            if (target == my) continue;

            Vector3 diff = my.pos - target.pos;
            if(diff.magnitude < radius)
                center += target.pos;
        }

        center /= targets.Count + 1;

        Vector3 dir = center - my.pos;
        dir.Normalize();
        steering.linear = dir * maxAcc;

        return steering;
    }
}
