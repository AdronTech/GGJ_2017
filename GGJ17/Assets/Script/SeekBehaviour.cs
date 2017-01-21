
using System;
using UnityEngine;

[Serializable]
public class SeekBehaviour : Steering {

    public MyPhysics target;

    public float maxAcc;
    public float radius;

    public override SteeringOutput getSteering()
    {
        SteeringOutput steering = new SteeringOutput();

        Vector3 des = target.pos - my.pos;
        float d = des.magnitude;
        des.Normalize();

        if(d < radius)
        {
            des *= d * my.maxVel / radius * 0.5f;
        }
        else
        {
            des *= my.maxVel;
        }

        steering.linear = des - my.vel;
        steering.linear = Vector3.ClampMagnitude(steering.linear, maxAcc);

        return steering;
    }

}
