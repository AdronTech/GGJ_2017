using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CohesionBehaviour : Steering
{
    private List<MyPhysics> targets;

    public float maxAcc;
    public float radius;

    public CohesionBehaviour()
    {
        targets = new List<MyPhysics>();
    }

    public override SteeringOutput getSteering()
    {
        targets.Clear();

        Collider[] colls = Physics.OverlapSphere(my.pos, radius);

        foreach (Collider col in colls)
        {
            CohesionBehaviour other = col.gameObject.GetComponent<CohesionBehaviour>();
            if (other != null && other != this)
                targets.Add(other.gameObject.GetComponent<MyPhysics>());
        }

        SteeringOutput steering = new SteeringOutput();

        if (targets.Count == 0)
            return steering;

        Vector3 center = new Vector3();
        center += my.pos;

        foreach (MyPhysics target in targets)
        {
            center += target.pos;
        }

        center /= targets.Count + 1;

        Vector3 dir = center - my.pos;
        dir.Normalize();
        steering.linear = dir * maxAcc;

        return steering;
    }
}
