using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlignmentBehaviour : Steering {

    private List<MyPhysics> targets;

    public float maxAcc;
    public float radius;

    public AlignmentBehaviour()
    {
        targets = new List<MyPhysics>();
    }

    public override SteeringOutput getSteering()
    {
        targets.Clear();

        Collider[] colls = Physics.OverlapSphere(my.pos, radius);

        foreach (Collider col in colls)
        {
            if (col.gameObject.GetComponent<CohesionBehaviour>() != null)
                targets.Add(col.gameObject.GetComponent<MyPhysics>());
        }

        SteeringOutput steering = new SteeringOutput();

        if (targets.Count == 0)
            return steering;

        Vector3 vel = new Vector3();
        foreach (MyPhysics target in targets)
        {
            vel += target.vel;
        }

        vel /= targets.Count;
        steering.linear = Vector3.ClampMagnitude(vel * maxAcc, maxAcc);

        return steering;
    }
}
