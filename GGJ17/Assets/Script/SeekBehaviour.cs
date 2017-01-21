
using System;
using UnityEngine;

[Serializable]
public class SeekBehaviour : Steering {

    public MyPhysics target;
    public float maxAcc;
    public float maxSpeed;
    private float targetSpeed;

    public float targetRadius;
    public float slowRadius;

    public float timeToTarget;

    public override SteeringOutput getSteering()
    {
        SteeringOutput steering = new SteeringOutput();
        Vector3 dir = target.pos - my.pos;

        float dist = dir.magnitude;

        if (dist < targetRadius)
            return steering;

        if (dist > slowRadius)
            targetSpeed = maxSpeed;
        else
            targetSpeed = maxSpeed * dist / slowRadius;

        Vector3 targetVel = dir.normalized * targetSpeed;

        steering.linear = (targetVel - my.vel) / timeToTarget;


        if (steering.linear.magnitude > maxAcc)
        {
            steering.linear.Normalize();
            steering.linear *= maxAcc;
        }

        return steering;
    }

}
